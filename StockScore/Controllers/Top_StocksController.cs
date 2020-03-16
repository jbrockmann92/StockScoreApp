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
    public class Top_StocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Top_StocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Top_Stocks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Top_Stocks.ToListAsync());
        }

        // GET: Top_Stocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var top_Stocks = await _context.Top_Stocks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (top_Stocks == null)
            {
                return NotFound();
            }

            return View(top_Stocks);
        }

        // GET: Top_Stocks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Top_Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,numberOne,numberTwo,numberThree,numberFour")] Top_Stocks top_Stocks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(top_Stocks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(top_Stocks);
        }

        // GET: Top_Stocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var top_Stocks = await _context.Top_Stocks.FindAsync(id);
            if (top_Stocks == null)
            {
                return NotFound();
            }
            return View(top_Stocks);
        }

        // POST: Top_Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,numberOne,numberTwo,numberThree,numberFour")] Top_Stocks top_Stocks)
        {
            if (id != top_Stocks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(top_Stocks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Top_StocksExists(top_Stocks.Id))
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
            return View(top_Stocks);
        }

        // GET: Top_Stocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var top_Stocks = await _context.Top_Stocks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (top_Stocks == null)
            {
                return NotFound();
            }

            return View(top_Stocks);
        }

        // POST: Top_Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var top_Stocks = await _context.Top_Stocks.FindAsync(id);
            _context.Top_Stocks.Remove(top_Stocks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Top_StocksExists(int id)
        {
            return _context.Top_Stocks.Any(e => e.Id == id);
        }
    }
}
