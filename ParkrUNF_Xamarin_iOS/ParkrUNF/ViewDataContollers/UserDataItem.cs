using System;
using Newtonsoft.Json;

namespace ParkrUNF
{
	public class UserDataItem
	{
		public string Id { get; set; }

		[JsonProperty(PropertyName = "version")]
		public byte[] Version { get; set; } 

		[JsonProperty(PropertyName = "deleted")]
		public bool Deleted { get; set; }

		[JsonProperty(PropertyName = "userName")]
		public string UserName { get; set; }

		[JsonProperty(PropertyName = "firstName")]
		public string FirstName { get; set; }

		[JsonProperty(PropertyName = "lastName")]
		public string LastName { get; set; }

		[JsonProperty(PropertyName = "passwordHash")]
		public string PasswordHash { get; set; }

		[JsonProperty(PropertyName = "password")]
		public string Password { get; set; }

	}
}

