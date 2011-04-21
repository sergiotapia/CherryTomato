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

            
            //Example 4: Using the Selected Index Changed Event of the MovieSearchResults class
            MovieSearchResultEventDemonstration();

            Console.ReadKey();
        }

        private static void MovieSearchResultEventDemonstration()
        {
            var tomato = new Tomato(ApiKey);
            var results = tomato.FindMovieByQuery("tank");
            results.SelectedIndexChanged += new SelectedIndexChangedEventHandler(results_SelectedIndexChanged);

            Console.WriteLine("The Currently Selected Movie: " + results.SelectedValue.Title);
            Console.WriteLine("The First 5 Cast Members: ");
            GetTopFiveCastMembers(results.SelectedValue.RottenTomatoesId);

            Console.WriteLine();

            results.SelectedIndex = 3;
        }

        static void results_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Inside the Index Changed Event\n");
            var results = sender as MovieSearchResults;

            Console.WriteLine("The New Selected Movie: " + results.SelectedValue.Title);
            GetTopFiveCastMembers(results.SelectedValue.RottenTomatoesId);
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

            var results = tomato.FindMovieByQuery("The Incredible Hulk");
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
