using System;
using Cloudtoid.Interprocess;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;
using System.Diagnostics;
using System.Runtime;

namespace IPC.Pipeline
{
	/// <summary>
	/// Inter-process bi-directional communication pipeline (IPC) or (IPBDC) that can both write and read data at the same time from the underlying channel. This uses <seealso cref="QueueFactory"/>, <seealso cref="IPublisher"/> and <seealso cref="ISubscriber"/> as the low-level backend API for communicating with other participants in the channel.
	/// <para>I recommend having one writer and multiple readers at the same time in one channel, but you can also have multiple writers and readers at the same time in the same channel. This class only wraps low-level part of interprocess library to easily setup and fit your projects without having to deal with <see cref="CancellationToken"/>, <see cref="CancellationTokenSource"/>, <see cref="QueueFactory"/>, <see cref="IPublisher"/>, <see cref="ISubscriber"/>, <see cref="Thread"/>, <see cref="System.Buffers.ArrayPool{T}"/>, <see cref="ReadOnlyMemory{T}"/>, <see cref="Memory{T}"/>, <see cref="ReadOnlySpan{T}"/> and <see cref="Span{T}"/>. Making it easier to implement and keep everything tidy in your project.
	/// <para>This interprocess pipeline works with JSON serialization/deserialization like for example a network package. Automatically serializes the given object to JSON in the <see cref="SendMessage(IPipelineData)"/> (or asynchronous version <see cref="SendMessageAsync(IPipelineData)"/>) method and then deserializes just before firing the <see cref="OnMessageReceived"/> event when received.</para>
	/// </para>
	/// </summary>
	public class IPCPipeline : IDisposable
	{
		/// <summary>
		/// Fired when a message received. Only works if <see cref="CanRead"/> is <see langword="true"/>/
		/// </summary>
		public event Func<ulong, object, Task> OnMessageReceived;

		private readonly QueueFactory factory;
		private IPublisher publisher;
		private volatile ISubscriber subscriber;
		private readonly Thread subThr;

		private bool _disposed;

		/// <summary>
		/// Gets the creation date of the current <see cref="IPCPipeline"/> instance.
		/// </summary>
		public DateTimeOffset CreatedAt { get; private set; }

		/// <summary>
		/// Gets whether this <see cref="IPCPipeline"/> can write. This checks if the given <seealso cref="PipeAccess"/> have <see cref="PipeAccess.Write"/>.
		/// </summary>
		public bool CanWrite => (_pipeAccess & PipeAccess.Write) == PipeAccess.Write;

		/// <summary>
		/// Gets whether this <see cref="IPCPipeline"/> can read. This checks if the given <seealso cref="PipeAccess"/> have <see cref="PipeAccess.Read"/>.
 		/// </summary>
		public bool CanRead => (_pipeAccess & PipeAccess.Read) == PipeAccess.Read;

		/// <summary>
		/// Gets the channel name this <see cref="IPCPipeline"/> communicates in.
		/// </summary>
		public string ChannelName => _channelName;

		private volatile CancellationTokenSource _cancellationTokenSource;
		private readonly CancellationToken _cancellationToken;
#pragma warning disable CA1805 // Do not initialize unnecessarily
		private readonly int bufSize = 0;
#pragma warning restore CA1805 // Do not initialize unnecessarily
#pragma warning disable CA1805 // Do not initialize unnecessarily
		private ulong msgId = 0;
#pragma warning restore CA1805 // Do not initialize unnecessarily
		private readonly PipeAccess _pipeAccess;
		private readonly string _channelName;
		private readonly IPCSettings _settings;

		/// <summary>
		/// Initializes a new instance of <see cref="IPCPipeline"/> that can connect to an existing channel or have a new channel.
		/// </summary>
		/// <param name="channelName">The name of the channel. Make sure to have a unique name for each channel. You will use this name in other processes to receive from or send to this instance.</param>
		/// <param name="accessMode">The access mode that specifies the use-case of this instance. Only enable the ones you want to use this instance for.</param>
		/// <param name="bufferSize">The size of the queue in bytes. Leave it -1 for the default size. The deafult size is (1024 * 1024) * 4 (4.194304 megabytes).</param>
		/// <param name="path">The path to the directory/folder in which the memory mapped and other files are stored in. Leave it <see langword="null"/> for the default path. This only works in Unix based systems.</param>
		/// <param name="settings">Extra settings that might be helpful to setup a proper instance of <see cref="IPCPipeline"/>.</param>
		public IPCPipeline(string channelName, PipeAccess accessMode, int bufferSize = -1, string path = null, IPCSettings settings = null)
		{
			CreatedAt = DateTimeOffset.UtcNow;
			_settings = settings ?? new IPCSettings();
			_channelName = channelName;
			_pipeAccess = accessMode;

			factory = new QueueFactory();

			if (bufferSize == -1)
				bufSize = (1024 * 1024) * 4;
			else
				bufSize = bufferSize;

			QueueOptions options = null;

			if (path == null)
				options = new QueueOptions(
					queueName: channelName,
					bytesCapacity: bufSize
					);
			else
				options = new QueueOptions(
						queueName: channelName,
						bytesCapacity: bufSize,
						path: path
						);

			if (CanWrite)
				publisher = factory.CreatePublisher(options);

			if (CanRead)
				subscriber = factory.CreateSubscriber(options);

			if (CanRead)
			{
				_cancellationTokenSource = new CancellationTokenSource();
				_cancellationToken = _cancellationTokenSource.Token;

				subThr = new Thread(ThrCallback);
				subThr.Start();
			}
		}

