using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CherryTomato;
using System.Configuration;
using CherryTomato.Entities;
using RTomatoes.Net;
using RTomatoes.Net.Entities;

namespace CherryTomatoMVCExample.Controllers
{
    public class DefaultController : Controller
    {
        public string ApiKey = ConfigurationManager.AppSettings["ApiKey"];

        // GET: The MovieSearchResult object
        public RTomato tomato
        {
            get
            {
                object temp = Session["Tomato"];
                return temp == null ? null : (RTomato)temp;
            }

            set
            {
                Session["Tomato"] = value;
            }
        }

        public MoviesSearch Results
        {
            get
            {
                object temp = Session["Results"];
                return (temp == null) ? null : (MoviesSearch)temp;
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
            return View("MovieSearch");
        }


        public ActionResult MovieInfo(int? id, string query)
        {
            if (id.HasValue)
            {
                var movie = tomato.MovieInfo(id.Value);
                return View("MovieInfo", movie);
            }
            else
            {
               return Redirect("Index");
            }
        }


        public ActionResult BoxOffice()
        {
            //if (tomato == null) tomato = new Tomato(ApiKey);
            //var movies = tomato.FindBoxOfficeList();
            return View("BoxOffice");
        }


        public ActionResult UpcomingReleases()
        {
            //if (tomato == null) tomato = new Tomato(ApiKey);
            //var movies = tomato.FindUpcomingMoviesList();
            return View("UpcomingReleases");
        }


        public ActionResult InTheaters()
        {
            //if (tomato == null) tomato = new Tomato(ApiKey);
            //var movies = tomato.FindMoviesInTheaterList();
            return View("InTheaters");
        }
    }
}
