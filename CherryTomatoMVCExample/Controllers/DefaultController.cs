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
        private string ApiKey = ConfigurationManager.AppSettings["ApiKey"];

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

        // GET: /Index
        public ActionResult Index()
        {
            return View("Index");
        }
        
        // GET: /MovieSearch
        public ActionResult MovieSearch(string title, string p)
        {
            int limit = 5;

            switch (p)
            {
                case "next":
                    return View("MovieSearch", tomato.NextPage);
                case "prev":
                    return View("MovieSearch", tomato.PreviousPage);
                default:
                    tomato = new Tomato(ApiKey);
                    return (string.IsNullOrEmpty(title) ?
                        View("MovieSearch") : View("MovieSearch", tomato.FindMoviesByQuery(title, limit)));
            }
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
