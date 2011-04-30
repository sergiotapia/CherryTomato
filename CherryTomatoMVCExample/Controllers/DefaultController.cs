using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CherryTomato;
using System.Configuration;
using CherryTomato.Entities;

namespace CherryTomatoMVCExample.Controllers
{
    public class DefaultController : Controller
    {
        public string ApiKey = ConfigurationManager.AppSettings["ApiKey"];

        // GET: The MovieSearchResult object
        public Tomato tomato
        {
            get
            {
                object temp = Session["Tomato"];
                return temp == null ? null : (Tomato)temp;
            }

            set
            {
                Session["Tomato"] = value;
            }
        }

        public MovieSearchResults Results
        {
            get
            {
                object temp = Session["Results"];
                return (temp == null) ? null : (MovieSearchResults)temp;
            }
            set
            {
                Session["Results"] = value;
            }
        }

        // GET: /Index
        public ActionResult Index()
        {
            return View("Index");
        }
        
        // GET: /MovieSearch
        public ActionResult MovieSearch(string q)
        {
            if (tomato == null)
                tomato = new CherryTomato.Tomato(System.Configuration.ConfigurationManager.AppSettings["ApiKey"]);

            if ((Results == null && !string.IsNullOrEmpty(q)) || 
                (!string.IsNullOrEmpty(q) && Results.SearchQuery != q))
            {
                Results = tomato.FindMoviesByQuery(q);
            }

            return View("MovieSearch", Results);
        }


        public ActionResult MovieInfo(int? id, string query)
        {
            if (id.HasValue)
            {
                var movie = tomato.FindMovieById(id.Value);
                return View("MovieInfo", movie);
            }
            else
            {
               return Redirect("Index");
            }
        }


        public ActionResult BoxOffice()
        {
            if (tomato == null) tomato = new Tomato(ApiKey);
            var movies = tomato.FindBoxOfficeList();
            return View("BoxOffice", movies);
        }


        public ActionResult UpcomingReleases()
        {
            if (tomato == null) tomato = new Tomato(ApiKey);
            var movies = tomato.FindUpcomingMoviesList();
            return View("UpcomingReleases", movies);
        }


        public ActionResult InTheaters()
        {
            if (tomato == null) tomato = new Tomato(ApiKey);
            var movies = tomato.FindMoviesInTheaterList();
            return View("InTheaters", movies);
        }
    }
}
