using System;
using System.Collections.Generic;
using System.Net;
using CherryTomato.Entities;

namespace CherryTomato
{
    public class Tomato
    {
        #region API Endpoints
        //For future reference, this website lists all of the available endpoints:
        //http://developer.rottentomatoes.com/docs/read/JSON

        /// <summary>
        /// Endpoint for searching for movies via a text query.
        /// </summary>
        private const string MOVIE_SEARCH = @"http://api.rottentomatoes.com/api/public/v1.0/movies.json?apikey={0}&q={1}&page_limit={3}";

        /// <summary>
        /// Endpoint for searching for an individual movie via it's RottenTomatoes ID number.
        /// </summary>
        private const string MOVIE_INDIVIDUAL_INFORMATION = @"http://api.rottentomatoes.com/api/public/v1.0/movies/{1}.json?apikey={0}";

        /// <summary>
        /// Endpoint for searching for reviews for an individual movie.
        /// </summary>
        private const string MOVIE_INDIVIDUAL_REVIEWS = @"http://api.rottentomatoes.com/api/public/v1.0/movies/{1}/reviews.json?apikey={0}";

        /// <summary>
        /// Endpoint for searching for the cast of an individual movie.
        /// </summary>
        private const string MOVIE_INDIVIDUAL_CAST = @"http://api.rottentomatoes.com/api/public/v1.0/movies/{1}/cast.json?apikey={0}";

        /// <summary>
        /// Endpoint for listing the current box office.
        /// </summary>
        private const string LIST_BOX_OFFICE = @"http://api.rottentomatoes.com/api/public/v1.0/lists/movies/box_office.json";
        
        /// <summary>
        /// Endpoint for listing movies that are in theaters.
        /// </summary>
        private const string LIST_IN_THEATERS = @"http://api.rottentomatoes.com/api/public/v1.0/lists/movies/in_theaters.json";
        
        /// <summary>
        /// Endpoint for listing opening movies.
        /// </summary>
        private const string LIST_OPENING_SOON = @"http://api.rottentomatoes.com/api/public/v1.0/lists/movies/opening.json";
        
        /// <summary>
        /// Endpoint for listing upcoming movies.
        /// </summary>
        private const string LIST_UPCOMING = @"http://api.rottentomatoes.com/api/public/v1.0/lists/movies/upcoming.json";
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

        /// <summary>
        /// Gets a list of CastMembers by a movie's ID
        /// </summary>
        /// <param name="movieID">The ID of the movie the cast belongs to</param>
        /// <returns>IEnumerable list of CastMembers</returns>
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

        /// <summary>
        /// Returns a Movie collection when provided with a set of ID numbers.
        /// </summary>
        /// <param name="movieIds">A collection of ints which represent RottenTomatoes ID numbers.</param>
        /// <returns>A collection of Movie objects.</returns>
        public List<Movie> FindMovieCollection(IEnumerable<int> movieIds)
        {
         
            List<Movie> movies = new List<Movie>();

            foreach (var id in movieIds)
            {
                movies.Add(FindMovieById(id));
            }

            return movies;
        }
    }
}
