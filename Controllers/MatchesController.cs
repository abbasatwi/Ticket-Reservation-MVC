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
            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "Name");
            ViewData["StadiumId"] = new SelectList(_context.Stadium, "Id", "Name");
            return View();
        }


        // POST: Matches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HomeTeamId,AwayTeamId,MatchDate,StadiumId,CategoryId")] Match match, IFormFile matchUrl)
        {
            var relativePath = _configuration["FileManagement:SystemFileUploads"];
            var path = Path.Combine(Directory.GetCurrentDirectory(), relativePath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (ModelState.IsValid)
            {
                if (match.HomeTeamId == match.AwayTeamId)
                {
                    ModelState.AddModelError("AwayTeamId", "A team cannot play against itself.");
                }
                // Validate match date
                if (match.MatchDate < DateTime.Today)
                {
                    ModelState.AddModelError("MatchDate", "Match date cannot be in the past.");
                }

                // Validate same stadium on the same day
                bool isStadiumConflict = _context.Match.Any(m => m.StadiumId == match.StadiumId && m.MatchDate == match.MatchDate);
                if (isStadiumConflict)
                {
                    ModelState.AddModelError("StadiumId", "Another match is already scheduled in this stadium on the same day.");
                }

                // Validate same team on the same day
                bool isTeamConflict = _context.Match.Any(m =>
                    (m.HomeTeamId == match.HomeTeamId || m.AwayTeamId == match.HomeTeamId || m.HomeTeamId == match.AwayTeamId || m.AwayTeamId == match.AwayTeamId) &&
                    m.MatchDate == match.MatchDate);

                if (isTeamConflict)
                {
                    ModelState.AddModelError("", "One or both teams have a match scheduled on the same day.");
                }

                // Check if there were validation errors
                if (!ModelState.IsValid)
                {
                    // Repopulate dropdowns and return view
                    PopulateDropdowns(match);
                    return View(match);
                }

                // Handle image upload
                if (matchUrl != null && matchUrl.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(matchUrl.FileName);
                    string filePath = Path.Combine(_configuration.GetValue<string>("FileManagement:SystemFileUploads"), fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await matchUrl.CopyToAsync(fileStream);
                    }

                    match.MatchUrl = fileName;
                }

                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Populate dropdowns for re-rendering form
            PopulateDropdowns(match);
            return View(match);
        }

        // Utility method for dropdowns
        private void PopulateDropdowns(Match match)
        {
            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "Name", match.AwayTeamId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", match.CategoryId);
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "Name", match.HomeTeamId);
            ViewData["StadiumId"] = new SelectList(_context.Stadium, "Id", "Name", match.StadiumId);
        }


        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var relativePath = _configuration["FileManagement:SystemFileUploads"];
            var path = Path.Combine(Directory.GetCurrentDirectory(), relativePath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            PopulateDropdowns(match);
            return View(match);
        }

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
                    if (match.HomeTeamId == match.AwayTeamId)
                    {
                        ModelState.AddModelError("AwayTeamId", "A team cannot play against itself.");
                    }
                    // Validate match date
                    if (match.MatchDate < DateTime.Today)
                    {
                        ModelState.AddModelError("MatchDate", "Match date cannot be in the past.");
                    }

                    // Validate same stadium on the same day, excluding the current match
                    bool isStadiumConflict = _context.Match
                        .Any(m => m.StadiumId == match.StadiumId && m.MatchDate == match.MatchDate && m.Id != match.Id); // Exclude current match by Id
                    if (isStadiumConflict)
                    {
                        ModelState.AddModelError("StadiumId", "Another match is already scheduled in this stadium on the same day.");
                    }

                    // Validate same team on the same day, excluding the current match
                    bool isTeamConflict = _context.Match.Any(m =>
                        (m.HomeTeamId == match.HomeTeamId || m.AwayTeamId == match.HomeTeamId || m.HomeTeamId == match.AwayTeamId || m.AwayTeamId == match.AwayTeamId) &&
                        m.MatchDate == match.MatchDate && m.Id != match.Id); // Exclude current match by Id

                    if (isTeamConflict)
                    {
                        ModelState.AddModelError("", "One or both teams have a match scheduled on the same day.");
                    }

                    // If a new matchUrl is uploaded, save it
                    if (matchUrl != null && matchUrl.Length > 0)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(matchUrl.FileName);
                        string filePath = Path.Combine(_configuration.GetValue<string>("FileManagement:SystemFileUploads"), fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await matchUrl.CopyToAsync(fileStream);
                        }

                        match.MatchUrl = fileName; // Set the new matchUrl
                    }
                    else if (string.IsNullOrEmpty(match.MatchUrl)) // Retain the existing image if no new image is provided
                    {
                        var existingMatch = await _context.Match.FindAsync(id);
                        match.MatchUrl = existingMatch?.MatchUrl; // Keep the existing image
                    }

                    // Save the updated match record
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
            PopulateDropdowns(match);
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
        public async Task<IActionResult> MatchDetails(int matchId)
        {
            try
            {
                // Retrieve the match from the database using EF Core
                var match = await _context.Match
                    .Include(m => m.HomeTeam)
                    .Include(m => m.AwayTeam)
                    .Include(m => m.Stadium)
                    .Include(m => m.Category)
                    .FirstOrDefaultAsync(m => m.Id == matchId);

                if (match == null)
                {
                    // If the match with the specified ID is not found, return a 404 Not Found response
                    return NotFound();
                }

                // Retrieve the tickets for the match from the database using EF Core
                var tickets = await _context.Ticket
                    .Where(t => t.MatchId == matchId)
                    .ToListAsync();

                // Calculate total tickets available
                int totalTicketsAvailable = tickets.Where(t => t.TicketStatus == "Available").Sum(t => t.Quantity);


                // Determine the starting price of the tickets
                decimal startingPrice = tickets.Any() ? tickets.Min(t => t.Price) : 0; // Default to 0 if no tickets are available

                // Pass the retrieved match, tickets, total tickets available, and starting price to the view using ViewBag
                ViewBag.Match = match;
                ViewBag.Tickets = tickets;
                ViewBag.TotalAvailableTickets = totalTicketsAvailable;
                ViewBag.StartingPrice = startingPrice;

                // Return the view
                return View("MatchDetails");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the execution
                // For example, log the exception or return an appropriate error response
                return StatusCode(500, ex.Message);
            }
        }
        public IActionResult Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                ViewBag.Message = "No results found. Please enter a search term.";
                return View("SearchResults", new List<Match>());
            }

            // Perform search for Matches
            var matches = _context.Match
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Stadium)
                .Include(m => m.Category)
                .Where(m => m.HomeTeam.Name.Contains(searchTerm) ||
                            m.AwayTeam.Name.Contains(searchTerm) ||
                            m.Stadium.Name.Contains(searchTerm) ||
                            m.Category.Name.Contains(searchTerm))
                .ToList();

            // Perform search for Teams
            var teams = _context.Teams
                .Where(t => t.Name.Contains(searchTerm))
                .ToList();

            // Perform search for Stadiums
            var stadiums = _context.Stadium
                .Where(s => s.Name.Contains(searchTerm) || s.Location.Contains(searchTerm))
                .ToList();

            // Use ViewBag to pass results to the view
            ViewBag.Matches = matches;
            ViewBag.Teams = teams;
            ViewBag.Stadiums = stadiums;

            if (!matches.Any() && !teams.Any() && !stadiums.Any())
            {
                ViewBag.Message = "No results found for the provided search term.";
            }

            return View("SearchResults");
        }


    }
}