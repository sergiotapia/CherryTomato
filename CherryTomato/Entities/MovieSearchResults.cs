using System;
using System.Collections.Generic;

namespace CherryTomato.Entities
{
    public class MovieSearchResults
    {
        public int ResultCount { get; set; }
        public List<Result> Results { get; set; }

        public MovieSearchResults()
        {
            Results = new List<Result>();
        }
    }

    public class Result
    {
        public int RottenTomatoesId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int Runtime { get; set; }
        public List<ReleaseDate> ReleaseDates { get; set; }
        public List<Rating> Ratings { get; set; }
        public string Synopsis { get; set; }
        public List<Poster> Posters { get; set; }
        public List<CastMember> Cast { get; set; }
        public List<Link> Links { get; set; }

        public Result()
        {
            ReleaseDates = new List<ReleaseDate>();
            Ratings = new List<Rating>();
            Posters = new List<Poster>();
            Cast = new List<CastMember>();
            Links = new List<Link>();
        }
    }
}
