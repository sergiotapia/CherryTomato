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
        static void Main(string[] args)
        {
            //Example 1: Finding a movie by it's ID number.
            FindingMovieByIdNumber();


            //Example 2: Searching for a movie by it's name.
            FindingMovieByName();
            

            Console.ReadKey();
        }

        private static void FindingMovieByName()
        {
            string apiKey = ConfigurationManager.AppSettings["ApiKey"];

            //A Tomato is the main object that will allow you to access RottenTomatoes information. 
            //Be sure to provide it with your API key in String format.
            var tomato = new Tomato(apiKey);

            var results = tomato.FindMovieByQuery("The Incredible Hulk");
            foreach (var movieSearchResult in results)
            {
                Console.WriteLine(movieSearchResult.Title);
            }
        }

        private static void FindingMovieByIdNumber()
        {
            string apiKey = ConfigurationManager.AppSettings["ApiKey"];

            //A Tomato is the main object that will allow you to access RottenTomatoes information. 
            //Be sure to provide it with your API key in String format.
            var tomato = new Tomato(apiKey);

            //Finding a movie by it's RottenTomatoes internal ID number.
            Movie movie = tomato.FindMovieById(9818);

            //The Movie object, contains all sorts of goodies you might want to know about a movie.
            Console.WriteLine(movie.Title);
            Console.WriteLine(movie.Year);
        }
    }
}
