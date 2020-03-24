using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockScore.Data;
using StockScore.Models;

using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StockScore.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.User.Include(u => u.IdentityUser);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userViewModel = new UserViewModel();

            var user = _context.User.FirstOrDefault(a => a.UserId == userId);
            if (user is null)
            {
                return RedirectToAction("Create");
            }

            userViewModel.User = _context.User.Where(u => u.UserId == userId).FirstOrDefault();
            userViewModel.Stocks = _context.User_Stocks.Where(u => u.UserId == userViewModel.User.Id).ToList();
            userViewModel.User.FirstName = user.FirstName;
            userViewModel.top_Stocks = _context.Top_Stocks.FirstOrDefault();

            for (int i = 0; i < userViewModel.Stocks.Count(); i++)
            {
                Scoring scoring = new Scoring();
                Searches search = new Searches();
                search.Symbol = userViewModel.Stocks[i].StockSymbol;
                search.TimeFrame = "Week";

                List<int> allScores = scoring.GetStockScore(search);
                //Considre just returning an int here. Can just take index 0 for now
                userViewModel.Stocks[i].Scores = new List<int>();
                int stockScoreLimit = int.Parse(userViewModel.Stocks[i].PurchaseDate);
                for (int j = 0; j < stockScoreLimit; j++)
                {
                    //Not ideal because of big O, but works for now
                    userViewModel.Stocks[0].Scores.Add(0);
                    //This will add an extra 0 on the end I think after it goes the first time
                    userViewModel.Stocks[0].Scores[j] += allScores[j];
                    //Probably works
                }
                //How to consolidate all scores? Or do I want to list each one individually on a graph?
            }
            //Make this its own method??
            if (userViewModel.Stocks.Count() == 0)
            {
                userViewModel.Stocks.Add(new User_Stocks() { });
                userViewModel.Stocks[0].Scores = new List<int>() { 0, 0, 0, 0, 0, 0};
                //Just make it meaningless values? 

            //Not very nice, but keeps it from erroring out
            }

            return View(userViewModel);
        }

        [HttpPost]
        //Should be able to have post method here where I assign the stock symbol they entered and then
        //return the view that searches for the symbol using their parameters
        public async Task<IActionResult> Index(UserViewModel user)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Searches search = new Searches();
            Scoring scoring = new Scoring();

            search.Symbol = user.Search.Symbol;
            search.TimeFrame = user.Search.TimeFrame;
            search.UserId = _context.User.Where(u => u.UserId == userId).FirstOrDefault().Id;
            search.Score = scoring.GetStockScore(search)[0];
            //Will return the first in the list of scores

            _context.Searches.Add(search);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Searches", user);
        }


        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,UserId")] User user)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            user.UserId = userId;
            if (ModelState.IsValid)
            {
                _context.User.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", user.UserId);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", user.UserId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,UserId")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", user.UserId);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
