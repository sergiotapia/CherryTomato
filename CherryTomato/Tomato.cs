using System;
using System.Collections.Generic;
using System.Net;
using CherryTomato.Entities;

namespace CherryTomato
{
    public class Tomato
    {
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
            var url = String.Format(@"http://api.rottentomatoes.com/api/public/v1.0/movies/{0}.json?apikey={1}", movieId, ApiKey);
            string response;

            using (var client = new WebClient())
            {
                response = client.DownloadString(url);
            }

            Movie movie = Parser.ParseMovie(response);
            return movie;
        }

        /// <summary>
        /// Returns a MovieSearchResults object with the amount of results found and the results.
        /// </summary>
        /// <param name="query">Search parameter</param>
        /// <param name="pageLimit">Amount of result pages to load</param>
        /// <param name="startingPage">Which page to start on</param>
        /// <returns></returns>
        public MovieSearchResults FindMovieByQuery(string query, int pageLimit, int startingPage)
        {
            var url = String.Format(@"http://api.rottentomatoes.com/api/public/v1.0/movies.json?apikey={0}&q={1}&page_limit={2}&page={3}",ApiKey, query, pageLimit, startingPage);

            string response;

            using (var client = new WebClient())
            {
                response = client.DownloadString(url);
            }

            MovieSearchResults results = Parser.ParseMovieSearchResults(response);
            return results;
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
