using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MovieDB.Models
{
	public class Genres
	{
        [JsonProperty(PropertyName = "genres")]
        public List<Genre> GenresList { get; set; }
	}
}

