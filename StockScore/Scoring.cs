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
        public List<int> GetStockScore(Searches search)
        {
            //I think it does make sense for this to be a List<int>. If I just need the score I can grab the last index, and if I need the list I have it
            //Maybe should make two methods?
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

                request = new RestRequest("query?function=TIME_SERIES_WEEKLY&symbol=" + search.Symbol + "&apikey=" + APIKeys.AVKey, DataFormat.Json);

                var response = client.Get(request);

                stockScores = GetOpenValues(response, search); //Opening values by week

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

            if (search.Id != 0)
            {
                googleScore = GetGoogleScore(search);
                var stockDirection = stockScores[stockScores.Count() - 1] / ((stockScores[stockScores.Count() - 4] + stockScores[stockScores.Count() - 3] + stockScores[stockScores.Count() - 2]) / 3);
                //Checks pretty crudely if it's going up or down

                for (int i = 0; i < 10; i++)
                {
                    stockScores[i] = googleScore * stockDirection;

                    int oneOrZero = 1;
                    if (i > i + 1)
                    {
                        oneOrZero = 0;
                    }

                    stockScores[i] += oneOrZero;
                }

                return stockScores;
            }

            return stockScores;
        }

        public int GetGoogleScore(Searches search)
        {
            List<JToken> jObjects = new List<JToken>();
            var client = new RestClient("https://www.googleapis.com/");
            var requests = new List<RestRequest>() { new RestRequest("customsearch/v1?key=" + APIKeys.GoogleKey + "&cx=006556387307943419452:v8lfespynzs&q=" + search.Symbol + " stock performance stock market"),
                new RestRequest("customsearch/v1?key=" + APIKeys.GoogleKey + "&linksite=https://www.bloomberg.com&cx=006556387307943419452:v8lfespynzs&q=" + search.Symbol + " stock performance stock market"),
                new RestRequest("customsearch/v1?key=" + APIKeys.GoogleKey + "&linksite=https://finance.yahoo.com&cx=006556387307943419452:v8lfespynzs&q=" + search.Symbol + " stock performance stock market"),
                new RestRequest("customsearch/v1?key=" + APIKeys.GoogleKey + "&linksite=https://www.forbes.com&cx=006556387307943419452:v8lfespynzs&q=" + search.Symbol + " stock performance stock market") };

            //This will run twice for each one right now. Once when it's first called, once when it goes through to get the past month

            if (search.IsForPastScores)
            {
                for (int i = 1; i < 5; i++)
                {
                    requests.Add(new RestRequest("customsearch/v1?key=" + APIKeys.GoogleKey + "&sort=date:r::" + long.Parse(DateTime.Now.Date.AddDays(-7 * i).ToString("yyyyMMdd")) + "&cx=006556387307943419452:v8lfespynzs&q=" + search.Symbol + " stock performance stock market"));
                    requests.Add(new RestRequest("customsearch/v1?key=" + APIKeys.GoogleKey + "&sort=date:r::" + long.Parse(DateTime.Now.Date.AddDays(-7 * i).ToString("yyyyMMdd")) + "&linksite=https://www.bloomberg.com&cx=006556387307943419452:v8lfespynzs&q=" + search.Symbol + " stock performance stock market"));
                    requests.Add(new RestRequest("customsearch/v1?key=" + APIKeys.GoogleKey + "&sort=date:r::" + long.Parse(DateTime.Now.Date.AddDays(-7 * i).ToString("yyyyMMdd")) + "&linksite=https://finance.yahoo.com&cx=006556387307943419452:v8lfespynzs&q=" + search.Symbol + " stock performance stock market"));
                    requests.Add(new RestRequest("customsearch/v1?key=" + APIKeys.GoogleKey + "&sort=date:r::" + long.Parse(DateTime.Now.Date.AddDays(-7 * i).ToString("yyyyMMdd")) + "&linksite=https://www.forbes.com&cx=006556387307943419452:v8lfespynzs&q=" + search.Symbol + " stock performance stock market"));
                }
            }
            

            
            for (int i = 0; i < requests.Count; i++)
            {
                List<JToken> tempJObjects = new List<JToken>();
                var response = client.Get(requests[i]);
                JObject jObject = JObject.Parse(response.Content);
                var objString = jObject["items"].ToList();
                for (int j = 0; j < objString.Count(); j++)
                {
                    tempJObjects.Add(objString[j]);
                }
                jObjects = tempJObjects;
            }

            int score = CheckAgainstWords(jObjects);

            if (search.IsForPastScores)
            {
                score /= 5;
            }

            return score;
        }

        public int CheckAgainstWords(List<JToken> jObjects)
        {
            int score = 0;
            for (int i = 0; i < jObjects.Count(); i++)
            {
                var jObject = jObjects[i].ToString().ToLower();
                for (int j = 0; j < Words.negativeWords.Length; j++)
                {
                    if (jObject.Contains(Words.negativeWords[j]))
                    {
                        score--;
                    }
                }
            }

            for (int i = 0; i < jObjects.Count(); i++)
            {
                var jObject = jObjects[i].ToString().ToLower();
                for (int j = 0; j < Words.positiveWords.Length; j++)
                {
                    if (jObject.Contains(Words.positiveWords[j]))
                    {
                        score++;
                    }
                }
            }

            return score;
        }

        public List<int> GetOpenValues(IRestResponse response, Searches search)
        {
            JObject jobject = JObject.Parse(response.Content);
            var children = jobject.Last.First.Children().ToList();
            List<int> stockScores = new List<int>();

            for (int i = 0; i < children.Count; i++)
            {
                var open = children[i].First.First.First.ToString();
                float tempScore = float.Parse(open);
                stockScores.Add((int)tempScore);
            }
            return stockScores;
        }

    }
}
