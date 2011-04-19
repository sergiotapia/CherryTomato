using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CherryTomato.Entities
{
    public class MovieSearchResults
    {
        /// <summary>
        /// Total Number Of Results
        /// </summary>
        public int ResultCount { get; set; }

        /// <summary>
        /// List Of Movie Results
        /// </summary>
        public List<Movie> Results { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MovieSearchResults()
        {
            Results = new List<Movie>();
        }
    }
}
