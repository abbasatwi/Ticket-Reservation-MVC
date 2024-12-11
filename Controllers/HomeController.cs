using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using project_new.Data;
using project_new.Models;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using project_new.Dtos;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;


namespace project_new.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IDistributedCache _cache;
        

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IDistributedCache cache)
        {
            _logger = logger;
            _context = context;
            _cache = cache;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("IndexAdmin");
            }

            // Fetch matches from Redis or database
            var matchesJson = await _cache.GetStringAsync("Matches");
            List<MatchDTO> matches;
            if (matchesJson == null)
            {
                _logger.LogInformation("Redis cache missed for 'Matches'. Fetching from the database.");

                matches = await _context.Match
                    .Include(m => m.HomeTeam)
                    .Include(m => m.AwayTeam)
                    .Include(m => m.Stadium)
                    .Include(m => m.Category)
                    .Take(8)
                    .Select(m => new MatchDTO
                    {
                        Id = m.Id,
                        MatchUrl = m.MatchUrl,
                        HomeTeamName = m.HomeTeam.Name,
                        AwayTeamName = m.AwayTeam.Name,
                        MatchDate = m.MatchDate,
                        CategoryName = m.Category.Name,
                        StadiumLocation = m.Stadium.Location
                    })
                    .ToListAsync();

                // Cache matches for 5 minutes
                await _cache.SetStringAsync("Matches", System.Text.Json.JsonSerializer.Serialize(matches), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }
            else
            {
                _logger.LogInformation("Data retrieved from Redis cache for 'Matches'.");
                matches = System.Text.Json.JsonSerializer.Deserialize<List<MatchDTO>>(matchesJson);
            }

            // Fetch teams from Redis or database
            var teamsJson = await _cache.GetStringAsync("Teams");
            List<TeamDTO> teams;
            if (teamsJson == null)
            {
                _logger.LogInformation("Redis cache missed for 'Teams'. Fetching from the database.");

                teams = await _context.Teams
                    .Include(t => t.Manager)
                    .Include(t => t.Captain)
                    .Include(t => t.Stadium)
                    .Select(t => new TeamDTO
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Description = t.Description,
                        LogoUrl = t.LogoUrl,
                        ManagerName = t.Manager.Name,
                        CaptainName = t.Captain.Name,
                        StadiumName = t.Stadium.Name
                    })
                    .Take(8)
                    .ToListAsync();

                // Cache teams for 5 minutes
                await _cache.SetStringAsync("Teams", System.Text.Json.JsonSerializer.Serialize(teams), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }
            else
            {
                _logger.LogInformation("Data retrieved from Redis cache for 'Teams'.");
                teams = System.Text.Json.JsonSerializer.Deserialize<List<TeamDTO>>(teamsJson);
            }

            // Fetch categories from Redis or database
            var categoriesJson = await _cache.GetStringAsync("Categories");
            List<CategoryDTO> categories;
            if (categoriesJson == null)
            {
                _logger.LogInformation("Redis cache missed for 'Categories'. Fetching from the database.");

                categories = await _context.Category
                    .Where(c => c.Name == "Champions League" || c.Name == "LaLiga" || c.Name == "Premier League")
                    .Select(c => new CategoryDTO
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .ToListAsync();

                // Cache categories for 5 minutes
                await _cache.SetStringAsync("Categories", System.Text.Json.JsonSerializer.Serialize(categories), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }
            else
            {
                _logger.LogInformation("Data retrieved from Redis cache for 'Categories'.");
                categories = System.Text.Json.JsonSerializer.Deserialize<List<CategoryDTO>>(categoriesJson);
            }

            // Pass data to ViewBag
            ViewBag.Matches = matches;
            ViewBag.Teams = teams;
            ViewBag.Categories = categories;

            return View(matches);
        }





        //         var santiago = _context.Stadium.FirstOrDefault(s => s.Name == "Santiago Bernabeu");
        //var campnou = _context.Stadium.FirstOrDefault(s => s.Name == "Camp Nou");
        //var allianz = _context.Stadium.FirstOrDefault(s => s.Name == "Allianz Arena");
        //var anfield = _context.Stadium.FirstOrDefault(s => s.Name == "Anfield");
        //var wembley = _context.Stadium.FirstOrDefault(s => s.Name == "Wembley");
        //var parcdesprinces = _context.Stadium.FirstOrDefault(s => s.Name == "Parc Des Princes");
        //var oldtrafford = _context.Stadium.FirstOrDefault(s => s.Name == "Old Trafford");



        //// Pass the stadiums to the view using ViewBag
        //ViewBag.Stadiums = new List<Stadium> { santiago, campnou, allianz, anfield, wembley, parcdesprinces, oldtrafford };

        public async Task<IActionResult> IndexAdminAsync()
        {
            var model = new AdminDashboard
            {
                StadiumCount = await _context.Stadium.CountAsync(),
                TeamCount = await _context.Teams.CountAsync(),
                TicketCount = await _context.Ticket.CountAsync(),
                MatchCount = await _context.Match.CountAsync(),
                CategoryCount = await _context.Category.CountAsync(),
                CaptainCount = await _context.Captain.CountAsync(),
                ManagerCount = await _context.Manager.CountAsync()
            };

            return View(model);
        }

        public IActionResult Privacy()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}