namespace IPC.Pipeline
{
	/// <summary>
	/// The message response containing the status of message delivery to other participants.
	/// </summary>
	public sealed class MessageResponse : IMessageResponse
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public bool IsSuccess { get; }

		/// <summary>
		/// Construct a new response instance to contain the status of the underlying message's delivery.
		/// </summary>
		/// <param name="isSuccess"><see langword="true"/> if the message was delivered successfully, <see langword="false"/> otherwise.</param>
		internal MessageResponse(bool isSuccess)
		{
			IsSuccess = isSuccess;
		}
	}
}
