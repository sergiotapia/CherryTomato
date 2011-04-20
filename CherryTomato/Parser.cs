using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Entities;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace CherryTomato
{
    public static class Parser
    {
        /// <summary>
        /// Parse a JSON string representing a Movie item
        /// </summary>
        /// <param name="json">The JSON string to be parsed</param>
        /// <returns>Movie object</returns>
        public static Movie ParseMovie(string json)
        {
            JToken jToken = JToken.FromObject(JObject.Parse(json));
            Movie movie = new Movie();

            movie.RottenTomatoesId = Convert.ToInt32(jToken["id"].ToString().Replace("\"", ""));
            movie.Title = (string)jToken["title"];
            movie.Year = (int)jToken["year"];
            movie.MpaaRating = (string)jToken["mpaa_rating"];

            // jToken["runtime"] is occasionally null or has the value of "" in the JSON string.
            // Here we remove any double quotes and if the string is not empty we convert 
            // it to an int and set the value of movie.Runtime.
            // If jToken["runtime"] has no value then movie.Runtime will be null
            string runtime = jToken["runtime"].ToString().Replace("\"", "");
            if (!string.IsNullOrEmpty(runtime))
                movie.Runtime = Convert.ToInt32(runtime);

            movie.Synopsis = (string)jToken["synopsis"];

            var directors = (JArray)jToken["abridged_directors"];

            if (directors != null)
            {
                foreach (var director in directors)
                {
                    movie.Directors.Add((string)director["name"]);
                }
            }

            var genres = (JArray)jToken["genres"];
            if (genres != null)
            {
                foreach (var genre in genres)
                {
                    movie.Genres.Add((string)genre);
                }
            }

            var castMembers = (JArray)jToken["abridged_cast"];
            if (castMembers != null)
            {
                foreach (var castMember in castMembers)
                {
                    CastMember member = new CastMember();
                    member.Name = (string)castMember["name"];
                    var characters = (JArray)castMember["characters"];
                    if (characters != null)
                    {
                        foreach (var character in characters)
                        {
                            member.Characters.Add((string)character);
                        }
                        movie.Cast.Add(member);
                    }
                }
            }

            var links = (JObject)jToken["links"];
            if (links != null)
            {
                foreach (var link in links)
                {
                    Link newLink = new Link();
                    newLink.Type = (string)link.Key;
                    newLink.Url = (string)link.Value;
                    movie.Links.Add(newLink);
                }
            }

            var posters = (JObject)jToken["posters"];
            if (posters != null)
            {
                foreach (var poster in posters)
                {
                    Poster newPoster = new Poster();
                    newPoster.Type = (string)poster.Key;
                    newPoster.Url = (string)poster.Value;
                    movie.Posters.Add(newPoster);
                }
            }

            var ratings = (JObject)jToken["ratings"];
            if (ratings != null)
            {
                foreach (var rating in ratings)
                {
                    Rating newRating = new Rating();
                    newRating.Type = (string)rating.Key;
                    newRating.Score = (int)rating.Value;
                    movie.Ratings.Add(newRating);
                }
            }

            var dates = (JObject)jToken["release_dates"];
            if (dates != null)
            {
                foreach (var date in dates)
                {
                    ReleaseDate releaseDate = new ReleaseDate();
                    releaseDate.Type = (string)date.Key;
                    var tmpDate = ((string)date.Value).Substring(0, ((string)date.Value).Count());
                    releaseDate.Date = DateTime.Parse(tmpDate);
                    movie.ReleaseDates.Add(releaseDate);
                }
            }

            return movie;
        }

        /// <summary>
        /// Parse Search Results For Movies
        /// </summary>
        /// <param name="json">JSON string to parse</param>
        /// <returns>MovieSearchResult object containing a list of Movie objects</returns>
        public static MovieSearchResults ParseMovieSearchResults(string json, int index=0, int limit=10)
        {
            JObject jObject = JObject.Parse(json);
            MovieSearchResults results = new MovieSearchResults();

            if (jObject["total"] != null)
                results.ResultCount = (int)jObject["total"];

            var movies = (JArray) jObject["movies"];
            if (movies != null)
            {
                foreach (var movie in movies)
                {
                    Movie m = ParseMovie(movie.ToString());
                    results.Add(m);
                }
            }

            return results;
        }


        public static IEnumerable<CastMember> ParseCastMembers(string json)
        {
            JToken jToken = JToken.FromObject(JObject.Parse(json));
            List<CastMember> Cast = new List<CastMember>();

            var castMembers = (JArray)jToken["cast"];
            if (castMembers != null)
            {
                foreach (var castMember in castMembers)
                {
                    CastMember member = new CastMember();
                    member.Name = (string)castMember["name"];
                    var characters = (JArray)castMember["characters"];
                    if (characters != null)
                    {
                        foreach (var character in characters)
                        {
                            member.Characters.Add((string)character);
                        }

                        Cast.Add(member);
                    }
                }
            }

            return Cast;
        }
    }
}
