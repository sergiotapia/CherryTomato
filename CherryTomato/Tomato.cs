using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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
        public MovieSearchResults FindMovieByQuery(string query, int pageLimit=10, int startingPage=0)
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
    }
}
