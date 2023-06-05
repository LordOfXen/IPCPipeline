using Newtonsoft.Json;

namespace IPC.Pipeline
{
	/// <summary>
	/// Built-in pipeline data that can store <see cref="string"/>.
	/// </summary>
	public sealed class PipelineStringData : PipelineData
	{
		/// <summary>
		/// The value to store in <see cref="string"/>.
		/// </summary>
		[JsonProperty]
		public string Value { get; private set; }

		/// <summary>
		/// Constructs a new instance with a new name and a new value for <see cref="Value"/>.
		/// </summary>
		/// <param name="name">The name of the message.</param>
		/// <param name="value">The value to store.</param>
		public PipelineStringData(string name, string value) : base(name)
		{
			Value = value;
		}
	}
}
