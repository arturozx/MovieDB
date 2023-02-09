using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MovieDB.Models
{
	public class Movies
	{
        [JsonProperty(PropertyName = "results")]
        public List<Movie> MoviesList { get; set; }
	}
}

