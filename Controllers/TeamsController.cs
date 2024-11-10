using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
            ViewData["CaptainId"] = new SelectList(_context.Captain, "Id", "Id");
            ViewData["ManagerId"] = new SelectList(_context.Manager, "Id", "Id");
            ViewData["CaptainId"] = new SelectList(_context.Stadium, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,LogoUrl,ManagerId,CaptainId,StadiumId")] Team team, IFormFile logoFile)
        {
            if (ModelState.IsValid)
            {
                // Check if a file is uploaded
                if (logoFile != null && logoFile.Length > 0)
                {
                    // Generate a unique file name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(logoFile.FileName);

                    // Save the file to wwwroot/Images directory
                    string filePath = Path.Combine(_configuration.GetSection("FileManagement:SystemFileUploads").Value, fileName);


                    // Save the file if it passed the malware scan
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await logoFile.CopyToAsync(fileStream);
                    }

                    // Set the file URL in the team model
                    team.LogoUrl = fileName;
                }

                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CaptainId"] = new SelectList(_context.Captain, "Id", "Id", team.CaptainId);
            ViewData["ManagerId"] = new SelectList(_context.Manager, "Id", "Id", team.ManagerId);
            return View(team);
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
            ViewData["CaptainId"] = new SelectList(_context.Captain, "Id", "Id", team.CaptainId);
            ViewData["ManagerId"] = new SelectList(_context.Manager, "Id", "Id", team.ManagerId);
            ViewData["CaptainId"] = new SelectList(_context.Stadium, "Id", "Id", team.CaptainId);
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,LogoUrl,ManagerId,CaptainId,StadiumId")] Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
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
            ViewData["CaptainId"] = new SelectList(_context.Captain, "Id", "Id", team.CaptainId);
            ViewData["ManagerId"] = new SelectList(_context.Manager, "Id", "Id", team.ManagerId);
            ViewData["CaptainId"] = new SelectList(_context.Stadium, "Id", "Id", team.CaptainId);
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
            var team = await _context.Teams.FindAsync(id);
            if (team != null)
            {
                _context.Teams.Remove(team);
            }

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
