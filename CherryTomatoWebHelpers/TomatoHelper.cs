using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.WebPages;
using System.Web.Helpers;
using System.Web.WebPages.Html;
using CherryTomato;
using System.Web;
using CherryTomato.Entities;

public class TomatoHelper
{
    public Tomato tomato { get; private set; }
    private static TomatoHelper tHelper { get; set; }
    private static string ApiKey { get; set; }

    public static TomatoHelper Initialize(string apikey)
    {
        ApiKey = apikey;
        if (tHelper == null)
            tHelper = new TomatoHelper();
        return tHelper;
    }

    private TomatoHelper() { if (tomato == null) tomato = new Tomato(ApiKey); }

    public HelperResult MovieSearchByTitle(string query, int page = 0, int limit = 5, string movieViewUrl=null,
        string tableStyle = null, string headerStyle = null, string footerStyle = null,
        string rowStyle = null, string alternatingRowStyle = null, object divAttributes = null)
    {
        VerifyInitialization();
        string q = HelperPage.Request["title"];
        string p = HelperPage.Request["p"];
        MovieSearchResults movies;

        if (q != tomato.SearchQuery)
            movies = tomato.FindMoviesByQuery(query);

        else
        {
            switch (p)
            {
                case "prev":
                    movies = tomato.PreviousPage;
                    break;
                case "next":
                    movies = tomato.NextPage;
                    break;
                default:
                    movies = tomato.FindMoviesByQuery(query);
                    break;
            }
        }

        var result = new HelperResult(w =>
            {
                w.Write("<div style='margin: .5em; width: 20em;'>" +
                    "<a href='" + HelperPage.UrlData[0] + "?title="+ q + 
                    "&p=prev' alt='previous'>Previous</a>&nbsp;" +
                    "<a href='" + HelperPage.UrlData[0] + "?title="+ q +
                    "&p=next' alt='previous'>Next</a></div>");

                w.Write("<table>");
                foreach (var item in movies)
	            {
                    string synop = item.Synopsis == "" ? "Not Available" : item.Synopsis;
                    w.Write("<tr style='margin: .2em; border-bottom: 1px solid gray'>");
                    
                    w.Write("<td><a style='margin: .5em; display: inline-block' href='" + HelperPage.Href(movieViewUrl, 
                        item.RottenTomatoesId) + 
                        "' alt='" + item.Title + "'><img title='" +item.Title+ "' src='" + item.Posters.OriginalPoster + 
                        "' alt='" + item.Title + "' style='width:91px; height:121px;' /></a></td>");

                    w.Write("<td style='width:20em;'><div style='width:20em; color: red; font-weight: bold;'>" + item.Title + 
                        "</div><div style='width:20em; height: 90px; overflow: auto; border-top: 1px solid gray;'>" +
                        synop + "</div></td>");

                    w.Write("</tr>");
	            }
                w.Write("</table>");
            }
        );

        return result;
    }


    /// <summary>
    /// Verify the Api Key has a value and that the Tomato object is not null
    /// </summary>
    private static void VerifyInitialization()
    {
        if (tHelper.tomato == null || string.IsNullOrEmpty(ApiKey))
        {
            throw new InvalidOperationException("TomatoHelper has not been initialized with the developer's Rotten Tomato API key");
        }
    }


    
}
