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


        public int index
        {
            get
            {
                object temp = Session["index"];
                return temp == null ? 1 : (int)temp;
            }
            set
            {
                Session["index"] = value;
            }
        }
        
        
        public ActionResult MovieSearch(string title, string p, int? total)
        {
            var tomato = new Tomato(ApiKey);
            MovieSearchResults results = null;

            if (!string.IsNullOrEmpty(title))
            {
                int limit = 5;

                switch (p)
                {
                    case "next":
                        if (total >= (index * limit))
                            index++;
                        break;
                    case "prev":
                        if (index > 1)
                            index--;
                        break;
                    default:
                        break;
                }

                results = tomato.FindMovieByQuery(query: title, pageLimit: limit, page: index);
            }
            return View("MovieSearch", results);
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
