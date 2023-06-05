using System;

namespace IPC.Pipeline
{
	/// <summary>
	/// The PipeAccess enumeration to pick which operations the underlying pipeline will use.
	/// </summary>
	[Flags]
	public enum PipeAccess
	{
		/// <summary>
		/// Specifies that the underlying pipeline will read data.
		/// </summary>
		Read = 1,
		/// <summary>
		/// Specifies that the underlying pipeline will write data.
		/// </summary>
		Write = 2,
		/// <summary>
		/// Specifies that the underlying pipeline will both read and write data.
		/// </summary>
		ReadWrite = Read | Write,
	}
}
