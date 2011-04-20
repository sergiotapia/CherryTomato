using System;
using System.Collections.Generic;
using System.Linq;

namespace CherryTomato.Entities
{
    public delegate void SelectedIndexChangedEventHandler(object sender, EventArgs e);

    public class MovieSearchResults : ICollection<Movie>
    {
        public event SelectedIndexChangedEventHandler SelectedIndexChanged;
        public virtual void OnSelectedIndexChanged(EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, e);
        }

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
            selectedIndex = 0;
        }

        /// <summary>
        /// Gets or sets the index of the currently selected index
        /// </summary>
        private int selectedIndex;
        public int SelectedIndex 
        {
            get { return selectedIndex; } 
            set
            {
                selectedIndex = value;
                OnSelectedIndexChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets the currently selected item
        /// </summary>
        public Movie SelectedValue 
        {
            get { return Movies.ElementAt(SelectedIndex); } 
        }

        #region LIST METHODS
        /// <summary>
        /// List indexer
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Movie this[int index]
        {
            get { return Movies[index]; }
            set { Movies[index] = value; }
        }

        /// <summary>
        /// Sorts the Movies by their ID
        /// </summary>
        public void Sort()
        {
            Movies.Sort();
        }

        /// <summary>
        /// Sorts the Movies by their ID
        /// </summary>
        /// <param name="comparer">Comparer to implement for sorting</param>
        public void Sort(IComparer<Movie> comparer)
        {
            Movies.Sort(comparer);
        }
        #endregion

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
