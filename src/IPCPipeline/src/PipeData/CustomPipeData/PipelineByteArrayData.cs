using Newtonsoft.Json;

namespace IPC.Pipeline
{
	/// <summary>
	/// Built-in pipeline data that can store <see cref="byte"/>[].
	/// </summary>
	public sealed class PipelineByteArrayData : PipelineData
	{
		/// <summary>
		/// The value to store in <see cref="byte"/>[].
		/// </summary>
		[JsonProperty]
		public byte[] Value { get; private set; }

		/// <summary>
		/// Constructs a new instance with a new name and a new value for <see cref="Value"/>.
		/// </summary>
		/// <param name="name">The name of the message.</param>
		/// <param name="value">The value to store.</param>
		public PipelineByteArrayData(string name, byte[] value) : base(name)
		{
			Value = value;
		}
	}
}
