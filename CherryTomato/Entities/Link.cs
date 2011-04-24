using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CherryTomato.Entities
{
    public class Link
    {
        public string Type { get; set; }
        public string Url { get; set; }
    }

    /// <summary>
    /// LinkCollection for Movie class Links
    /// </summary>
    public class MovieLinkCollection : List<Link>
    {
        public MovieLinkCollection() : base() { }

        /// <summary>
        /// Gets and sets the url for the the current page or movie item
        /// </summary>
        public string Self
        {
            get
            {
                if (base.Exists(p => p.Type.ToLower() == "self"))
                    return base.Find(p => p.Type.ToLower() == "self").Url;
                return null;
            }

            set
            {
                if (!base.Exists(p => p.Type.ToLower() == "self"))
                {
                    var link = new Link()
                    {
                        Type = "self",
                        Url = value
                    };
                    base.Add(link);
                }
                else
                {
                    base.Find(p => p.Type.ToLower() == "self").Url = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the url for the link to the RottenTomatoes website for this movie
        /// </summary>
        public string Alternate
        {
            get
            {
                if (base.Exists(p => p.Type.ToLower() == "alternate"))
                    return base.Find(p => p.Type.ToLower() == "alternate").Url;
                return null;
            }

            set
            {
                if (!base.Exists(p => p.Type.ToLower() == "alternate"))
                {
                    var link = new Link()
                    {
                        Type = "alternate",
                        Url = value
                    };
                    base.Add(link);
                }
                else
                {
                    base.Find(p => p.Type.ToLower() == "alternate").Url = value;
                }
            }
        }
    }


    /// <summary>
    /// Link collection for MovieSearchResults class
    /// </summary>
    public class MovieSearchLinkCollection : List<Link>
    {
        /// <summary>
        /// Gets and sets the url for the the current page or movie item
        /// </summary>
        public string Self
        {
            get
            {
                if (base.Exists(p => p.Type.ToLower() == "self"))
                    return base.Find(p => p.Type.ToLower() == "self").Url;
                return null;
            }

            set
            {
                if (!base.Exists(p => p.Type.ToLower() == "self"))
                {
                    var link = new Link()
                    {
                        Type = "self",
                        Url = value
                    };
                    base.Add(link);
                }
                else
                {
                    base.Find(p => p.Type.ToLower() == "self").Url = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the url for the next MovieSearchResult page
        /// </summary>
        public string Next
        {
            get
            {
                if (base.Exists(p => p.Type.ToLower() == "next"))
                    return base.Find(p => p.Type.ToLower() == "next").Url;
                // If there is no "Next" link then return the current page
                return Self;
            }

            set
            {
                if (!base.Exists(p => p.Type.ToLower() == "next"))
                {
                    var link = new Link()
                    {
                        Type = "next",
                        Url = value
                    };
                    base.Add(link);
                }
                else
                {
                    base.Find(p => p.Type.ToLower() == "next").Url = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the url for the link to the RottenTomatoes website for this movie
        /// </summary>
        public string Previous
        {
            get
            {
                if (base.Exists(p => p.Type.ToLower() == "prev"))
                    return base.Find(p => p.Type.ToLower() == "prev").Url;
                // If there is no "Previous" link then return the current page
                return Self;
            }

            set
            {
                if (!base.Exists(p => p.Type.ToLower() == "prev"))
                {
                    var link = new Link()
                    {
                        Type = "prev",
                        Url = value
                    };
                    base.Add(link);
                }
                else
                {
                    base.Find(p => p.Type.ToLower() == "prev").Url = value;
                }
            }
        }
    }
}
