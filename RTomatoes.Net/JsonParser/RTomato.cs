using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RTomatoes.Net.Entities;
using System.Net;
using System.Xml;
using System.Web.Script.Serialization;

namespace RTomatoes.Net
{
    public class RTomato : ICollection<Movie>
    {
        private static RTomato rtomato = null;
        private static string API_KEY { get; set; }
        private List<Movie> Movies { get; set; }

        private RTomato()
        {
            Movies = new List<Movie>();
        }

        private RTomato(string ApiKey)
        {
            API_KEY = ApiKey;
            Movies = new List<Movie>();
        }

        public static RTomato Initialize(string api_key)
        {
            if (rtomato == null)
                rtomato = new RTomato(api_key);

            return rtomato;
        }

        public MoviesSearch TitleSearch(string title)
        {
            if (rtomato == null)
                throw new InvalidOperationException("RTomato has not been initialized. Initialize with the 'Initialize()' method.");

            if (string.IsNullOrEmpty(title)) return null;

            int index = 1;
            string url = string.Format(API_URLS.MOVIE_SEARCH, API_KEY, title, 50, index++);
            var results = JsonToObject<MoviesSearch>(url);

            while (true)
            {
                url = string.Format(API_URLS.MOVIE_SEARCH, API_KEY, title, 50, index++);                
                results.movies.AddRange(JsonToObject<MoviesSearch>(url).movies);

                if (results.movies.Count >= 100
                    || results.movies.Count >= results.total)
                {
                    break;
                }
            }

            return results;
        }

        public Movie MovieInfo(int id)
        {
            if (rtomato == null)
                throw new InvalidOperationException("RTomato has not been initialized. Initialize with the 'Initialize()' method.");

            if (id == 0) 
                throw new ArgumentException("Movie ID must be greater than 0");

            string url = string.Format(API_URLS.MOVIE_INDIVIDUAL_INFORMATION, API_KEY, id);
            return JsonToObject<Movie>(url);
        }

        private T JsonToObject<T>(string url)
        {
            using (var client = new WebClient())
            {
                string json = client.DownloadString(url).Trim();
                var parser = new JavaScriptSerializer(new SimpleTypeResolver());

                return parser.Deserialize<T>(json);
            }
        }

        #region ICollection<Movie> Members

        public void Add(Movie item)
        {
            Movies.Add(item);
        }

        public void Clear()
        {
            Movies.Clear();
        }

        public bool Contains(Movie item)
        {
            return Movies.Contains(item);
        }

        public void CopyTo(Movie[] array, int arrayIndex)
        {
            Movies.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return Movies.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(Movie item)
        {
            return Movies.Remove(item);
        }

        #endregion

        #region IEnumerable<Movie> Members

        public IEnumerator<Movie> GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Movies.GetEnumerator();
        }

        #endregion
    }
}
