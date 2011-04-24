using System;
using System.Configuration;
using CherryTomato.Entities;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace CherryTomato.Examples
{
    class Program
    {
        private static string ApiKey = ConfigurationManager.AppSettings["ApiKey"];

        static void Main(string[] args)
        {
            //Example 1: Finding a movie by it's ID number.
            //FindingMovieByIdNumber();

            //Example 2: Searching for a movie by it's name.
            //FindingMovieByName();


            //Example 3: Get the full cast for a movie by the movie id.
            //GetMovieCast();

            //Example 4: Displaying the current box office charts.
            //DisplayCurrentBoxOffice();

            //Example 5: Displaying the current movies in Theaters.
            //DisplayMoviesInTheaters();

            //Example 6: Displaying opening movies.
            //DisplayOpeningMovies();

            //Example 7: Displaying upcoming movies.
            //DisplayUpcomingMovies();
            
            
            //Example 8: Using the Selected Index Changed Event of the MovieSearchResults class
            MovieSearchResultEventDemonstration();

            Console.ReadKey();
        }

        private static void DisplayUpcomingMovies()
        {
            //A Tomato is the main object that will allow you to access RottenTomatoes information. 
            //Be sure to provide it with your API key in String format.
            var tomato = new Tomato(ApiKey);

            //The movies are automatically ordered according to their gross at the box-office.
            //Unfortunately the JSON API doesn't offer the gross (money earned) only their relative position
            //on the charts.
            var movies = tomato.FindUpcomingMoviesList();

            foreach (var movie in movies)
            {
                Console.WriteLine(movie.Title);
            }
        }

        private static void DisplayOpeningMovies()
        {
            //A Tomato is the main object that will allow you to access RottenTomatoes information. 
            //Be sure to provide it with your API key in String format.
            var tomato = new Tomato(ApiKey);

            //The movies are automatically ordered according to their gross at the box-office.
            //Unfortunately the JSON API doesn't offer the gross (money earned) only their relative position
            //on the charts.
            var movies = tomato.FindOpeningMoviesList();

            foreach (var movie in movies)
            {
                Console.WriteLine(movie.Title);
            }
        }

        private static void DisplayMoviesInTheaters()
        {
            //A Tomato is the main object that will allow you to access RottenTomatoes information. 
            //Be sure to provide it with your API key in String format.
            var tomato = new Tomato(ApiKey);

            //The movies are automatically ordered according to their gross at the box-office.
            //Unfortunately the JSON API doesn't offer the gross (money earned) only their relative position
            //on the charts.
            var movies = tomato.FindMoviesInTheaterList();

            foreach (var movie in movies)
            {
                Console.WriteLine(movie.Title);
            }
        }

        private static void DisplayCurrentBoxOffice()
        {
            //A Tomato is the main object that will allow you to access RottenTomatoes information. 
            //Be sure to provide it with your API key in String format.
            var tomato = new Tomato(ApiKey);

            //The movies are automatically ordered according to their gross at the box-office.
            //Unfortunately the JSON API doesn't offer the gross (money earned) only their relative position
            //on the charts.
            var movies = tomato.FindBoxOfficeList();

            foreach (var movie in movies)
            {
                Console.WriteLine(movie.Title);
            }
        }

        private static void MovieSearchResultEventDemonstration()
        {
            var tomato = new Tomato(ApiKey);
            var results = tomato.FindMoviesByQuery("harlem");
            results.SelectedIndexChanged += new SelectedIndexChangedEventHandler(results_SelectedIndexChanged);

            Console.WriteLine("The Currently Selected Movie: " + results.SelectedValue.Title);
            Console.WriteLine("The First 5 Cast Members: ");
            GetTopFiveCastMembers(results.SelectedValue.RottenTomatoesId);

            Console.WriteLine();

            results.SelectedIndex = 0;
        }

        static void results_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Inside the Index Changed Event\n");
            var results = sender as MovieSearchResults;

            Console.WriteLine("The New Selected Movie: " + results.SelectedValue.Title);
            GetTopFiveCastMembers(results.SelectedValue.RottenTomatoesId);

            Console.WriteLine();
            Console.WriteLine("Getting Detailed Info On " + results.SelectedValue.Title);
            Console.WriteLine();

            Movie movie = results.GetSelectedMovieDetailedInfo(ApiKey);
            Console.WriteLine("Title: " + movie.Title);
            Console.WriteLine("Year :" + movie.Year);
            Console.WriteLine("Synopsis: " + movie.Synopsis);
            
            movie.Genres.ForEach(g =>
            {
                Console.WriteLine("Genres: " + g);
            });

            movie.Directors.ForEach(d => { Console.WriteLine("Directors :" + d); });

            movie.Cast.ForEach(c =>
            {
                Console.WriteLine("Cast: " + c.Name);
                c.Characters.ForEach(ch => { Console.WriteLine("Characters: " + ch); });
            });
        }

        private static void GetTopFiveCastMembers(int movieID)
        {
            var tomato = new Tomato(ApiKey);

            //Retreive the cast for a movie based on the movie's ID
            var Cast = tomato.GetFullCastByMovieID(movieID).Take(5);
            Console.WriteLine();

            foreach (var member in Cast)
            {
                Console.WriteLine("Cast Member: " + member.Name);
            }
        }

        /// <summary>
        /// Retreives the movie cast for Gone With The Wind
        /// </summary>
        private static void GetMovieCast()
        {
            //A Tomato is the main object that will allow you to access RottenTomatoes information. 
            //Be sure to provide it with your API key in String format.
            var tomato = new Tomato(ApiKey);

            //Retreive the cast for a movie based on the movie's ID
            var Cast = tomato.GetFullCastByMovieID(9818);

            foreach (var castmember in Cast)
            {
                Console.WriteLine("Cast Member: " + castmember.Name);
            }
        }

        private static void FindingMovieByName()
        {
            //A Tomato is the main object that will allow you to access RottenTomatoes information. 
            //Be sure to provide it with your API key in String format.
            var tomato = new Tomato(ApiKey);

            var results = tomato.FindMoviesByQuery("The Incredible Hulk");
            foreach (var movieSearchResult in results)
            {
                Console.WriteLine(movieSearchResult.Title);
            }
        }

        private static void FindingMovieByIdNumber()
        {
            //A Tomato is the main object that will allow you to access RottenTomatoes information. 
            //Be sure to provide it with your API key in String format.
            var tomato = new Tomato(ApiKey);

            //Finding a movie by it's RottenTomatoes internal ID number.
            Movie movie = tomato.FindMovieById(9818);

            //The Movie object, contains all sorts of goodies you might want to know about a movie.
            Console.WriteLine(movie.Title);
            Console.WriteLine(movie.Year);
        }
    }
}
