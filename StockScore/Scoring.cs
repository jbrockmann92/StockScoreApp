using StockScore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RestSharp.Authenticators;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace StockScore
{
    public class Scoring
    {
        public int GetStockScore(Searches search)
        {
            //API call
            //Web Crawler/Google search
            //Positive and negative words(??) to test against

            //Logic to compare them and assign that total to the score int
            //Return a letter grade instead?

            //Probably need a different api request if they choose a year time frame

            int googleScore;
            var client = new RestClient("https://www.alphavantage.co/");
            var request = new RestRequest("query?function=TIME_SERIES_DAILY&symbol=" + search.Symbol + "&apikey=" + APIKeys.AVKey, DataFormat.Json);
            //Got rid of time frame before &apikey= above
            var response = client.Get(request);

            JObject jobject = JObject.Parse(response.Content);
            var children = jobject.Last.First.Children().ToList();
            List<int> stockScores = new List<int>();

            for (int i = 0; i < children.Count; i++)
            {
                var open = children[i].First.First.First.ToString();
                //Only gets the opening value at the moment, but will probably work for my purposes
                float tempScore = float.Parse(open);
                stockScores.Add((int)tempScore);
            }

            //Want something also that checks if the stock is going up or down?

            googleScore = GetGoogleScore(search);

            //Something like take every 7 and search them against Google articles for 1 week ago, 2 weeks ago, etc.

            //Calculate things here. stockScores will have all necessary values by this point

            return googleScore;
        }

        public int GetGoogleScore(Searches search)
        {
            List<JToken> jObjects = new List<JToken>();

            var client = new RestClient("https://www.googleapis.com/");
            var requests = new List<RestRequest>() { new RestRequest("customsearch/v1?key=" + APIKeys.GoogleKey + "&cx=006556387307943419452:v8lfespynzs&q=" + search.Symbol + " stock performance stock market"),
                new RestRequest("customsearch/v1?key=" + APIKeys.GoogleKey + "&linksite=https://www.bloomberg.com&cx=006556387307943419452:v8lfespynzs&q=" + search.Symbol + " stock performance stock market"),
                new RestRequest("customsearch/v1?key=" + APIKeys.GoogleKey + "&linksite=https://finance.yahoo.com&cx=006556387307943419452:v8lfespynzs&q=" + search.Symbol + " stock performance stock market"),
                new RestRequest("customsearch/v1?key=" + APIKeys.GoogleKey + "&daterestrict=d[1]&cx=006556387307943419452:v8lfespynzs&q=" + search.Symbol + " stock performance stock market") };
            for (int i = 0; i < 4; i++)
            {
                var response = client.Get(requests[i]);
                JObject jObject = JObject.Parse(response.Content);
                var objString = jObject["items"].ToList();
                for (int j = 0; j < 10; j++)
                {
                    jObjects.Add(objString[j]);
                    //Not ideal for (0)n notation, but should work for now
                }
            }

            int score = CheckAgainstWords(jObjects);
            //Something about testing against the Words class and assigning score. How to do for each week in the past?
            //JObject jobject = JObject.Parse(response.Content);
            //var objString = jobject["items"].ToList();

            return score;
        }

        public int CheckAgainstWords(List<JToken> jObjects)
        {
            int score = 0;
            for (int i = 0; i < Words.negativeWords.Length; i++)
            {
                var jObject = jObjects[i].ToString().ToLower();
                if (jObject.Contains(Words.negativeWords[i]))
                {
                    score--;
                }
            }

            for (int i = 0; i < Words.positiveWords.Length; i++)
            {
                var jObject = jObjects[i].ToString().ToLower();
                if (jObject.Contains(Words.positiveWords[i]))
                {
                    score++;
                }
            }

            return score;
        }

        //Without analyzing past weeks data I still fulfill the user stories. Move on after analyzing against positive or negative
        //then worry about deepening the algorithm

    }
}
