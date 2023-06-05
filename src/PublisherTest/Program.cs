using System;
using System.Linq;
using IPC.Pipeline;

namespace PublisherTest
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// Create an instance of our pipeline with a unique channel name.
			// The specified channel name will be used to read and write process independent data.
			// PipeAccess.Write makes this a 'publisher'.
			IPCPipeline pipe = new IPCPipeline("myCustomUniqueChannel", PipeAccess.Write);

			// Create a Random for generating to send a random integer value.
			Random r = new Random();
			while(true)
			{
				// Notify console viewers about the action we are going to take.
				Console.WriteLine("[" + pipe.CreatedAt + "] Sending data in channel {0}...", pipe.ChannelName);

				// If we have a greater value than 0.6 from NextDouble, we can instead send a text message with a random text.
				if (r.NextDouble() > 0.6)
				{
					string randomText = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", r.Next(5, 64)).Select(s => s[r.Next(s.Length)]).ToArray());
					pipe.SendMessage("textMessage", randomText);
				}
				else
				{
					// Send an integer message to subscribers.
					pipe.SendMessage("myIntegerMessage", r.Next());
				}

				// Limit the amount of messages we are going to send by sleeping the thread for a second.
				System.Threading.Thread.Sleep(1000);
			}
		}
	}
}
