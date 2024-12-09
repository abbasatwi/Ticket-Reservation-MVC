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
    public class MatchesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public MatchesController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Match.Include(m => m.AwayTeam).Include(m => m.Category).Include(m => m.HomeTeam).Include(m => m.Stadium);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match
                .Include(m => m.AwayTeam)
                .Include(m => m.Category)
                .Include(m => m.HomeTeam)
                .Include(m => m.Stadium)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        // POST: Matches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HomeTeamId,AwayTeamId,MatchDate,StadiumId,CategoryId")] Match match, IFormFile matchUrl)
        {
            if (ModelState.IsValid)
            {
                // Validation logic
                ValidateMatch(match);

                if (!ModelState.IsValid)
                {
                    PopulateDropdowns();
                    return View(match);
                }

                // Handle file upload
                match.MatchUrl = await SaveFileAsync(matchUrl);

                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateDropdowns();
            return View(match);
        }

        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            PopulateDropdowns();
            return View(match);
        }

        // POST: Matches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HomeTeamId,AwayTeamId,MatchUrl,MatchDate,StadiumId,CategoryId")] Match match, IFormFile matchUrl)
        {
            if (id != match.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Validation logic
                    ValidateMatch(match);

                    if (!ModelState.IsValid)
                    {
                        PopulateDropdowns();
                        return View(match);
                    }

                    // Handle file upload or retain existing URL
                    if (matchUrl != null && matchUrl.Length > 0)
                    {
                        match.MatchUrl = await SaveFileAsync(matchUrl);
                    }
                    else
                    {
                        _context.Entry(match).Property(m => m.MatchUrl).IsModified = false;
                    }

                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.Id))
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
            PopulateDropdowns();
            return View(match);
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match
                .Include(m => m.AwayTeam)
                .Include(m => m.Category)
                .Include(m => m.HomeTeam)
                .Include(m => m.Stadium)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var match = await _context.Match.FindAsync(id);
            if (match != null)
            {
                _context.Match.Remove(match);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(int id)
        {
            return _context.Match.Any(e => e.Id == id);
        }

        // Helper: Populate dropdowns
        private void PopulateDropdowns()
        {
            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "Name");
            ViewData["StadiumId"] = new SelectList(_context.Stadium, "Id", "Name");
        }

        // Helper: File upload
        private async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var relativePath = _configuration["FileManagement:SystemFileUploads"];
            var path = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(path, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return fileName;
        }

        // Helper: Validation logic
        private void ValidateMatch(Match match)
        {
            if (match.HomeTeamId == match.AwayTeamId)
            {
                ModelState.AddModelError("AwayTeamId", "A team cannot play against itself.");
            }

            if (match.MatchDate < DateTime.Today)
            {
                ModelState.AddModelError("MatchDate", "Match date cannot be in the past.");
            }

            // Exclude the current match ID from stadium conflict check
            bool isStadiumConflict = _context.Match.Any(m =>
                m.StadiumId == match.StadiumId &&
                m.MatchDate == match.MatchDate &&
                m.Id != match.Id); // Exclude current match
            if (isStadiumConflict)
            {
                ModelState.AddModelError("StadiumId", "Another match is already scheduled in this stadium on the same day.");
            }

            // Exclude the current match ID from team conflict check
            bool isTeamConflict = _context.Match.Any(m =>
                m.Id != match.Id && // Exclude current match
                m.MatchDate == match.MatchDate &&
                (m.HomeTeamId == match.HomeTeamId ||
                 m.AwayTeamId == match.HomeTeamId ||
                 m.HomeTeamId == match.AwayTeamId ||
                 m.AwayTeamId == match.AwayTeamId));

            if (isTeamConflict)
            {
                ModelState.AddModelError("", "One or both teams have a match scheduled on the same day.");
            }
        }
    }
}
