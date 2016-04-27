using System;
using Newtonsoft.Json;

namespace ParkrUNF
{
	public class ToDoItem
	{
		public string Id { get; set; }

		[JsonProperty(PropertyName = "version")]
		public byte[] Version { get; set; } 

		[JsonProperty(PropertyName = "text")]
		public string Text { get; set; }

		[JsonProperty(PropertyName = "complete")]
		public bool Complete { get; set; }
	}
}

