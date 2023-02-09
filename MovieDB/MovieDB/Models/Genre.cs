using System;
using Newtonsoft.Json;

namespace MovieDB.Models
{
	public class Genre
	{
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
		public string Name { get; set; }
	}
}

