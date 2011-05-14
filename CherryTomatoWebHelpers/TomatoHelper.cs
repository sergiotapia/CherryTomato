using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.WebPages;
using RTomatoes.Net;
using RTomatoes.Net.Entities;

public static class TomatoHelper
{
    private static RTomato tomato { get; set; }
    private static string ApiKey { get; set; }

    public static void Initialize(string apikey)
    {
        ApiKey = apikey;
        if (tomato == null)
            tomato = RTomato.Initialize(apikey); 
    }


    public static HelperResult TitleSearch(object divAttributes = null)
    {
        VerifyInitialization();
        string q = HelperPage.Request["q"];
        List<Movie> movies;
        
        var page = new Page();
        string url = page.ClientScript.GetWebResourceUrl(typeof(TomatoHelper), "CherryTomato.WebHelpers.images.RottenTomatoBG.png");

        if (!string.IsNullOrEmpty(q))
        {
            var Results = tomato.TitleSearch(q);
            if (Results.movies.Count > 0)
            {
                movies = Results.movies;
                
                return new HelperResult(w =>
                    {                        
                        var grid = new WebGrid(movies, rowsPerPage: 5, canPage: false);
                        w.WriteLine("<form action='' method='post'>");
                        w.WriteLine("<div style='width: 242px;height:312px;background: url(\"" + url + "\") no-repeat'>");
                        w.WriteLine("<input width='100' type='text' name='q' style='border: transparent; position: relative; top: 16%; left: 13%;' />");
                        w.WriteLine("<input type='submit' name='btnSubmit' text='' style='width:25px; height: 25px; position: relative; top: 16%; left: 83%;");
                        w.WriteLine("<div style='border-top: solid gray 1px; overflow: auto; height: 65%;position: relative; top: 20%; width: 220px; margin: 0 auto;'>");
                        
                        w.WriteLine( HttpUtility.HtmlDecode(
                            grid.GetHtml(
                                tableStyle: "MovieSearchTable",
                                rowStyle: "MovieSearchRow",
                                alternatingRowStyle: "MovieSearchAlt",
                                columns: grid.Columns(
                                    grid.Column("Title", "Movie",
                                    format: (item) =>
                                        {
                                            return "<h4 style=\"color: red;margin: 0em; width: 15em;\">" +
                                                item.Title + "</h4><h5 style='margin:0;'>(" + item.Year +
                                                ")</h5><a href=\"~/../MovieInfo/" + item.id + "\"><img src=\"" +
                                                item.posters.original +
                                                "\" width='91' height='121' alt='image' /></a>";
                                        })
                                    )
                            ).ToHtmlString()));
                        w.WriteLine("</div>");
                        w.WriteLine("</div>");
                        w.WriteLine("</form>");
                    }
                );
            }
        }

        return new HelperResult(w =>
            {
                w.WriteLine("<form action='' method='post'>");
                w.WriteLine("<div style='width: 242px;height:312px;background: url(\"" + url + "\") no-repeat'>");
                w.WriteLine("<input width='100' type='text' name='q' style='border: transparent;position: relative; top: 15%; left: 13%;' />");
                w.WriteLine("<input type='submit' value='' style='background-color: transparent;width:25px; height: 26px; position: relative; top: 14%; left: 16%;"); 
                w.WriteLine("<div style='border-top: solid gray 1px; overflow: auto; height: 65%;position: relative; top: 20%; width: 220px; margin: .1em auto;'>");
                w.WriteLine("No Data Found");
                w.WriteLine("</div>");
                w.WriteLine("</div>");
                w.WriteLine("</form>");
            });
    }


    /// <summary>
    /// Verify the Api Key has a value and that the Tomato object is not null
    /// </summary>
    private static void VerifyInitialization()
    {
        if (tomato == null || string.IsNullOrEmpty(ApiKey))
        {
            throw new InvalidOperationException("TomatoHelper has not been initialized with the developer's Rotten Tomato API key");
        }
    }


    
}
