using System;
using System.Collections.Generic;

namespace CherryTomato.Entities
{
    public class Movie
    {
        public int RottenTomatoesId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string MpaaRating { get; set; }
        public int Runtime { get; set; }
        public string Synopsis { get; set; }
        public List<ReleaseDate> ReleaseDates { get; set; }
        public List<Rating> Ratings { get; set; }
        public List<string> Genres { get; set; }
        public List<string> Directors { get; set; }
        public List<CastMember> Cast { get; set; }
        public List<Link> Links { get; set; }
        public List<Poster> Posters { get; set; }

        public Movie()
        {
            Directors = new List<string>();
            Genres = new List<string>();
            Cast = new List<CastMember>();
            Links = new List<Link>();
            Posters = new List<Poster>();
            ReleaseDates = new List<ReleaseDate>();
            Ratings = new List<Rating>();
        }
    }

    public class ReleaseDate
    {
        public string Type { get; set; }
        public DateTime Date { get; set; }
    }

    public class CastMember
    {
        public string Actor { get; set; }
        public List<string> Characters { get; set; }

        public CastMember()
        {
            Characters = new List<string>();
        }
    }

    public class Link
    {
        public string Type { get; set; }
        public string Url { get; set; }
    }

    public class Poster
    {
        public string Type { get; set; }
        public string Url { get; set; }
    }

    public class Rating
    {
        public string Type { get; set; }
        public int Score { get; set; }
    }
}