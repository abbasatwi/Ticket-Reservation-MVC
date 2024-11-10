using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using project_new.Data;
using project_new.Models;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;


namespace project_new.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _context;
		private readonly HttpClient _client;
		Uri baseAddress = new Uri("http://localhost:5019/api/");
		public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
		{
			_logger = logger;
			_context = context;
			_client = new HttpClient();
			_client.BaseAddress = baseAddress;
		}

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("IndexAdmin");
            }

            // Fetch matches
            var matches = await _context.Match
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Stadium)
                .Include(m => m.Category)
                .Take(8)
                .ToListAsync();

            // Fetch teams directly from the database
            var teams = await _context.Teams.Take(8).ToListAsync();

            var categories = await _context.Category
                .Where(c => c.Name == "Champions League" || c.Name == "LaLiga" || c.Name == "Premier League")
                .ToListAsync();

            // Pass data to ViewBag
            ViewBag.Teams = teams;
            ViewBag.Matches = matches;
            ViewBag.TotalPages = 1;
            ViewBag.Categories = categories;

            return View(teams);
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
