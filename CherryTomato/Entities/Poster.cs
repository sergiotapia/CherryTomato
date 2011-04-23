using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CherryTomato.Entities
{
    public class Poster
    {
        public string Type { get; set; }
        public string Url { get; set; }
    }

    public class PosterCollection : List<Poster>
    {
        public PosterCollection() : base() { }

        /// <summary>
        /// Gets and sets the url for the thumbnail poster
        /// </summary>
        public string ThumbnailPoster
        {
            get
            {
                if (base.Exists(p => p.Type.ToLower() == "thumbnail"))
                    return base.Find(p => p.Type.ToLower() == "thumbnail").Url;
                return null;
            }

            set
            {
                if (!base.Exists(p => p.Type.ToLower() == "thumbnail"))
                {
                    var poster = new Poster()
                    {
                        Type = "thumbnail",
                        Url = value
                    };
                    base.Add(poster);
                }
                else
                {
                    base.Find(p => p.Type.ToLower() == "thumbnail").Url = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the url for the profile poster
        /// </summary>
        public string ProfilePoster
        {
            get
            {
                if (base.Exists(p => p.Type.ToLower() == "profile"))
                    return base.Find(p => p.Type.ToLower() == "profile").Url;
                return null;
            }

            set
            {
                if (!base.Exists(p => p.Type.ToLower() == "profile"))
                {
                    var poster = new Poster()
                    {
                        Type = "profile",
                        Url = value
                    };
                    base.Add(poster);
                }
                else
                {
                    base.Find(p => p.Type.ToLower() == "profile").Url = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the url for the detailed poster
        /// </summary>
        public string DetailedPoster
        {
            get
            {
                if (base.Exists(p => p.Type.ToLower() == "detailed"))
                    return base.Find(p => p.Type.ToLower() == "detailed").Url;
                return null;
            }

            set
            {
                if (!base.Exists(p => p.Type.ToLower() == "detailed"))
                {
                    var poster = new Poster()
                    {
                        Type = "detailed",
                        Url = value
                    };
                    base.Add(poster);
                }
                else
                {
                    base.Find(p => p.Type.ToLower() == "detailed").Url = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the url for the original poster
        /// </summary>
        public string OriginalPoster
        {
            get
            {
                if (base.Exists(p => p.Type.ToLower() == "original"))
                    return base.Find(p => p.Type.ToLower() == "original").Url;
                return null;
            }

            set
            {
                if (!base.Exists(p => p.Type.ToLower() == "original"))
                {
                    var poster = new Poster()
                    {
                        Type = "original",
                        Url = value
                    };
                    base.Add(poster);
                }
                else
                {
                    base.Find(p => p.Type.ToLower() == "original").Url = value;
                }
            }
        }
    }
}
