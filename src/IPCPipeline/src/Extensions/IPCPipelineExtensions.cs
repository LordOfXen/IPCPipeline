using Cloudtoid.Interprocess;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace IPC.Pipeline
{
	/// <summary>
	/// Extensions related to <see cref="IPCPipeline"/> that aims making things easier.
	/// </summary>
	public static class IPCPipelineExtensions
	{
		/// <summary>
		/// Converts the received <see langword="object"/> to the desired underlying object.
		/// The underlying <see langword="object"/> will contain a <seealso cref="JObject"/> instance by default.
		/// </summary>
		/// <typeparam name="T">The desired object to create an instance of with the underlying data of <paramref name="o"/>.</typeparam>
		/// <param name="o">The <see cref="JObject"/> instance to create an instance of <typeparamref name="T"/></param>
		/// <returns>The new <see langword="object"/> created from the <see cref="JObject"/> instance.</returns>
		public static T ToObject<T>(this object o) where T : IPipelineData
		{
			JObject jobj = o as JObject;
			if (jobj == null)
				return default(T);

			return jobj.ToObject<T>();
		}


		/// <summary>
		/// Sends a <see cref="string"/> message in the current channel. This works if <see cref="IPCPipeline.CanWrite"/> is <see langword="true"/>.
		/// <para>Note that this instance will also receive this message if <seealso cref="IPCPipeline.CanRead"/> is <see langword="true"/>.</para>
		/// </summary>
		/// <param name="pipeline">An instance to <see cref="IPCPipeline"/> to send message from.</param>
		/// <param name="messageName">The name of the message for identification purposes.</param>
		/// <param name="value">The text data to send along with the message.</param>
		/// <returns>Returns the response that contains the delivery status of the message.</returns>
		/// <exception cref="ReadOnlyPipeException">Thrown if <see cref="IPCPipeline.CanWrite"/> returns <see langword="false"/>.</exception>
		public static MessageResponse SendMessage(this IPCPipeline pipeline, string messageName, string value)
		{
			return pipeline.SendMessage(new PipelineStringData(messageName, value));
		}

		/// <summary>
		/// Sends a <see cref="byte"/>[]message in the current channel. This works if <see cref="IPCPipeline.CanWrite"/> is <see langword="true"/>.
		/// <para>Note that this instance will also receive this message if <seealso cref="IPCPipeline.CanRead"/> is <see langword="true"/>.</para>
		/// </summary>
		/// <param name="pipeline">An instance to <see cref="IPCPipeline"/> to send message from.</param>
		/// <param name="messageName">The name of the message for identification purposes.</param>
		/// <param name="value">The text data to send along with the message.</param>
		/// <returns>Returns the response that contains the delivery status of the message.</returns>
		/// <exception cref="ReadOnlyPipeException">Thrown if <see cref="IPCPipeline.CanWrite"/> returns <see langword="false"/>.</exception>
		public static MessageResponse SendMessage(this IPCPipeline pipeline, string messageName, byte[] value)
		{
			return pipeline.SendMessage(new PipelineByteArrayData(messageName, value));
		}

		/// <summary>
		/// Sends a <see cref="long"/> message in the current channel. This works if <see cref="IPCPipeline.CanWrite"/> is <see langword="true"/>.
		/// <para>Note that this instance will also receive this message if <seealso cref="IPCPipeline.CanRead"/> is <see langword="true"/>.</para>
		/// </summary>
		/// <param name="pipeline">An instance to <see cref="IPCPipeline"/> to send message from.</param>
		/// <param name="messageName">The name of the message for identification purposes.</param>
		/// <param name="value">The text data to send along with the message.</param>
		/// <returns>Returns the response that contains the delivery status of the message.</returns>
		/// <exception cref="ReadOnlyPipeException">Thrown if <see cref="IPCPipeline.CanWrite"/> returns <see langword="false"/>.</exception>
		public static MessageResponse SendMessage(this IPCPipeline pipeline, string messageName, long value)
		{
			return pipeline.SendMessage(new PipelineLongData(messageName, value));
		}

		/// <summary>
		/// Sends an <see cref="int"/> message in the current channel. This works if <see cref="IPCPipeline.CanWrite"/> is <see langword="true"/>.
		/// <para>Note that this instance will also receive this message if <seealso cref="IPCPipeline.CanRead"/> is <see langword="true"/>.</para>
		/// </summary>
		/// <param name="pipeline">An instance to <see cref="IPCPipeline"/> to send message from.</param>
		/// <param name="messageName">The name of the message for identification purposes.</param>
		/// <param name="value">The text data to send along with the message.</param>
		/// <returns>Returns the response that contains the delivery status of the message.</returns>
		/// <exception cref="ReadOnlyPipeException">Thrown if <see cref="IPCPipeline.CanWrite"/> returns <see langword="false"/>.</exception>
		public static MessageResponse SendMessage(this IPCPipeline pipeline, string messageName, int value)
		{
			return pipeline.SendMessage(new PipelineIntData(messageName, value));
		}

		/// <summary>
		/// Sends a <see cref="double"/> message in the current channel. This works if <see cref="IPCPipeline.CanWrite"/> is <see langword="true"/>.
		/// <para>Note that this instance will also receive this message if <seealso cref="IPCPipeline.CanRead"/> is <see langword="true"/>.</para>
		/// </summary>
		/// <param name="pipeline">An instance to <see cref="IPCPipeline"/> to send message from.</param>
		/// <param name="messageName">The name of the message for identification purposes.</param>
		/// <param name="value">The text data to send along with the message.</param>
		/// <returns>Returns the response that contains the delivery status of the message.</returns>
		/// <exception cref="ReadOnlyPipeException">Thrown if <see cref="IPCPipeline.CanWrite"/> returns <see langword="false"/>.</exception>
		public static MessageResponse SendMessage(this IPCPipeline pipeline, string messageName, double value)
		{
			return pipeline.SendMessage(new PipelineDoubleData(messageName, value));
		}

		/// <summary>
		/// Sends a <see cref="float"/> message in the current channel. This works if <see cref="IPCPipeline.CanWrite"/> is <see langword="true"/>.
		/// <para>Note that this instance will also receive this message if <seealso cref="IPCPipeline.CanRead"/> is <see langword="true"/>.</para>
		/// </summary>
		/// <param name="pipeline">An instance to <see cref="IPCPipeline"/> to send message from.</param>
		/// <param name="messageName">The name of the message for identification purposes.</param>
		/// <param name="value">The text data to send along with the message.</param>
		/// <returns>Returns the response that contains the delivery status of the message.</returns>
		/// <exception cref="ReadOnlyPipeException">Thrown if <see cref="IPCPipeline.CanWrite"/> returns <see langword="false"/>.</exception>
		public static MessageResponse SendMessage(this IPCPipeline pipeline, string messageName, float value)
		{
			return pipeline.SendMessage(new PipelineFloatData(messageName, value));
		}

		/// <summary>
		/// Sends a <see cref="bool"/> message in the current channel. This works if <see cref="IPCPipeline.CanWrite"/> is <see langword="true"/>.
		/// <para>Note that this instance will also receive this message if <seealso cref="IPCPipeline.CanRead"/> is <see langword="true"/>.</para>
		/// </summary>
		/// <param name="pipeline">An instance to <see cref="IPCPipeline"/> to send message from.</param>
		/// <param name="messageName">The name of the message for identification purposes.</param>
		/// <param name="value">The text data to send along with the message.</param>
		/// <returns>Returns the response that contains the delivery status of the message.</returns>
		/// <exception cref="ReadOnlyPipeException">Thrown if <see cref="IPCPipeline.CanWrite"/> returns <see langword="false"/>.</exception>
		public static MessageResponse SendMessage(this IPCPipeline pipeline, string messageName, bool value)
		{
			return pipeline.SendMessage(new PipelineBoolData(messageName, value));
		}

		/// <summary>
		/// Sends a <see cref="string"/> message in the current channel. This works if <see cref="IPCPipeline.CanWrite"/> is <see langword="true"/>.
		/// <para>Note that this instance will also receive this message if <seealso cref="IPCPipeline.CanRead"/> is <see langword="true"/>.</para>
		/// </summary>
		/// <param name="pipeline">An instance to <see cref="IPCPipeline"/> to send message from.</param>
		/// <param name="messageName">The name of the message for identification purposes.</param>
		/// <param name="value">The text data to send along with the message.</param>
		/// <returns>Returns the response that contains the delivery status of the message.</returns>
		/// <exception cref="ReadOnlyPipeException">Thrown if <see cref="IPCPipeline.CanWrite"/> returns <see langword="false"/>.</exception>
		public static async Task<MessageResponse> SendMessageAsync(this IPCPipeline pipeline, string messageName, string value)
		{
			return await pipeline.SendMessageAsync(new PipelineStringData(messageName, value));
		}

		/// <summary>
		/// Sends a <see cref="byte"/>[]message in the current channel. This works if <see cref="IPCPipeline.CanWrite"/> is <see langword="true"/>.
		/// <para>Note that this instance will also receive this message if <seealso cref="IPCPipeline.CanRead"/> is <see langword="true"/>.</para>
		/// </summary>
		/// <param name="pipeline">An instance to <see cref="IPCPipeline"/> to send message from.</param>
		/// <param name="messageName">The name of the message for identification purposes.</param>
		/// <param name="value">The text data to send along with the message.</param>
		/// <returns>Returns the response that contains the delivery status of the message.</returns>
		/// <exception cref="ReadOnlyPipeException">Thrown if <see cref="IPCPipeline.CanWrite"/> returns <see langword="false"/>.</exception>
		public static async Task<MessageResponse> SendMessageAsync(this IPCPipeline pipeline, string messageName, byte[] value)
		{
			return await pipeline.SendMessageAsync(new PipelineByteArrayData(messageName, value));
		}

		/// <summary>
		/// Sends a <see cref="long"/> message in the current channel. This works if <see cref="IPCPipeline.CanWrite"/> is <see langword="true"/>.
		/// <para>Note that this instance will also receive this message if <seealso cref="IPCPipeline.CanRead"/> is <see langword="true"/>.</para>
		/// </summary>
		/// <param name="pipeline">An instance to <see cref="IPCPipeline"/> to send message from.</param>
		/// <param name="messageName">The name of the message for identification purposes.</param>
		/// <param name="value">The text data to send along with the message.</param>
		/// <returns>Returns the response that contains the delivery status of the message.</returns>
		/// <exception cref="ReadOnlyPipeException">Thrown if <see cref="IPCPipeline.CanWrite"/> returns <see langword="false"/>.</exception>
		public static async Task<MessageResponse> SendMessageAsync(this IPCPipeline pipeline, string messageName, long value)
		{
			return await pipeline.SendMessageAsync(new PipelineLongData(messageName, value));
		}

		/// <summary>
		/// Sends an <see cref="int"/> message in the current channel. This works if <see cref="IPCPipeline.CanWrite"/> is <see langword="true"/>.
		/// <para>Note that this instance will also receive this message if <seealso cref="IPCPipeline.CanRead"/> is <see langword="true"/>.</para>
		/// </summary>
		/// <param name="pipeline">An instance to <see cref="IPCPipeline"/> to send message from.</param>
		/// <param name="messageName">The name of the message for identification purposes.</param>
		/// <param name="value">The text data to send along with the message.</param>
		/// <returns>Returns the response that contains the delivery status of the message.</returns>
		/// <exception cref="ReadOnlyPipeException">Thrown if <see cref="IPCPipeline.CanWrite"/> returns <see langword="false"/>.</exception>
		public static async Task<MessageResponse> SendMessageAsync(this IPCPipeline pipeline, string messageName, int value)
		{
			return await pipeline.SendMessageAsync(new PipelineIntData(messageName, value));
		}

		/// <summary>
		/// Sends a <see cref="double"/> message in the current channel. This works if <see cref="IPCPipeline.CanWrite"/> is <see langword="true"/>.
		/// <para>Note that this instance will also receive this message if <seealso cref="IPCPipeline.CanRead"/> is <see langword="true"/>.</para>
		/// </summary>
		/// <param name="pipeline">An instance to <see cref="IPCPipeline"/> to send message from.</param>
		/// <param name="messageName">The name of the message for identification purposes.</param>
		/// <param name="value">The text data to send along with the message.</param>
		/// <returns>Returns the response that contains the delivery status of the message.</returns>
		/// <exception cref="ReadOnlyPipeException">Thrown if <see cref="IPCPipeline.CanWrite"/> returns <see langword="false"/>.</exception>
		public static async Task<MessageResponse> SendMessageAsync(this IPCPipeline pipeline, string messageName, double value)
		{
			return await pipeline.SendMessageAsync(new PipelineDoubleData(messageName, value));
		}

		/// <summary>
		/// Sends a <see cref="float"/> message in the current channel. This works if <see cref="IPCPipeline.CanWrite"/> is <see langword="true"/>.
		/// <para>Note that this instance will also receive this message if <seealso cref="IPCPipeline.CanRead"/> is <see langword="true"/>.</para>
		/// </summary>
		/// <param name="pipeline">An instance to <see cref="IPCPipeline"/> to send message from.</param>
		/// <param name="messageName">The name of the message for identification purposes.</param>
		/// <param name="value">The text data to send along with the message.</param>
		/// <returns>Returns the response that contains the delivery status of the message.</returns>
		/// <exception cref="ReadOnlyPipeException">Thrown if <see cref="IPCPipeline.CanWrite"/> returns <see langword="false"/>.</exception>
		public static async Task<MessageResponse> SendMessageAsync(this IPCPipeline pipeline, string messageName, float value)
		{
			return await pipeline.SendMessageAsync(new PipelineFloatData(messageName, value));
		}

		/// <summary>
		/// Sends a <see cref="bool"/> message in the current channel. This works if <see cref="IPCPipeline.CanWrite"/> is <see langword="true"/>.
		/// <para>Note that this instance will also receive this message if <seealso cref="IPCPipeline.CanRead"/> is <see langword="true"/>.</para>
		/// </summary>
		/// <param name="pipeline">An instance to <see cref="IPCPipeline"/> to send message from.</param>
		/// <param name="messageName">The name of the message for identification purposes.</param>
		/// <param name="value">The text data to send along with the message.</param>
		/// <returns>Returns the response that contains the delivery status of the message.</returns>
		/// <exception cref="ReadOnlyPipeException">Thrown if <see cref="IPCPipeline.CanWrite"/> returns <see langword="false"/>.</exception>
		public static async Task<MessageResponse> SendMessageAsync(this IPCPipeline pipeline, string messageName, bool value)
		{
			return await pipeline.SendMessageAsync(new PipelineBoolData(messageName, value));
		}
	}
}
