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
            string apiKey = ConfigurationManager.AppSettings["ApiKey"];

            //A Tomato is the main object that will allow you to access RottenTomatoes information. 
            //Be sure to provide it with your API key in String format.
            var tomatoe = new Tomato(apiKey);

            //Example 1: Finding a movie by it's RottenTomatoes internal ID number.
            Movie movie = tomatoe.FindMovieById(9818);


            //The Movie object, contains all sorts of goodies you might want to know about a movie.
            Console.WriteLine(movie.Title);
            Console.WriteLine(movie.Year);
            foreach (var rating in movie.Ratings)
            {
                Console.WriteLine("{0} Rating: {1}", rating.Type, rating.Score);
            }

            foreach (var castMember in movie.Cast)
            {
                Console.WriteLine("{0} as {1}", castMember.Name, castMember.Characters[0]);
            }

            //Example 2: Finding a movie by it's name. 
            string searchTerm = "Gone With The Wind";
            var results = tomatoe.FindMovieByQuery(searchTerm);
            
            Console.WriteLine("Searching with query: [" + searchTerm + "]");
            Console.WriteLine("Found {0} results.", results.ResultCount);
            foreach (var result in results)
            {
                Console.WriteLine("ID: {0} \n Title: {1} \n Runtime: {2}\n", result.RottenTomatoesId, result.Title, result.Runtime);
            }

            //Normally, the first result will be the one you're looking for.
            var foundMovie = tomatoe.FindMovieById(results.ElementAt(0).RottenTomatoesId);
            Console.WriteLine(foundMovie.Title);
            Console.WriteLine(foundMovie.Synopsis);

            Console.WriteLine();

            // Testing with Gone With The Wind ID: 9818
            var Cast = (from c in tomatoe.GetCastByMovieID(9818).AsParallel()
                        where c.Name.Split(' ')[1].Contains("gh")
                        orderby c.Name descending
                        select c);

            foreach (var actor in Cast)
            {
                Console.WriteLine("Name: {0}", actor.Name);
            }

            Console.WriteLine();
            Console.WriteLine("Fetch movie from list by it's id: ");
            Console.WriteLine(results["9818"].Title);
        }

    }
}
