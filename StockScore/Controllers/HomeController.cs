using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using RestSharp.Authenticators;
using StockScore.Data;
using StockScore.Models;

namespace StockScore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            Scoring scoring = new Scoring();
            Top_Stocks top_Stocks = new Top_Stocks();
            List<Searches> unsortedStocks = new List<Searches>();

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                for (int i = 0; i < top_Stocks.possibleTopStocks.Count(); i++)
                {
                    Searches search = new Searches();
                    search.Symbol = top_Stocks.possibleTopStocks[i];
                    search.Score = scoring.GetGoogleScore(search);
                    unsortedStocks.Add(search);
                }

                top_Stocks = SortStocks(unsortedStocks);
                //top_Stocks.UserId = userId;
                _context.Top_Stocks.Add(top_Stocks);
                await _context.SaveChangesAsync();
                //Probably don't need to assign this a user, but I will for now

                //By this point, sortedStocks should contain the four strings that are the stock symbols for the highest scoring stocks

                //Store these in the db, then you can grab them by the signed in user's id in the UsersController. Store as Top_Stocks
                List<string[]> peopleToContact = new List<string[]>();

                for (int i = 0; i < _context.User.Count(); i++)
                {
                    string[] person = new string[3];
                    var UserList = _context.User.ToList();
                    var IdUserList = _context.Users.ToList();
                    for (int j = 0; j < UserList.Count(); j++)
                    {
                        person[0] = UserList[j].FirstName + " " + UserList[j].LastName;
                        person[1] = IdUserList.Where(u => u.Id == UserList[j].UserId).FirstOrDefault().Email;
                        if (j < 4)
                        {
                            var stockList = _context.Top_Stocks.ToList();
                            person[2] = stockList[j].NumberOne + " " + stockList[j].NumberTwo + " " + stockList[j].NumberThree + " " + stockList[j].NumberFour;
                        }
                        peopleToContact.Add(person);
                        //Not sure if this is disgusting or beautiful.. But it works
                    }
                }

                foreach (string[] person in peopleToContact)
                {
                    SendSimpleMessage(person);
                    //I think it's running twice on sign in right now. Fix that
                }

                return Redirect("./Identity/Account/Login");
            }
            if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "Users");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public Top_Stocks SortStocks(List<Searches> stocks)
        {
            Top_Stocks top_Stocks = new Top_Stocks();

            List<Searches> sortedSearches = stocks.OrderByDescending(s => s.Score).ToList();

            top_Stocks.NumberOne = sortedSearches[0].Symbol.ToUpper();
            top_Stocks.NumberTwo = sortedSearches[1].Symbol.ToUpper();
            top_Stocks.NumberThree = sortedSearches[2].Symbol.ToUpper();
            top_Stocks.NumberFour = sortedSearches[3].Symbol.ToUpper();

            return top_Stocks;
        }

        public static IRestResponse SendSimpleMessage(string[] personToContact)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new System.Uri("https://api.mailgun.net/v3");
            client.Authenticator =
            new HttpBasicAuthenticator("api",
                                       APIKeys.MailgunKey);
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "sandboxfc0597be9eaa439ca18c08a8f738c777.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Mailgun Sandbox <postmaster@sandboxfc0597be9eaa439ca18c08a8f738c777.mailgun.org>");
            request.AddParameter("to", "" + personToContact[0] + "<" + personToContact[1] + ">");
            request.AddParameter("subject", "Hello Jacob Brockmann");
            request.AddParameter("text", "Hello " + personToContact[0] + ", Here are StockScore's top recommended stock purchases for today: " + personToContact[2]);
            request.Method = Method.POST;
            return client.Execute(request);
        }
    }
}
