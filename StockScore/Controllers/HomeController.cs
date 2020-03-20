using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        public IActionResult Index()
        {
            Scoring scoring = new Scoring();
            Top_Stocks top_Stocks = new Top_Stocks();
            List<Searches> unsortedStocks = new List<Searches>();

            //Will need to change once I've implemented time frame, but works for now

            //May be better in the Startup class, but works here for now

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                for (int i = 0; i < top_Stocks.possibleTopStocks.Count(); i++)
                {
                    Searches search = new Searches();
                    search.Symbol = top_Stocks.possibleTopStocks[i];
                    search.Score = scoring.GetStockScore(search);
                    unsortedStocks.Add(search);
                }

                top_Stocks = SortStocks(unsortedStocks);
                //top_Stocks.UserId = userId;
                _context.Top_Stocks.Add(top_Stocks);
                _context.SaveChanges();
                //Probably don't need to assign this a user, but I will for now

                //By this point, sortedStocks should contain the four strings that are the stock symbols for the highest scoring stocks

                //Store these in the db, then you can grab them by the signed in user's id in the UsersController. Store as Top_Stocks

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
    }
}
