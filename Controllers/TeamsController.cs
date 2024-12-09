using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.ProjectModel;
using project_new.Data;
using project_new.Models;

namespace project_new.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        Uri baseAddress = new Uri("http://localhost:5019/api/");

        public TeamsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Teams.Include(t => t.Captain).Include(t => t.Manager).Include(t => t.Stadium);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(t => t.Captain)
                .Include(t => t.Manager)
                .Include(t => t.Stadium)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            PopulateViewData();
            return View();
        }

        // Helper method to populate ViewData for dropdowns
        private void PopulateViewData()
        {
            ViewData["Manager"] = new SelectList(_context.Manager, "Id", "Name");
            ViewData["Captain"] = new SelectList(_context.Captain, "Id", "Name");
            ViewData["Stadium"] = new SelectList(_context.Stadium, "Id", "Name");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,LogoUrl,ManagerId,CaptainId,StadiumId")] Team team, IFormFile logoFile)
        {
            if (ModelState.IsValid)
            {
                // Check for uniqueness of Manager, Captain, and Stadium
                if (_context.Teams.Any(t => t.ManagerId == team.ManagerId))
                {
                    ModelState.AddModelError("ManagerId", "The selected Manager is already assigned to another team.");
                }
                if (_context.Teams.Any(t => t.CaptainId == team.CaptainId))
                {
                    ModelState.AddModelError("CaptainId", "The selected Captain is already assigned to another team.");
                }
                if (_context.Teams.Any(t => t.StadiumId == team.StadiumId))
                {
                    ModelState.AddModelError("StadiumId", "The selected Stadium is already assigned to another team.");
                }
            }

                // Validate Name
                if (string.IsNullOrWhiteSpace(team.Name))
            {
                ModelState.AddModelError("Name", "The Name field is required.");
            }
            else if (team.Name.Length < 3)
            {
                ModelState.AddModelError("Name", "The Name must be at least 3 characters long.");
            }

            // Validate Description
            if (string.IsNullOrWhiteSpace(team.Description))
            {
                ModelState.AddModelError("Description", "The Description field is required.");
            }

            // Validate Logo File
            if (logoFile == null || logoFile.Length == 0)
            {
                ModelState.AddModelError("LogoUrl", "A logo file is required.");
            }
            else if (!IsImageFileValid(logoFile))
            {
                ModelState.AddModelError("LogoUrl", "The logo file must be a valid image format (JPEG, PNG).");
            }

            if (ModelState.IsValid)
            {
                // Handle logo file upload
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(logoFile.FileName);
                string filePath = Path.Combine(_configuration.GetSection("FileManagement:SystemFileUploads").Value, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await logoFile.CopyToAsync(fileStream);
                }
                team.LogoUrl = fileName;

                // Save team
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate dropdowns in case of an error
            PopulateViewData();
            return View(team);
        }

        // Helper Method to Validate Image Files
        private bool IsImageFileValid(IFormFile file)
        {
            string[] validExtensions = { ".jpg", ".jpeg", ".png" };
            string fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return validExtensions.Contains(fileExtension);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            PopulateViewData();
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,LogoUrl,ManagerId,CaptainId,StadiumId")] Team team, IFormFile logoFile)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Allow Manager, Captain, and Stadium to belong to the current team
                if (_context.Teams.Any(t => t.ManagerId == team.ManagerId && t.Id != team.Id))
                {
                    ModelState.AddModelError("ManagerId", "The selected Manager is already assigned to another team.");
                }
                if (_context.Teams.Any(t => t.CaptainId == team.CaptainId && t.Id != team.Id))
                {
                    ModelState.AddModelError("CaptainId", "The selected Captain is already assigned to another team.");
                }
                if (_context.Teams.Any(t => t.StadiumId == team.StadiumId && t.Id != team.Id))
                {
                    ModelState.AddModelError("StadiumId", "The selected Stadium is already assigned to another team.");
                }

                // Validate Name
                if (string.IsNullOrWhiteSpace(team.Name))
                {
                    ModelState.AddModelError("Name", "The Name field is required.");
                }
                else if (team.Name.Length < 3)
                {
                    ModelState.AddModelError("Name", "The Name must be at least 3 characters long.");
                }

                // Validate Description
                if (string.IsNullOrWhiteSpace(team.Description))
                {
                    ModelState.AddModelError("Description", "The Description field is required.");
                }

                // Validate Logo File
                if (logoFile != null && logoFile.Length > 0)
                {
                    if (!IsImageFileValid(logoFile))
                    {
                        ModelState.AddModelError("LogoUrl", "The logo file must be a valid image format (JPEG, PNG).");
                    }
                    else
                    {
                        // Handle logo file upload
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(logoFile.FileName);
                        string filePath = Path.Combine(_configuration.GetSection("FileManagement:SystemFileUploads").Value, fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await logoFile.CopyToAsync(fileStream);
                        }
                        team.LogoUrl = fileName;
                    }
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(team);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TeamExists(team.Id))
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
            }

            PopulateViewData();
            return View(team);
        }


        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(t => t.Captain)
                .Include(t => t.Manager)
                .Include(t => t.Stadium)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Teams
                .Include(t => t.HomeMatches)
                .Include(t => t.AwayMatches)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            // Check if the team is part of any matches
            if (team.HomeMatches.Any() || team.AwayMatches.Any())
            {
                ModelState.AddModelError(string.Empty, "The team cannot be deleted because it is associated with one or more matches.");
                return View(team);
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }

        //[HttpGet("{id}", Name = "GetTeam")]
        //public IActionResult GetTeam(int id)
        //{
        //    var team = _context.Teams
        //        .Include(t => t.Manager)
        //        .Include(t => t.Captain)
        //        .Include(t => t.Stadium)
        //        .Include(t => t.HomeMatches)
        //            .ThenInclude(m => m.AwayTeam)
        //        .Include(t => t.AwayMatches)
        //            .ThenInclude(m => m.HomeTeam)
        //        .FirstOrDefault(t => t.Id == id);

        //    if (team == null)
        //    {
        //        return NotFound();
        //    }

        //    // Create a custom DTO to represent the team without circular references
        //    var teamDto = new
        //    {
        //        team.Id,
        //        team.Name,
        //        team.Description,
        //        team.LogoUrl,
        //        Manager = new { team.Manager.Id, team.Manager.Name },
        //        Captain = new { team.Captain.Id, team.Captain.Name },
        //        Stadium = new
        //        {
        //            team.Stadium.Id,
        //            team.Stadium.Name,
        //            team.Stadium.Description,
        //            team.Stadium.Capacity,
        //            team.Stadium.Location
        //        },
        //        HomeMatches = team.HomeMatches.Select(m => new
        //        {
        //            m.Id,
        //            m.MatchDate,
        //            HomeTeam = new { m.HomeTeam.Id, m.HomeTeam.Name },
        //            AwayTeam = new { m.AwayTeam.Id, m.AwayTeam.Name }
        //        }),
        //        AwayMatches = team.AwayMatches.Select(m => new
        //        {
        //            m.Id,
        //            m.MatchDate,
        //            HomeTeam = new { m.HomeTeam.Id, m.HomeTeam.Name },
        //            AwayTeam = new { m.AwayTeam.Id, m.AwayTeam.Name }
        //        })
        //    };

        //    // Serialize the DTO to JSON
        //    var json = JsonConvert.SerializeObject(teamDto);

        //    // Return the JSON response
        //    return Ok(json);
        //}

        public async Task<IActionResult> GetTeam(int id)
        {
            try
            {
                // Retrieve the team from the database using EF Core
                var team = await _context.Teams
                    .Include(t => t.Manager)
                    .Include(t => t.Captain)
                    .Include(t => t.Stadium)
                    .Include(t => t.HomeMatches)
                        .ThenInclude(m => m.AwayTeam)
                    .Include(t => t.AwayMatches)
                        .ThenInclude(m => m.HomeTeam)
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (team == null)
                {
                    // If the team with the specified ID is not found, return a 404 Not Found response
                    return NotFound();
                }

                // Ensure stadium and matches are not null
                if (team.Stadium == null)
                {
                    // Handle the case where the stadium is null
                    // For example, log the error or return an appropriate error response
                    return StatusCode(500, "Stadium not found for the team.");
                }

                if (team.HomeMatches == null)
                {
                    // Handle the case where home matches are null
                    // For example, log the error or return an appropriate error response
                    return StatusCode(500, "Home matches not found for the team.");
                }

                if (team.AwayMatches == null)
                {
                    // Handle the case where away matches are null
                    // For example, log the error or return an appropriate error response
                    return StatusCode(500, "Away matches not found for the team.");
                }

                // Pass the retrieved team, stadium, and matches to the view using ViewBag
                ViewBag.Team = team;
                ViewBag.Stadium = team.Stadium;
                ViewBag.Matches = team.HomeMatches.Concat(team.AwayMatches).ToList();

                // Return the view
                return View();
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the execution
                // For example, log the exception or return an appropriate error response
                return StatusCode(500, ex.Message);
            }
        }



    }
}
