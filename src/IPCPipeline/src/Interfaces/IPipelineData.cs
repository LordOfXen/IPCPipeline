using Newtonsoft.Json;
using System;

namespace IPC.Pipeline
{
	/// <summary>
	/// Root interface for pipeline data to store information in messages.
	/// </summary>
	public interface IPipelineData
	{
		/// <summary>
		/// Gets the name of the message.
		/// </summary>
		[JsonProperty]
		string Name { get; }
		/// <summary>
		/// Gets the time this message was sent at.
		/// </summary>
		[JsonProperty]
		DateTimeOffset SentAt { get; }
	}
}
