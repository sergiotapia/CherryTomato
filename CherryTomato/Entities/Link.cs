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

    public class LinkCollection : List<Link>
    {
        public LinkCollection() : base() { }

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
}
