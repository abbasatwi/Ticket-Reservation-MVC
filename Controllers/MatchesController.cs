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
            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "Id");
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id");
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "Id");
            ViewData["StadiumId"] = new SelectList(_context.Stadium, "Id", "Id");
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HomeTeamId,AwayTeamId,MatchDate,StadiumId,CategoryId")] Match match, IFormFile matchUrl)
        {
            if (ModelState.IsValid)
            {
                // Check if an image file is uploaded
                if (matchUrl != null && matchUrl.Length > 0)
                {
                    // Generate a unique file name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(matchUrl.FileName);

                    // Save the file to a directory
                    string filePath = Path.Combine(_configuration.GetSection("FileManagement:SystemFileUploads").Value, fileName);

                    // Save the file
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await matchUrl.CopyToAsync(fileStream);
                    }

                    // Set the file URL in the match model
                    match.MatchUrl = fileName;
                }

                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Populate the dropdown lists with meaningful data
            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "Name", match.AwayTeamId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", match.CategoryId);
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "Name", match.HomeTeamId);
            ViewData["StadiumId"] = new SelectList(_context.Stadium, "Id", "Name", match.StadiumId);
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
            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "Id", match.AwayTeamId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", match.CategoryId);
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "Id", match.HomeTeamId);
            ViewData["StadiumId"] = new SelectList(_context.Stadium, "Id", "Id", match.StadiumId);
            return View(match);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HomeTeamId,AwayTeamId,MatchUrl,MatchDate,StadiumId,CategoryId")] Match match)
        {
            if (id != match.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "Id", "Id", match.AwayTeamId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", match.CategoryId);
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "Id", "Id", match.HomeTeamId);
            ViewData["StadiumId"] = new SelectList(_context.Stadium, "Id", "Id", match.StadiumId);
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
            // Check if the search term is null or empty
            if (string.IsNullOrEmpty(searchTerm))
            {
                ViewBag.Message = "No results found. Please enter a search term.";
                return View("SearchResults", new List<Match>()); // Return an empty list
            }

            // Perform search logic based on the searchTerm
            var matches = _context.Match
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Stadium)
                .Include(m => m.Category)
                .Where(m => m.HomeTeam.Name.Contains(searchTerm) || m.AwayTeam.Name.Contains(searchTerm))
                .ToList();

            // Check if no matches are found
            if (matches == null || !matches.Any())
            {
                ViewBag.Message = "No matches found for the provided search term.";
            }

            return View("SearchResults", matches);
        }


    }
}
