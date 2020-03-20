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
            //Why doesn't the information get passed? I passed an object, but it's blank by the time it gets here?
        {
            var applicationDbContext = _context.User.Include(u => u.IdentityUser);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userViewModel = new UserViewModel();
            userViewModel.User = _context.User.Where(u => u.UserId == userId).FirstOrDefault();

            var user = _context.User.FirstOrDefault(a => a.UserId == userId);
            if (user is null)
            {
                return RedirectToAction("Create");
            }
            userViewModel.Stocks = _context.User_Stocks.Where(u => u.UserId == userViewModel.User.Id).ToList();
            userViewModel.User.FirstName = user.FirstName;
            //Need to do this differently, but it's a band aid for now

            //Make list of possible top stocks and foreach run the GetStockScore method. Can I run this somewhere on startup? Index is not the best place at all
            //because it will run every time the user goes back to the homepage

            //Something with await here if possible
            userViewModel.top_Stocks = _context.Top_Stocks.FirstOrDefault();

            return View(userViewModel);
        }

        [HttpPost]
        //Should be able to have post method here where I assign the stock symbol they entered and then
        //return the view that searches for the symbol using their parameters
        public async Task<IActionResult> Index(UserViewModel user)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Searches search = new Searches();
            search.Symbol = user.Search.Symbol;
            search.TimeFrame = user.Search.TimeFrame;
            search.UserId = _context.User.Where(u => u.UserId == userId).FirstOrDefault().Id;
            Scoring scoring = new Scoring();
            //This should work

            //Something here with if statements that will test if the timeframe is weekly or yearly and return based on those instead

            search.Score = scoring.GetStockScore(search);

            _context.Searches.Add(search);
            await _context.SaveChangesAsync();
            //If I'm going to add the whole search, I need to have the api call and take in the parameters
            //on the Index page. Seems possible

            //This should work, as long as the await works like I think it does

            return RedirectToAction("Index", "Searches", user);
            //info is not being stored once the redirect happens. Have to reassign in that controller? Why pass the parameter then?
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
