using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project_new.Data;
using project_new.Models;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace project_new.Controllers
{
    [Authorize]
    public class StadiumController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public StadiumController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Stadium
        public async Task<IActionResult> Index()
        {
            return View(await _context.Stadium.ToListAsync());
        }
        // GET: Stadium for user
        public IActionResult IndexUser()
        {
            return View("IndexUser");
        }

        // GET: Stadium/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stadium = await _context.Stadium
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stadium == null)
            {
                return NotFound();
            }

            return View(stadium);
        }
        [Authorize(Roles = "Admin")]
        // GET: Stadium/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Capacity,Location,PicUrl,BackPicUrl")] Stadium stadium, IFormFile picFile, IFormFile backPicFile)
        {
            var path = $"{_configuration.GetSection("FileManagement:SystemFileUploads").Value}";

            // Early validation for file upload
            if (picFile == null && string.IsNullOrEmpty(stadium.PicUrl))
            {
                ModelState.AddModelError("PicUrl", "The Picture file is required.");
            }

            if (backPicFile == null && string.IsNullOrEmpty(stadium.BackPicUrl))
            {
                ModelState.AddModelError("BackPicUrl", "The Background Picture file is required.");
            }

            // Return view if model state is invalid
            if (!ModelState.IsValid)
            {
                return View(stadium);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // File processing for PicUrl
                    if (picFile != null && picFile.Length > 0)
                    {
                        string picFileName = Guid.NewGuid().ToString() + Path.GetExtension(picFile.FileName);
                        string picFilePath = Path.Combine(path, picFileName);

                        if (!ScanFile(picFile))
                        {
                            ModelState.AddModelError(string.Empty, "Malware detected in the uploaded PicUrl file.");
                            return View(stadium);
                        }

                        using (var fileStream = new FileStream(picFilePath, FileMode.Create))
                        {
                            await picFile.CopyToAsync(fileStream);
                        }

                        stadium.PicUrl = picFileName;
                    }

                    // File processing for BackPicUrl
                    if (backPicFile != null && backPicFile.Length > 0)
                    {
                        string backPicFileName = Guid.NewGuid().ToString() + Path.GetExtension(backPicFile.FileName);
                        string backPicFilePath = Path.Combine(path, backPicFileName);

                        if (!ScanFile(backPicFile))
                        {
                            ModelState.AddModelError(string.Empty, "Malware detected in the uploaded BackPicUrl file.");
                            return View(stadium);
                        }

                        using (var fileStream = new FileStream(backPicFilePath, FileMode.Create))
                        {
                            await backPicFile.CopyToAsync(fileStream);
                        }

                        stadium.BackPicUrl = backPicFileName;
                    }

                    // Save the stadium entity to the database
                    _context.Add(stadium);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the stadium: " + ex.Message);
                    return View(stadium);
                }
            }

            return View(stadium);
        }



        // Helper method to scan the file for malware
        private bool ScanFile(IFormFile file)
        {
            // Implement the malware scanning logic using Windows Defender Antivirus API or command-line tool
            // This example uses the Windows Defender Antivirus command-line tool as shown in the previous response
            // Ensure appropriate permissions are set to execute the tool and access files
            // Return true if the file is clean, false if malware is detected

            // Example implementation:
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c MpCmdRun.exe -Scan -ScanType 3 -File \"{file.FileName}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = Process.Start(processStartInfo);
            process.WaitForExit();
            var output = process.StandardOutput.ReadToEnd();
            return !output.Contains("has been detected as malware");
        }


        // Helper method to check if the uploaded file is an image file
        private bool IsImageFile(IFormFile file)
        {
            if (file.ContentType.ToLower() != "image/jpeg" && file.ContentType.ToLower() != "image/png")
            {
                return false;
            }
            // You can also check file size here if needed
            // For example:
            // if (file.Length > 5 * 1024 * 1024) // 5MB
            // {
            //     return false;
            // }
            return true;
        }

        [Authorize(Roles = "Admin")]
        // GET: Stadium/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stadium = await _context.Stadium.FindAsync(id);
            if (stadium == null)
            {
                return NotFound();
            }
            return View(stadium);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Capacity,Location,PicUrl,BackPicUrl")] Stadium stadium, IFormFile picFile, IFormFile backPicFile)
        {
            if (id != stadium.Id)
            {
                return NotFound();
            }

            // Early validation for file upload
            if (picFile == null && string.IsNullOrEmpty(stadium.PicUrl))
            {
                ModelState.AddModelError("PicUrl", "The Picture file is required.");
            }

            if (backPicFile == null && string.IsNullOrEmpty(stadium.BackPicUrl))
            {
                ModelState.AddModelError("BackPicUrl", "The Background Picture file is required.");
            }

            // Return view if model state is invalid
            if (!ModelState.IsValid)
            {
                return View(stadium);
            }

            try
            {
                // Check if a file is uploaded for PicUrl
                if (picFile != null && picFile.Length > 0)
                {
                    string picFileName = Guid.NewGuid().ToString() + Path.GetExtension(picFile.FileName);
                    string picFilePath = Path.Combine(_configuration.GetSection("FileManagement:SystemFileUploads").Value, picFileName);

                    if (!ScanFile(picFile))
                    {
                        ModelState.AddModelError(string.Empty, "Malware detected in the uploaded PicUrl file.");
                        return View(stadium);
                    }

                    using (var fileStream = new FileStream(picFilePath, FileMode.Create))
                    {
                        await picFile.CopyToAsync(fileStream);
                    }

                    stadium.PicUrl = picFileName;
                }

                // Check if a file is uploaded for BackPicUrl
                if (backPicFile != null && backPicFile.Length > 0)
                {
                    string backPicFileName = Guid.NewGuid().ToString() + Path.GetExtension(backPicFile.FileName);
                    string backPicFilePath = Path.Combine(_configuration.GetSection("FileManagement:SystemFileUploads").Value, backPicFileName);

                    if (!ScanFile(backPicFile))
                    {
                        ModelState.AddModelError(string.Empty, "Malware detected in the uploaded BackPicUrl file.");
                        return View(stadium);
                    }

                    using (var fileStream = new FileStream(backPicFilePath, FileMode.Create))
                    {
                        await backPicFile.CopyToAsync(fileStream);
                    }

                    stadium.BackPicUrl = backPicFileName;
                }

                _context.Update(stadium);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StadiumExists(stadium.Id))
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


        [Authorize(Roles = "Admin")]
        // GET: Stadium/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stadium = await _context.Stadium
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stadium == null)
            {
                return NotFound();
            }

            return View(stadium);
        }

        // POST: Stadium/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stadium = await _context.Stadium.FindAsync(id);
            if (stadium == null)
            {
                return NotFound();
            }

            // Check if the stadium is referenced by any team
            bool isReferencedByTeam = await _context.Teams.AnyAsync(t => t.StadiumId == id);

            // Check if the stadium is referenced by any match
            bool isReferencedByMatch = await _context.Match.AnyAsync(m => m.StadiumId == id);

            if (isReferencedByTeam || isReferencedByMatch)
            {
                // Add an error message to the model state
                ModelState.AddModelError(string.Empty, "This stadium cannot be deleted because it is referenced by a team or a match.");
                return View(stadium); // Return the same view with the error message
            }

            // If no references exist, proceed with deletion
            _context.Stadium.Remove(stadium);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool StadiumExists(int id)
        {
            return _context.Stadium.Any(e => e.Id == id);
        }

        public async Task<IActionResult> StadiumMatches(int id)
        {
            var stadium = await _context.Stadium.FindAsync(id);

            if (stadium == null)
            {
                return NotFound();
            }

            var matches = await _context.Match
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Where(m => m.StadiumId == id)
                .ToListAsync();

            ViewBag.Stadium = stadium;
            ViewBag.Matches = matches;

            return View();
        }



    }
}