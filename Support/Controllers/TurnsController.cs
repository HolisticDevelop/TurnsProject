using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Support.Data;
using Support.Models;

namespace Support.Controllers
{
    public class TurnsController : Controller
    {
        private readonly SupportContext _context;

        public TurnsController(SupportContext context)
        {
            _context = context;
        }

        // GET: Turns
        public async Task<IActionResult> Index()
        {
            var supportContext = _context.Turn.Include(t => t.User);
            return View(await supportContext.ToListAsync());
        }

        // GET: Turns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Turn == null)
            {
                return NotFound();
            }

            var turn = await _context.Turn
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turn == null)
            {
                return NotFound();
            }

            return View(turn);
        }

        // GET: Turns/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "Id", "FullName");
            return View();
        }

        // POST: Turns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartTime,EndTime,UserId")] Turn turn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "FullName", turn.UserId);
            return View(turn);
        }

        // GET: Turns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Turn == null)
            {
                return NotFound();
            }

            var turn = await _context.Turn.FindAsync(id);
            if (turn == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "FullName", turn.UserId);
            return View(turn);
        }

        // POST: Turns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartTime,EndTime,UserId")] Turn turn)
        {
            if (id != turn.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnExists(turn.Id))
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
            ViewData["UserId"] = new SelectList(_context.User, "Id", "FullName", turn.UserId);
            return View(turn);
        }

        // GET: Turns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Turn == null)
            {
                return NotFound();
            }

            var turn = await _context.Turn
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turn == null)
            {
                return NotFound();
            }

            return View(turn);
        }

        // POST: Turns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Turn == null)
            {
                return Problem("Entity set 'SupportContext.Turn'  is null.");
            }
            var turn = await _context.Turn.FindAsync(id);
            if (turn != null)
            {
                _context.Turn.Remove(turn);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurnExists(int id)
        {
          return (_context.Turn?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
