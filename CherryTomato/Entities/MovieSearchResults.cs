using System;
using System.Collections.Generic;
using System.Linq;

namespace CherryTomato.Entities
{
    public delegate void SelectedIndexChangedEventHandler(object sender, EventArgs e);

    public class MovieSearchResults : ICollection<Movie>
    {
        #region Public Properties
        /// <summary>
        /// Search Query used to obtain movie results
        /// </summary>
        public string SearchQuery { get; set; }

        /// <summary>
        /// Total Number Of Results
        /// </summary>
        public int ResultCount { get; set; }

        /// <summary>
        /// Gets or sets the index of the currently selected index
        /// </summary>
        private int selectedIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                if (selectedIndex != value)
                {
                    selectedIndex = value;
                    OnSelectedIndexChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets the currently selected item
        /// </summary>
        public Movie SelectedValue
        {
            get { return Movies.ElementAt(SelectedIndex); }
        }

        /// <summary>
        /// Get the next page of results
        /// </summary>
        public string NextPage
        {
            get
            {
                string url = this.Links.Next;

                if (string.IsNullOrEmpty(url))
                    return this.CurrentPage;

                return url;
            }
        }

        /// <summary>
        /// Get the previous page of results, returns the current page when their are no previous pages
        /// </summary>
        public string PreviousPage
        {
            get
            {
                string url = this.Links.Previous;

                if (string.IsNullOrEmpty(url))
                    return this.CurrentPage;

                return url;
            }
        }

        /// <summary>
        /// Get the previous page of results, returns the current page when their are no previous pages
        /// </summary>
        public string CurrentPage
        {
            get
            {
                return this.Links.Self;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MovieSearchResults(string apikey=null)
        {
            ResultCount = 0;
            Movies = new List<Movie>();
            selectedIndex = 0;
            Links = new MovieSearchLinkCollection();
        } 
        #endregion

        #region Events
        public event SelectedIndexChangedEventHandler SelectedIndexChanged;
        public virtual void OnSelectedIndexChanged(EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, e);
        } 
        #endregion

        #region Collections
        /// <summary>
        /// List Of Movie Results
        /// </summary>
        private List<Movie> Movies { get; set; }

        /// <summary>
        /// Collection of Link objects
        /// </summary>
        public MovieSearchLinkCollection Links { get; set; } 
        #endregion

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

        internal void AddRange(MovieSearchResults movieSearchResults)
        {
            foreach (var item in movieSearchResults.Movies)
            {
                if (Movies.All(i=>i.RottenTomatoesId != item.RottenTomatoesId))
                    Movies.Add(item);
            }        
        }
    }
}
