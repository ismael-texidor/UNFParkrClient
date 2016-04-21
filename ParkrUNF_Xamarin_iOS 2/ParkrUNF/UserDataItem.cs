using System;
using Newtonsoft.Json;

namespace ParkrUNF
{
	public class UserDataItem
	{
		public string Id { get; set; }

		[JsonProperty(PropertyName = "text")]
		public string Text { get; set; }

		[JsonProperty(PropertyName = "complete")]
		public bool Complete { get; set; }

		[JsonProperty(PropertyName = "userName")]
		public string Username { get; set; }

		[JsonProperty(PropertyName = "passwordHash")]
		public string PasswordHash { get; set; }

		[JsonProperty(PropertyName = "firstName")]
		public string FirstName { get; set; }

		[JsonProperty(PropertyName = "lastName")]
		public string LastName { get; set; }

	}
}

