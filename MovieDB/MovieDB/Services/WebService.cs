using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using MovieDB.Models;
using Newtonsoft.Json.Converters;

namespace MovieDB.Services
{
	public class WebService
	{
        #region Constants
        private const string API_KEY = "?api_key=02827685182e761fd77f3cac07b1bef6";
        private const string MOVIES_URL = "https://api.themoviedb.org/3/{0}{1}{2}";
        #endregion

        #region Vars and Properties
        private HttpClient _client;
        #endregion


        #region Lifecycle
		public WebService()
		{
			this._client = new HttpClient();
		}
        #endregion

        #region Methods
        public async Task<List<Genre>> GetGenres()
        {
            Uri uri = new Uri(string.Format(MOVIES_URL, "genre/movie/list", API_KEY, string.Empty));
            HttpResponseMessage response = await this._client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Genres>(content);
                return result.GenresList;
            }

            return null;
        }

        public async Task<List<Movie>> GetMoviesByGenre(int genreId)
        {
            Uri uri = new Uri(string.Format(MOVIES_URL, "discover/movie", API_KEY, $"&with_genres={genreId}"));
            HttpResponseMessage response = await this._client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Movies>(content);
                return result.MoviesList;
            }

            return null;
        }
        #endregion
    }
}

