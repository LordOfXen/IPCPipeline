using Newtonsoft.Json;

namespace IPC.Pipeline
{
	/// <summary>
	/// Built-in primitive pipeline data that can store <see cref="bool"/>.
	/// </summary>
	public sealed class PipelineBoolData : PipelineData
	{
		/// <summary>
		/// The value to store in <see cref="bool"/>.
		/// </summary>
		[JsonProperty]
		public bool Value { get; private set; }

		/// <summary>
		/// Constructs a new instance with a new name and a new value for <see cref="Value"/>.
		/// </summary>
		/// <param name="name">The name of the message.</param>
		/// <param name="value">The value to store.</param>
		public PipelineBoolData(string name, bool value) : base(name)
		{
			Value = value;
		}
	}
}
