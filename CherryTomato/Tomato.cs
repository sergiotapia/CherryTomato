﻿using System;
using System.Collections.Generic;
using System.Linq;
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
            var url = String.Format(MOVIE_INDIVIDUAL_INFORMATION, ApiKey, movieId);
            var movie = GetMovieInfoByUrl(url);

            foreach (var link in movie.Links)
            {
                link.Url = link.Url + "?apikey=" + ApiKey;
            }

            return movie;
        }

        /// <summary>
        /// Returns a movie object retrieved from the input Url string.
        /// This is used to return the movie data for the Link items in the Movie class.
        /// </summary>
        /// <param name="url">The url of the Movie object</param>
        /// <returns>Movie data</returns>
        public Movie GetMovieInfoByUrl(string url)
        {
            var jsonResponse = GetJsonResponse(url);
            var movie = Parser.ParseMovie(jsonResponse);

            // The Api Key has to be appended to each link for them to work
            foreach (var link in movie.Links)
            {
                link.Url = link.Url + "?apikey=" + ApiKey;
            }

            return movie;
        }

        /// <summary>
        /// Returns a MovieSearchResults object with the amount of results found and the results.
        /// </summary>
        /// <param name="query">Search term</param>
        /// <param name="pageLimit">Amount of result pages to load</param>
        /// <returns>MovieSearchResults object</returns>
        public MovieSearchResults FindMoviesByQuery(string query, int pageLimit = 10, int page=0)
        {
            var url = String.Format(MOVIE_SEARCH, ApiKey, query, pageLimit, page);
            var jsonResponse = GetJsonResponse(url);
            var results = GetMovieSearchResults(jsonResponse);
            return results;
        }

        /// <summary>
        /// Returns a MovieSearchResults object with the amount of results found and the results.
        /// </summary>
        /// <param name="query">Search url</param>
        /// <param name="pageLimit">Amount of result pages to load</param>
        /// <returns>MovieSearchResults object</returns>
        public MovieSearchResults FindMoviesByUrl(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                var jsonResponse = GetJsonResponse(url);
                var results = GetMovieSearchResults(jsonResponse);
                return results;
            }
            else return null;
        }

        /// <summary>
        /// Gets a list of Movies currently at the top in the Box Office.
        /// </summary>
        /// <returns>MovieSearchResult object</returns>
        public MovieSearchResults FindBoxOfficeList()
        {
            var url = string.Format(LIST_BOX_OFFICE, ApiKey);
            string jsonResponse = GetJsonResponse(url);
            return GetMovieSearchResults(jsonResponse);
        }

        /// <summary>
        /// Gets a list of Movies currently in theaters.
        /// </summary>
        /// <returns>MovieSearchResults</returns>
        public MovieSearchResults FindMoviesInTheaterList()
        {
            var url = string.Format(LIST_IN_THEATERS, ApiKey);
            string jsonResponse = GetJsonResponse(url);
            return GetMovieSearchResults(jsonResponse);
        }

        /// <summary>
        /// Gets a list of opening movies.
        /// </summary>
        /// <returns>MovieSearchResults</returns>
        public MovieSearchResults FindOpeningMoviesList()
        {
            var url = string.Format(LIST_OPENING_SOON, ApiKey);
            var jsonResponse = GetJsonResponse(url);
            return GetMovieSearchResults(jsonResponse);
        }

        private MovieSearchResults GetMovieSearchResults(string jsonResponse)
        {
            var results = Parser.ParseMovieSearchResults(jsonResponse);

            foreach (var link in results.Links)
            {
                link.Url = link.Url + "&apikey=" + ApiKey;
            }

            return results;
        }

        /// <summary>
        /// Gets a list of upcoming movies.
        /// </summary>
        /// <returns>MovieSearchResults</returns>
        public MovieSearchResults FindUpcomingMoviesList()
        {
            var url = string.Format(LIST_UPCOMING, ApiKey);
            var jsonResponse = GetJsonResponse(url);
            return GetMovieSearchResults(jsonResponse);
        }


        public IEnumerable<CastMember> GetFullCastByMovieID(int movieID)
        {
            var url = string.Format(MOVIE_INDIVIDUAL_CAST, ApiKey, movieID);
            var jsonResponse = GetJsonResponse(url);
            var results = Parser.ParseFullMovieCast(jsonResponse);

            return results;
        }

        /// <summary>
        /// Fetches the JSON string from the URL.
        /// </summary>
        /// <param name="url">URL to download the JSON from.</param>
        /// <returns>JSON formatted string</returns>
        private static string GetJsonResponse(string url)
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
            return movieIds.Select(id => FindMovieById(id)).ToList();
        }

        #region API Endpoints
        //For future reference, this website lists all of the available endpoints:
        //http://developer.rottentomatoes.com/docs/read/JSON

        /// <summary>
        /// Endpoint for searching for movies via a text query.
        /// </summary>
        private const string MOVIE_SEARCH = @"http://api.rottentomatoes.com/api/public/v1.0/movies.json?apikey={0}&q={1}&page_limit={2}&page={3}";

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
        private const string LIST_BOX_OFFICE = @"http://api.rottentomatoes.com/api/public/v1.0/lists/movies/box_office.json?apikey={0}";

        /// <summary>
        /// Endpoint for listing movies that are in theaters.
        /// </summary>
        private const string LIST_IN_THEATERS = @"http://api.rottentomatoes.com/api/public/v1.0/lists/movies/in_theaters.json?apikey={0}";

        /// <summary>
        /// Endpoint for listing opening movies.
        /// </summary>
        private const string LIST_OPENING_SOON = @"http://api.rottentomatoes.com/api/public/v1.0/lists/movies/opening.json?apikey={0}";

        /// <summary>
        /// Endpoint for listing upcoming movies.
        /// </summary>
        private const string LIST_UPCOMING = @"http://api.rottentomatoes.com/api/public/v1.0/lists/movies/upcoming.json?apikey={0}";
        #endregion
    }
}
