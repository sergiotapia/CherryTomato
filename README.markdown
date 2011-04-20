CherryTomato - RottenTomatoes in the .NET Flavor
=================================================================================================

![.NET | CherryTomato](http://i.imgur.com/n1wKY.png ".NET | CherryTomato")


CherryTomato makes using information provided by RottenTomatoes.com very simple. It's aim is to allow developers to quickly and safely create powerful applications with the JSON api provided for free by RottenTomatoes.

Full C# POCO objects allow you to concentrate on creating a great application and not parsing random JSON from a source.

How Do You Use It?
------------------
The most basic form is to search for a movie using it's unique identification number.

        string apiKey = ConfigurationManager.AppSettings["ApiKey"];
        var tomatoe = new Tomatoe(apiKey);
 
        //Numerical ID in this case is 9818.
        var movie = tomatoe.FindMovieById(9818);
    
        Console.WriteLine(movie.Name);
        Console.WriteLine(movie.Year);
        foreach (var rating in movie.Ratings)
        {
            Console.WriteLine("{0} Rating: {1}", rating.Type, rating.Score);
        }

        Console.ReadKey();

Standing On The Shoulders of Giants
------------------
CherryTomato is proudly powered by the phenomenal library JSON.Net provided for free by James Newton.

[http://james.newtonking.com/pages/json-net.aspx](http://james.newtonking.com/pages/json-net.aspx "NewtonKing")

DISCLAIMER
------------------
This library is still in alpha stages, some of the method names and exposed properties might change.