using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using CherryTomato.Entities;
using System.Diagnostics;
using System.IO;

namespace CherryTomato
{
    public class Tomato
    {

        #region URL CONSTANTS
        private const string MOVIE_URL = @"http://api.rottentomatoes.com/api/public/v1.0/movies/{0}.json?apikey={1}";
        private const string SEARCH_URL = @"http://api.rottentomatoes.com/api/public/v1.0/movies/movies.json?apikey={0}";
        private const string MOVIE_LIST_URL = @"http://api.rottentomatoes.com/api/public/v1.0/lists/movies/{0}.json?apikey={1}";
        private const string DVD_LIST_URL = @"http://api.rottentomatoes.com/api/public/v1.0/lists/dvds.json?apikey={0}";
        private const string URL_QUERY = @"&q={2}&page_limit={3}&page={4}";
        private const string MOVIE_ITEMS_URL = @"http://api.rottentomatoes.com/api/public/v1.0/movies/{0}/{1}.json?apikey={2}";
        #endregion

        public string ApiKey { get; set; }

        public Tomato(string apiKey)
        {
            ApiKey = apiKey;
        }

        /// <summary>
        /// Returns a Movie object with fields populated with relevant data. 
        /// </summary>
        /// <param name="movieId">RottenTomatoes unique ID number.</param>
        /// <returns>Movie object.</returns>
        public Movie FindMovieById(int movieId)
        {
            var url = String.Format(MOVIE_URL, movieId, ApiKey);
            return Parser.ParseMovie(GetJSONString(url));
        }

        /// <summary>
        /// Returns a MovieSearchResults object with the amount of results found and the results.
        /// </summary>
        /// <param name="query">Search parameter</param>
        /// <param name="pageLimit">Amount of result pages to load</param>
        /// <param name="startingPage">Which page to start on</param>
        /// <returns>List of Movie Results</returns>
        public MovieSearchResults FindMovieByQuery(string query, int pageLimit=10, int startingPage=0)
        {
            var url = String.Format(SEARCH_URL + URL_QUERY, ApiKey, null, query, pageLimit, startingPage);
            return Parser.ParseMovieSearchResults(GetJSONString(url));
        }

        /// <summary>
        /// Gets a list of Box Office's Top Ten movies
        /// </summary>
        /// <returns>MovieSearchResults</returns>
        public MovieSearchResults GetBoxOfficeList()
        {
            var url = string.Format(MOVIE_LIST_URL, "box_office", ApiKey);
            var results = Parser.ParseMovieSearchResults(GetJSONString(url));

            if (results.ResultCount == 0) results.ResultCount = results.Count;
            return results;
        }

        /// <summary>
        /// Gets a list of movies currently in theaters
        /// </summary>
        /// <param name="page_index">Page to return</param>
        /// <param name="page_limit">Number of items per page</param>
        /// <returns>MovieSearchResults</returns>
        public MovieSearchResults GetInTheatersList(int page_index=0, int page_limit=10)
        {
            var url = string.Format(MOVIE_LIST_URL + URL_QUERY, "in_theaters", ApiKey, null, page_limit, page_index);
            var search = Parser.ParseMovieSearchResults(GetJSONString(url));

            if (search.ResultCount == 0) search.ResultCount = search.Count;
            return search;
        }

        /// <summary>
        /// Gets a list of opening movies
        /// </summary>
        /// <returns>MovieSearchResults</returns>
        public MovieSearchResults GetOpeningMoviesList()
        {
            var url = string.Format(MOVIE_LIST_URL, "opening", ApiKey);
            var search = Parser.ParseMovieSearchResults(GetJSONString(url));

            if (search.ResultCount == 0) search.ResultCount = search.Count;
            return search;
        }

        /// <summary>
        /// Gets a list of upcoming movies
        /// </summary>
        /// <param name="page_index">Page to return</param>
        /// <param name="page_limit">Number of items per page</param>
        /// <returns>MovieSearchResults</returns>
        public MovieSearchResults GetUpcomingMoviesList(int page_index = 0, int page_limit = 10)
        {
            var url = string.Format(MOVIE_LIST_URL + URL_QUERY, "upcoming", ApiKey, null, page_limit, page_index);
            var search = Parser.ParseMovieSearchResults(GetJSONString(url));

            if (search.ResultCount == 0) search.ResultCount = search.Count;
            return search;
        }


        public IEnumerable<CastMember> GetCastByMovieID(int movieID)
        {
            string url = string.Format(MOVIE_ITEMS_URL, movieID, "cast", ApiKey);
            return Parser.ParseCastMembers(GetJSONString(url));
        }

        /// <summary>
        /// Fetches the JSON string from the input url
        /// </summary>
        /// <param name="url">URL to get the JSON string from</param>
        /// <returns>JSON formatted string</returns>
        private string GetJSONString(string url)
        {
            using (var client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }
    }
}
