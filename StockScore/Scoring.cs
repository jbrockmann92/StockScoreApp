﻿using StockScore.Models;
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
        public List<int> GetStockScore(Searches search)
        {
            //API call
            //Web Crawler/Google search
            //Positive and negative words(??) to test against

            //Logic to compare them and assign that total to the score int
            //Return a letter grade instead?

            //Probably need a different api request if they choose a year time frame

            int googleScore;
            List<int> stockScores = new List<int>();
            var client = new RestClient("https://www.alphavantage.co/");
            RestRequest request = null;
            
            if (search.TimeFrame == "Day")
            {
                request = new RestRequest("query?function=TIME_SERIES_DAILY&symbol=" + search.Symbol + "&apikey=" + APIKeys.AVKey, DataFormat.Json);
                //Probably better to put these three identicaly pieces into a method and call it three times

                var response = client.Get(request);

                stockScores = GetOpenValues(response, search); //Opening values by day

                //Want something also that checks if the stock is going up or down?
            }
            else if (search.TimeFrame == "Week")
            {
                //if (search.IsForPastScores == false)
                //{
                    request = new RestRequest("query?function=TIME_SERIES_WEEKLY&symbol=" + search.Symbol + "&apikey=" + APIKeys.AVKey, DataFormat.Json);

                    var response = client.Get(request);

                    stockScores = GetOpenValues(response, search); //Opening values by week
                //}

                //Maybe can only be if statement that runs after what's above?
                //else if (search.IsForPastScores)
                //{
                //    //4x by week for the past week. Store in list of List<int>?
                //    for (int i = 0; i < 5; i++)
                //    {
                //        request = new RestRequest("query?function=TIME_SERIES_WEEKLY&symbol=" + search.Symbol + "&apikey=" + APIKeys.AVKey, DataFormat.Json);
                //        //Will return requests that are each 7 days earlier for each loop
                //        var response = client.Get(request);

                //        stockScores = GetOpenValues(response, search);
                //    }
                //}

                //Want something also that checks if the stock is going up or down?
            }
            else if (search.TimeFrame == "Month")
            {
                request = new RestRequest("query?function=TIME_SERIES_MONTHLY&symbol=" + search.Symbol + "&apikey=" + APIKeys.AVKey, DataFormat.Json);

                var response = client.Get(request);

                stockScores = GetOpenValues(response, search); //Opening values by month

                //Want something also that checks if the stock is going up or down?
                //Probably could just take it in quarters, average the quarters, and assign to variables, the see what order they come out
            }

            //Something like take every 7 and search them against Google articles for 1 week ago, 2 weeks ago, etc.

            //Calculate things here. stockScores will have all necessary values by this point
            googleScore = GetGoogleScore(search);

            for (int i = 0; i < 10; i++)
            {
                //Logic to factor in the Google score based on the last 10? stock values
            }

            return stockScores;
        }

        public int GetGoogleScore(Searches search)
        {
            List<JToken> jObjects = new List<JToken>();

            //"&sort=review-date:r::" + long.Parse(DateTime.Now.Date.AddDays(-7*i).ToString("yyyyMMdd")) +  Need this in the Google requests if IsForPastSearches is true

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
                for (int j = 0; j < objString.Count(); j++)
                {
                    jObjects.Add(objString[j]);
                    //Not ideal for (0)n notation, but should work for now
                    //Looks like I need an await here somewhere. It's not working unless I have break points here
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

        public List<int> GetOpenValues(IRestResponse response, Searches search)
        {
            JObject jobject = JObject.Parse(response.Content);
            var children = jobject.Last.First.Children().ToList();
            List<int> stockScores = new List<int>();

            if (search.IsForPastScores)
            {

            }

            for (int i = 0; i < children.Count; i++)
            {
                var open = children[i].First.First.First.ToString();
                //Only gets the opening value at the moment, but will probably work for my purposes
                float tempScore = float.Parse(open);
                stockScores.Add((int)tempScore);
                //This will be daily open values for the stock
            }
            return stockScores;
        }

    }
}
