using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project_new.Data;
using project_new.Models;

namespace project_new.Controllers
{
    public class CaptainsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CaptainsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Captains
        public async Task<IActionResult> Index()
        {
            return View(await _context.Captain.ToListAsync());
        }

        // GET: Captains/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var captain = await _context.Captain
                .FirstOrDefaultAsync(m => m.Id == id);
            if (captain == null)
            {
                return NotFound();
            }

            return View(captain);
        }

        // GET: Captains/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Captains/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Captain captain)
        {
            if (ModelState.IsValid)
            {
                _context.Add(captain);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(captain);
        }

        // GET: Captains/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var captain = await _context.Captain.FindAsync(id);
            if (captain == null)
            {
                return NotFound();
            }
            return View(captain);
        }

        // POST: Captains/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Captain captain)
        {
            if (id != captain.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(captain);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaptainExists(captain.Id))
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
            return View(captain);
        }

        // GET: Captains/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var captain = await _context.Captain
                .FirstOrDefaultAsync(m => m.Id == id);
            if (captain == null)
            {
                return NotFound();
            }

            return View(captain);
        }

        // POST: Captains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var captain = await _context.Captain.FindAsync(id);
            if (captain != null)
            {
                _context.Captain.Remove(captain);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaptainExists(int id)
        {
            return _context.Captain.Any(e => e.Id == id);
        }
    }
}
