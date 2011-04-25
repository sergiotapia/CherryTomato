using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CherryTomato.Entities
{
    public static class API_URLS
    {

        #region API Endpoints
        //For future reference, this website lists all of the available endpoints:
        //http://developer.rottentomatoes.com/docs/read/JSON

        /// <summary>
        /// Endpoint for searching for movies via a text query.
        /// Format Key: 0: Api Key, 1: query, 2: Page Limit, 3: Page Index
        /// </summary>
        public const string MOVIE_SEARCH = @"http://api.rottentomatoes.com/api/public/v1.0/movies.json?apikey={0}&q={1}&page_limit={2}&page={3}";

        /// <summary>
        /// Endpoint for searching for an individual movie via it's RottenTomatoes ID number.
        /// </summary>
        public const string MOVIE_INDIVIDUAL_INFORMATION = @"http://api.rottentomatoes.com/api/public/v1.0/movies/{1}.json?apikey={0}";

        /// <summary>
        /// Endpoint for searching for reviews for an individual movie.
        /// </summary>
        public const string MOVIE_INDIVIDUAL_REVIEWS = @"http://api.rottentomatoes.com/api/public/v1.0/movies/{1}/reviews.json?apikey={0}";

        /// <summary>
        /// Endpoint for searching for the cast of an individual movie.
        /// </summary>
        public const string MOVIE_INDIVIDUAL_CAST = @"http://api.rottentomatoes.com/api/public/v1.0/movies/{1}/cast.json?apikey={0}";

        /// <summary>
        /// Endpoint for listing the current box office.
        /// </summary>
        public const string LIST_BOX_OFFICE = @"http://api.rottentomatoes.com/api/public/v1.0/lists/movies/box_office.json?apikey={0}";

        /// <summary>
        /// Endpoint for listing movies that are in theaters.
        /// </summary>
        public const string LIST_IN_THEATERS = @"http://api.rottentomatoes.com/api/public/v1.0/lists/movies/in_theaters.json?apikey={0}";

        /// <summary>
        /// Endpoint for listing opening movies.
        /// </summary>
        public const string LIST_OPENING_SOON = @"http://api.rottentomatoes.com/api/public/v1.0/lists/movies/opening.json?apikey={0}";

        /// <summary>
        /// Endpoint for listing upcoming movies.
        /// </summary>
        public const string LIST_UPCOMING = @"http://api.rottentomatoes.com/api/public/v1.0/lists/movies/upcoming.json?apikey={0}";
        #endregion
    }
}
