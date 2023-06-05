﻿using Newtonsoft.Json;

namespace IPC.Pipeline
{
	/// <summary>
	/// Built-in primitive pipeline data that can store <see cref="int"/>.
	/// </summary>
	public sealed class PipelineIntData : PipelineData
	{
		/// <summary>
		/// The value to store in <see cref="int"/>.
		/// </summary>
		[JsonProperty]
		public int Value { get; private set; }

		/// <summary>
		/// Constructs a new instance with a new name and a new value for <see cref="Value"/>.
		/// </summary>
		/// <param name="name">The name of the message.</param>
		/// <param name="value">The value to store.</param>
		public PipelineIntData(string name, int value) : base(name)
		{
			Value = value;
		}
	}
}
