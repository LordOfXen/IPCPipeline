using IPC.Pipeline;
using System;
using System.Threading.Tasks;

namespace SubscriberTest
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// Create an instance of our pipeline with the same name used to create our publisher so we can read what it sends.
			// PipeAccess.Read makes this a 'subscriber'.
			IPCPipeline pipe = new IPCPipeline("myCustomUniqueChannel", PipeAccess.Read, -1, null);

			// Listen to OnMessageReceived to catch incoming messages.
			pipe.OnMessageReceived += Pipe_OnMessageReceived;

			// Notify about the console viewers about that we are listening to incoming messages now.
			Console.WriteLine("[" + pipe.CreatedAt + "] Listening to incoming messages in channel \"{0}\"...", pipe.ChannelName);

			// Keep the process running.
			while (true)
				Console.ReadLine();
		}

		private static Task Pipe_OnMessageReceived(ulong arg1, object arg2)
		{
			// We do not know what we received so we can first get the base data type.
			PipelineData data = arg2.ToObject<PipelineData>();

			// Note that the name of the data can be null if not specified while sending in the publisher instance.
			if (data.Name == "myIntegerMessage")
			{
				// We now know we received the integer message, can convert to the related data type to gather the value.
				PipelineIntData intData = arg2.ToObject<PipelineIntData>();
				Console.WriteLine("[" + intData.SentAt + "] Received int data \"" + intData.Name + "\": " + intData.Value);
			}
			else
			{
				// Log other type of messages.
				Console.WriteLine("[" + data.SentAt + "] Received data: " + (data.Name ?? "N/A"));
			}

			return Task.CompletedTask;
		}
	}
}
