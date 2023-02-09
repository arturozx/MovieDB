using System;
using System.Collections.Generic;

namespace MovieDB.Models
{
	public class MoviesByGenre
	{
        public string GenreName { get; set; }
        public List<Movie> MoviesList { get; set; }
	}
}

