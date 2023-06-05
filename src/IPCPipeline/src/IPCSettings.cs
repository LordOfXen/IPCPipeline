namespace IPC.Pipeline
{
	/// <summary>
	/// Settings to change the behavior of a <see cref="IPCPipeline"/>.
	/// </summary>
	public sealed class IPCSettings
	{
		/// <summary>
		/// Gets or sets whether to ignore messages sent earlier than the creation date of the underlying <see cref="IPCPipeline"/> instance. This avoids message rain (the ones left in the queue if there are no other subscribers) when connecting to an existing channel of a publisher.
		/// </summary>
		public bool IgnorePastMessages { get; set; }

		/// <summary>
		/// Default constructor for <see cref="IPCSettings"/>.
		/// </summary>
		public IPCSettings() { }
	}
}
