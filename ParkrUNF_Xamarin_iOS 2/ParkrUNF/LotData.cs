using System;
using Newtonsoft.Json;

namespace ParkrUNF
{
	public class LotData
	{
		public string Id { get; set; }

		[JsonProperty(PropertyName = "deleted")]
		public bool Deleted { get; set; }

		[JsonProperty(PropertyName = "lotId")]
		public string lotId { get; set; }

		[JsonProperty(PropertyName = "floor")]
		public bool Floor { get; set; }

		[JsonProperty(PropertyName = "t10_5")]
		public double T10_5 { get; set; }

		[JsonProperty(PropertyName = "t11")]
		public int T11 { get; set; }

		[JsonProperty(PropertyName = "t11_5")]
		public int T11_5 { get; set; }

		[JsonProperty(PropertyName = "t12")]
		public int T12 { get; set; }

		[JsonProperty(PropertyName = "t12_5")]
		public int T12_5 { get; set; }

		[JsonProperty(PropertyName = "t1")]
		public double T1 { get; set; }

		[JsonProperty(PropertyName = "t1_5")]
		public int T1_5 { get; set; }

		[JsonProperty(PropertyName = "t2")]
		public int T2 { get; set; }

		[JsonProperty(PropertyName = "t2_5")]
		public int T2_5 { get; set; }

		[JsonProperty(PropertyName = "t3")]
		public int T3 { get; set; }

		[JsonProperty(PropertyName = "t3_5")]
		public int T3_5 { get; set; }

		[JsonProperty(PropertyName = "t4")]
		public int T4 { get; set; }

		[JsonProperty(PropertyName = "t4_5")]
		public int T4_5 { get; set; }

		[JsonProperty(PropertyName = "t5")]
		public int T5 { get; set; }

		[JsonProperty(PropertyName = "t5_5")]
		public int T5_5 { get; set; }

		[JsonProperty(PropertyName = "t6")]
		public int T6 { get; set; }

	}
}

