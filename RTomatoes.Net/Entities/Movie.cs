using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace RTomatoes.Net.Entities
{
    public class Movie
    {
        public int id { get; set; }
        public string title { get; set; }
        public int? year { get; set; }
        public int? runtime { get; set; }
        public ReleaseDate release_dates { get; set; }
        public Rating ratings { get; set; }
        public string synopsis { get; set; }
        public Poster posters { get; set; }
        public List<Cast> abridged_cast { get; set; }
        public List<Directors> abridged_directors { get; set; }
        public List<Link> links { get; set; }
        public List<string> genres { get; set; }
        public string mpaa_rating { get; set; }
    }
}
