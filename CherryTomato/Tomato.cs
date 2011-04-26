using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CherryTomato.Entities;

namespace CherryTomato
{
    public class Tomato
    {
        public string ApiKey { get; set; }

        private MovieSearchResults SearchResults { get; set; }

        private Movie MovieInfo { get; set; }

        public MovieSearchResults NextPage
        {
            get
            {
                SearchResults = GetMovieSearchResults(SearchResults.NextPage);
                return SearchResults;
            }
        }

        public MovieSearchResults PreviousPage
        {
            get
            {
                SearchResults = GetMovieSearchResults(SearchResults.PreviousPage);
                return SearchResults;
            }
        }


        public Tomato(string apiKey)
        {
            ApiKey = apiKey;
            MovieInfo = new Movie();
            SearchResults = new MovieSearchResults();
        }

        /// <summary>
        /// Returns a Movie object with fields populated with relevant data. 
        /// </summary>
        /// <param name="movieId">RottenTomatoes unique ID number.</param>
        /// <returns>Movie object.</returns>
        public Movie FindMovieById(int movieId)
        {
            var url = String.Format(API_URLS.MOVIE_INDIVIDUAL_INFORMATION, ApiKey, movieId);
            return GetMovieInfoByUrl(url);;
        }

        /// <summary>
        /// Returns a movie object retrieved from the input Url string.
        /// This is used to return the movie data for the Link items in the Movie class.
        /// </summary>
        /// <param name="url">The url of the Movie object</param>
        /// <returns>Movie data</returns>
        public Movie GetMovieInfoByUrl(string url)
        {
            MovieInfo = Parser.ParseMovie(Parser.GetJsonResponse(url));

            // The Api Key has to be appended to each link for them to work
            foreach (var link in MovieInfo.Links)
            {
                link.Url = link.Url + "?apikey=" + ApiKey;
            }

            return MovieInfo;
        }

        /// <summary>
        /// Returns a MovieSearchResults object with the amount of results found and the results.
        /// </summary>
        /// <param name="query">Search term</param>
        /// <param name="pageLimit">Amount of result pages to load</param>
        /// <returns>MovieSearchResults object</returns>
        public MovieSearchResults FindMoviesByQuery(string query, int pageLimit = 10, int page=0)
        {
            var url = String.Format(API_URLS.MOVIE_SEARCH, ApiKey, query, pageLimit, page);
            return GetMovieSearchResults(url);
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
                return GetMovieSearchResults(url);
            }
            else return null;
        }

        /// <summary>
        /// Gets a list of Movies currently at the top in the Box Office.
        /// </summary>
        /// <returns>MovieSearchResult object</returns>
        public MovieSearchResults FindBoxOfficeList()
        {
            var url = string.Format(API_URLS.LIST_BOX_OFFICE, ApiKey);
            return GetMovieSearchResults(url);
        }

        /// <summary>
        /// Gets a list of Movies currently in theaters.
        /// </summary>
        /// <returns>MovieSearchResults</returns>
        public MovieSearchResults FindMoviesInTheaterList()
        {
            var url = string.Format(API_URLS.LIST_IN_THEATERS, ApiKey);
            return GetMovieSearchResults(url);
        }

        /// <summary>
        /// Gets a list of opening movies.
        /// </summary>
        /// <returns>MovieSearchResults</returns>
        public MovieSearchResults FindOpeningMoviesList()
        {
            var url = string.Format(API_URLS.LIST_OPENING_SOON, ApiKey);
            return GetMovieSearchResults(url);
        }


        /// <summary>
        /// Gets a list of upcoming movies.
        /// </summary>
        /// <returns>MovieSearchResults</returns>
        public MovieSearchResults FindUpcomingMoviesList()
        {
            var url = string.Format(API_URLS.LIST_UPCOMING, ApiKey);
            return GetMovieSearchResults(url);
        }

        /// <summary>
        /// Get the parsed MovieSearchResults object with formatted links
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private MovieSearchResults GetMovieSearchResults(string url)
        {
            SearchResults = Parser.ParseMovieSearchResults(url);

            foreach (var link in SearchResults.Links)
            {
                link.Url = link.Url + "&apikey=" + ApiKey;
            }

            return SearchResults;
        }

        public IEnumerable<CastMember> GetFullCastByMovieID(int movieID)
        {
            var url = string.Format(API_URLS.MOVIE_INDIVIDUAL_CAST, ApiKey, movieID);
            return Parser.ParseFullMovieCast(Parser.GetJsonResponse(url));
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
    }
}
