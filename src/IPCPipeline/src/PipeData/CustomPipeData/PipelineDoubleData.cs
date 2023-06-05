using Newtonsoft.Json;

namespace IPC.Pipeline
{
	/// <summary>
	/// Built-in primitive pipeline data that can store <see cref="double"/>.
	/// </summary>
	public sealed class PipelineDoubleData : PipelineData
	{
		/// <summary>
		/// The value to store in <see cref="double"/>.
		/// </summary>
		[JsonProperty]
		public double Value { get; private set; }

		/// <summary>
		/// Constructs a new instance with a new name and a new value for <see cref="Value"/>.
		/// </summary>
		/// <param name="name">The name of the message.</param>
		/// <param name="value">The value to store.</param>
		public PipelineDoubleData(string name, double value) : base(name)
		{
			Value = value;
		}
	}
}
