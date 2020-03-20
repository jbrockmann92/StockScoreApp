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
            List<string> unsortedStocks = new List<string>();
            

            //Will need to change once I've implemented time frame, but works for now


            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                for (int i = 0; i < top_Stocks.possibleTopStocks.Count(); i++)
                {
                    Searches search = new Searches();
                    search.Symbol = top_Stocks.possibleTopStocks[i];
                    int stockScore = scoring.GetStockScore(search);

                }

                List<string> sortedStocks = SortStocks();

                return Redirect("./Identity/Account/Login");
            }
            if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "Users", top_Stocks);
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

        public List<string> SortStocks()
        {

        }
    }
}
