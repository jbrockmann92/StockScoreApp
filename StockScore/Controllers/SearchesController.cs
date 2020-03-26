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

namespace StockScore.Controllers
{
    public class SearchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SearchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Searches
        public async Task<IActionResult> Index(int? id)
        {
            UserViewModel model = new UserViewModel();
            Scoring scoring = new Scoring();
            List<Searches> searches = new List<Searches>();

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            model.Id = _context.User.Where(u => u.UserId == userId).FirstOrDefault().Id;
            model.Searches = _context.Searches.Where(s => s.UserId == model.Id).ToList();
            model.Search = _context.Searches.Where(s => s.UserId == model.Id).ToList()[_context.Searches.Count() - 1];
            model.Searches[model.Searches.Count - 1].IsForPastScores = true;
            model.PastMonthScores = scoring.GetStockScore(model.Searches[model.Searches.Count - 1]);
            model.Searches[model.Searches.Count - 1].IsForPastScores = false;
            model.Searches = model.Searches.OrderByDescending(s => s.Id).ToList();
            model.Searches[0].Score = model.PastMonthScores[0];
            searches = model.Searches;
            model.Searches = new List<Searches>();

            for (int i = 0; i < 5 && i < model.Searches.Count(); i++)
            {
                model.Searches.Add(searches[i]);
            }

            if (id != 0)
            {
                model.Search = _context.Searches.Where(s => s.Id == id).FirstOrDefault();
            }

            return View(model);
        }

        // GET: Searches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Searches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ticker,Score,DateSearched")] Searches searches)
        {
            if (ModelState.IsValid)
            {
                _context.Add(searches);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(searches);
        }

        // GET: Searches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var searches = await _context.Searches.FindAsync(id);
            if (searches == null)
            {
                return NotFound();
            }
            return View(searches);
        }

        // POST: Searches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ticker,Score,DateSearched")] Searches searches)
        {
            if (id != searches.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(searches);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SearchesExists(searches.Id))
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
            return View(searches);
        }

        // GET: Searches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var searches = await _context.Searches
                .FirstOrDefaultAsync(m => m.Id == id);
            if (searches == null)
            {
                return NotFound();
            }

            return View(searches);
        }

        // POST: Searches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var searches = await _context.Searches.FindAsync(id);
            _context.Searches.Remove(searches);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SearchesExists(int id)
        {
            return _context.Searches.Any(e => e.Id == id);
        }
    }
}