		private async void ThrCallback()
		{
			while (!_cancellationTokenSource.IsCancellationRequested)
			{
				_cancellationToken.ThrowIfCancellationRequested();

				byte[] buffer = System.Buffers.ArrayPool<byte>.Shared.Rent(bufSize);
				try
				{
					ReadOnlyMemory<byte> payload = subscriber.Dequeue(new Memory<byte>(buffer), _cancellationToken);
					await AsyncCallback(msgId, payload.ToArray());
				}
				finally
				{
					System.Buffers.ArrayPool<byte>.Shared.Return(buffer, true);

					// Increment message id.
					msgId++;
				}
			}
		}

		private async Task AsyncCallback(ulong msgId, byte[] payload)
		{
			if (OnMessageReceived == null)
				return;

			string payloadString;
			try
			{
				payloadString = System.Text.Encoding.Unicode.GetString(payload);
			}
			catch (Exception ex)
			{
				Trace.WriteLine("Exception while getting payload string: " + ex.ToString() + "");
				return;
			}

			if (payloadString == null)
				return;

			Trace.WriteLine("Payload #" + msgId + " received (" + payload.Length + " byte" + (payload.Length > 1 ? "s" : "") + "): " + payloadString);

			object deserializedData = JsonConvert.DeserializeObject(payloadString, new JsonSerializerSettings()
			{
				NullValueHandling = NullValueHandling.Include,
			});

			if (_settings.IgnorePastMessages)
			{
				PipelineData data = deserializedData.ToObject<PipelineData>();

				// We are requested to ignore previous messages that were in queue before.
				if (data.SentAt < CreatedAt)
					return;
			}

			await OnMessageReceived(msgId, deserializedData).ConfigureAwait(false);
		}


		/// <summary>
		/// Sends a message in the current channel. This works if <see cref="CanWrite"/> is <see langword="true"/>.
		/// <para>Note that this instance will also receive this message if <seealso cref="CanRead"/> is <see langword="true"/>.</para>
		/// </summary>
		/// <param name="data">The data to send along with the message.</param>
		/// <returns>Returns the response that contains the delivery status of the message.</returns>
		/// <exception cref="ReadOnlyPipeException">Thrown if <see cref="CanWrite"/> returns <see langword="false"/>.</exception>
		public virtual MessageResponse SendMessage(IPipelineData data)
		{
			if (!CanWrite)
				throw new ReadOnlyPipeException();

			string retvalJson = JsonConvert.SerializeObject(data, new JsonSerializerSettings()
			{
				NullValueHandling = NullValueHandling.Include,
			});
			if (retvalJson == null)
				return null;

			byte[] bytes = System.Text.Encoding.Unicode.GetBytes(retvalJson);

			if (!publisher.TryEnqueue(new ReadOnlySpan<byte>(bytes)))
			{
				return new MessageResponse(false);
			}

			return new MessageResponse(true);
		}

		/// <summary>
		/// Sends a message in the current channel asynchronously. This works if <see cref="CanWrite"/> is <see langword="true"/>.
		/// <para>Note that this instance will also receive this message if <seealso cref="CanRead"/> is <see langword="true"/>.</para>
		/// </summary>
		/// <param name="data">The data to send along with the message.</param>
		/// <returns>Returns the response that contains the delivery status of the message.</returns>
		/// <exception cref="ReadOnlyPipeException">Thrown if <see cref="CanWrite"/> returns <see langword="false"/>.</exception>
		public virtual async Task<MessageResponse> SendMessageAsync(IPipelineData data)
		{
			if (!CanWrite)
				throw new ReadOnlyPipeException();

			return await Task.FromResult(SendMessage(data));
		}

		/// <summary>
		///  Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <param name="disposing"><see langword="true"/> to dispose managed objects, otherwise <see langword="false"/>.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
					if (CanWrite && publisher != null)
					{
						publisher.Dispose();
						publisher = null;
					}

					if (CanRead && subscriber != null)
					{
						subscriber.Dispose();
						subscriber = null;
					}

					if (CanRead && _cancellationTokenSource != null)
					{
						if (!_cancellationTokenSource.IsCancellationRequested)
							_cancellationTokenSource.Cancel();

						_cancellationTokenSource.Dispose();
						_cancellationTokenSource = null;
					}
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				_disposed = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~C_GatewayPipeline()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
