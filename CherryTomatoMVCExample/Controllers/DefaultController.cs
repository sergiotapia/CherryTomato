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
        public MovieSearchResults Results
        {
            get
            {
                object temp = Session["Results"];
                return temp == null ? null : (MovieSearchResults)temp;
            }
            set { Session["Results"] = value; }
        }

        // GET: /Default1/
        private string ApiKey = ConfigurationManager.AppSettings["ApiKey"];

        private Tomato _tomato;
        public Tomato tomato
        {
            get
            {
                if (_tomato == null) _tomato = new Tomato(ApiKey);
                return _tomato;
            }
        }


        public ActionResult Index()
        {
            return View("Index");
        }
        
        
        public ActionResult MovieSearch(string title, string p, int? total)
        {
            int limit = 5;

            if (!string.IsNullOrEmpty(p))
            {
                switch (p)
                {
                    case "next":
                        Results = tomato.FindMoviesByUrl(Results.Links.Next);
                        break;
                    case "prev":
                        Results = tomato.FindMoviesByUrl(Results.Links.Previous);
                        break;
                    default:
                        Results = null;
                        break;
                }
            }
            
            else if (!string.IsNullOrEmpty(title) && p == null)
            {
                Results = tomato.FindMoviesByQuery(query: title, pageLimit: limit, page: 1);
            }
            return View("MovieSearch", Results);
        }


        public ActionResult MovieInfo(int? id, string query)
        {
            if (id.HasValue)
            {
                var tomato = new Tomato(ApiKey);
                var movie = tomato.FindMovieById(id.Value);

                return View("MovieInfo", movie);
            }
            else
            {
               return Redirect("Index");
            }
        }
    }
}
