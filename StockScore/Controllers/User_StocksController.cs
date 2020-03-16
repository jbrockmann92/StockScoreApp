using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockScore.Data;
using StockScore.Models;

namespace StockScore.Controllers
{
    public class User_StocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public User_StocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: User_Stocks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.User_Stocks.Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: User_Stocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user_Stocks = await _context.User_Stocks
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user_Stocks == null)
            {
                return NotFound();
            }

            return View(user_Stocks);
        }

        // GET: User_Stocks/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: User_Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StockSymbol,PurchaseDate,UserId")] User_Stocks user_Stocks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user_Stocks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", user_Stocks.UserId);
            return View(user_Stocks);
        }

        // GET: User_Stocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user_Stocks = await _context.User_Stocks.FindAsync(id);
            if (user_Stocks == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", user_Stocks.UserId);
            return View(user_Stocks);
        }

        // POST: User_Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StockSymbol,PurchaseDate,UserId")] User_Stocks user_Stocks)
        {
            if (id != user_Stocks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user_Stocks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!User_StocksExists(user_Stocks.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", user_Stocks.UserId);
            return View(user_Stocks);
        }

        // GET: User_Stocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user_Stocks = await _context.User_Stocks
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user_Stocks == null)
            {
                return NotFound();
            }

            return View(user_Stocks);
        }

        // POST: User_Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user_Stocks = await _context.User_Stocks.FindAsync(id);
            _context.User_Stocks.Remove(user_Stocks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool User_StocksExists(int id)
        {
            return _context.User_Stocks.Any(e => e.Id == id);
        }
    }
}
