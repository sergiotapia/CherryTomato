using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CherryTomato.Entities
{
    public class MovieSearchResults : ICollection<Movie>
    {
        /// <summary>
        /// Total Number Of Results
        /// </summary>
        public int ResultCount { get; set; }

        /// <summary>
        /// List Of Movie Results
        /// </summary>
        private List<Movie> Movies { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MovieSearchResults()
        {
            ResultCount = 0;
            Movies = new List<Movie>();
        }


        public Movie this[int index]
        {
            get { return Movies[index]; }
            set { Movies[index] = value; }
        }

        public Movie this[string movieID]
        {
            get { return Movies.Where(m => m.RottenTomatoesId.ToString() == movieID).FirstOrDefault(); }
            set
            {
                Movie temp = Movies.Where(m => m.RottenTomatoesId.ToString() == movieID).FirstOrDefault();
                if (temp != null) temp = value;
                else throw new InvalidOperationException("Object does not exist");
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
            get { return false; }
        }

        public bool Remove(Movie item)
        {
            return Movies.Remove(item);
        }

        #endregion

        #region IEnumerable<Movie> Members

        public IEnumerator<Movie> GetEnumerator()
        {
            return Movies.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
