using System;

namespace IPC.Pipeline
{
	/// <summary>
	/// Exception thrown when tried to send a message in a readonly <see cref="IPCPipeline"/>. To not face this exception, enable write access in <see cref="IPCPipeline.IPCPipeline(string, PipeAccess, int, string, IPCSettings)"/> and make sure to have <see cref="PipeAccess.Write"/> or <see cref="PipeAccess.ReadWrite"/>.
	/// </summary>
	public sealed class ReadOnlyPipeException : Exception
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public ReadOnlyPipeException() : base() { }
		/// <summary>
		/// Default constructor with <paramref name="msg"/> parameter to send a description related to the exception.
		/// </summary>
		/// <param name="msg">The description containing the reason of this exception getting thrown.</param>
		public ReadOnlyPipeException(string msg) : base(msg) { }
	}
}
