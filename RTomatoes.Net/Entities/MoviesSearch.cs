using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace RTomatoes.Net.Entities
{
    public class MoviesSearch
    {
        public int total { get; set; }
        public List<Movie> movies { get; set; }
        public Link links { get; set; }
        public string link_template { get; set; }
    }
}
