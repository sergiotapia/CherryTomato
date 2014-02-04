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
        // GET: /Default1/
        private string ApiKey = ConfigurationManager.AppSettings["ApiKey"];

        public ActionResult Index(int? id)
        {
            var tomato = new Tomato(ApiKey);
            Movie movie = null;
            
            if (id.HasValue) 
                movie = tomato.FindMovieById(id.Value);

            return View("Index", movie);
        }

        //
        // GET: /Default1/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

     
        public ActionResult MovieSearch(string title, string p, int? total)
        {
            var tomato = new Tomato(ApiKey);
            MovieSearchResults results = null;

            if (!string.IsNullOrEmpty(title))
            {
                int limit = 3;
                int index = 0;

                switch (p)
                {
                    case "next":
                        if (total > (index * limit))
                            index++;
                        break;
                    case "prev":
                        if (index > 0)
                            index--;
                        break;
                    default:
                        break;
                }

                results = tomato.FindMovieByQuery(title, limit, index);
            }
            return View("MovieSearch", results);
        }
    }
}
