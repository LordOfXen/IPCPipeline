using System;

namespace IPC.Pipeline
{
	/// <summary>
	/// Default, empty pipeline data that only contains the <see cref="Name"/> proprerty.
	/// This can be used to see which message you received before converting it to your underlying model.
	/// </summary>
	public class PipelineData : IPipelineData
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public string Name { get; protected set; }
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public DateTimeOffset SentAt { get; protected set; }

		/// <summary>
		/// Default constructor to construct an instance of <see cref="PipelineData"/>.
		/// </summary>
		/// <param name="name">The name of the message.</param>
		public PipelineData(string name)
		{
			Name = name;
			SentAt = DateTimeOffset.UtcNow;
		}

		/// <summary>
		/// Protected constructor to construct a parameterless instance of <see cref="PipelineData"/>. Note that this requires the <see cref="Name"/> and <see cref="SentAt"/> properties to be set manually.
		/// </summary>
		protected PipelineData() {}
	}
}
