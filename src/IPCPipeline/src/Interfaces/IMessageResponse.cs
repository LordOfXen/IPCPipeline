namespace IPC.Pipeline
{
	/// <summary>
	/// Message Response interface for flexibility in case we get new classes in the future.
	/// </summary>
	public interface IMessageResponse
	{
		/// <summary>
		/// Gets whether the message delivered successfully or failed.
		/// </summary>
		bool IsSuccess { get; }
	}
}
