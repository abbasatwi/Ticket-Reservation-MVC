using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using project_new.Data;
using project_new.Models;
using QRCoder;
using Stripe;
using Stripe.Checkout;

namespace project_new.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private int _ticketId;
        private int _quantity;

        public TicketsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ticket.Include(t => t.Match);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .Include(t => t.Match)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            ViewData["MatchId"] = new SelectList(
                _context.Match.Select(m => new
                {
                    m.Id,
                    Display = $"{m.HomeTeam.Name} vs {m.AwayTeam.Name} - {m.MatchDate:yyyy-MM-dd}"
                }),
                "Id", "Display");

            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MatchId,TicketStatus,Quantity,Price,Section,QRCode")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Re-populate the MatchId dropdown
            ViewData["MatchId"] = new SelectList(
                _context.Match.Select(m => new
                {
                    m.Id,
                    Display = $"{m.HomeTeam.Name} vs {m.AwayTeam.Name} - {m.MatchDate:yyyy-MM-dd}"
                }),
                "Id",
                "Display",
                ticket.MatchId);

            return View(ticket);
        }


        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            ViewData["MatchId"] = new SelectList(
                _context.Match.Select(m => new
                {
                    m.Id,
                    Display = $"{m.HomeTeam.Name} vs {m.AwayTeam.Name} - {m.MatchDate:yyyy-MM-dd}"
                }),
                "Id", "Display", ticket.MatchId);

            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MatchId,TicketStatus,Quantity,Price,Section,QRCode")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
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
            ViewData["MatchId"] = new SelectList(
_context.Match.Select(m => new
{
    m.Id,
    Display = $"{m.HomeTeam.Name} vs {m.AwayTeam.Name} - {m.MatchDate:yyyy-MM-dd}"
}),
"Id", "Display", ticket.MatchId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .Include(t => t.Match)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket != null)
            {
                _context.Ticket.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.Id == id);
        }


        public async Task<IActionResult> Checkout(int ticketId, int quantity)
        {
            TempData["TicketId"] = ticketId;
            TempData["Quantity"] = quantity;

            // Get the ticket details based on the ticketId
            var ticket = await _context.Ticket.FindAsync(ticketId);

            if (ticket == null)
            {
                return NotFound();
            }

            // Get the logged-in user's email
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            // Define the success and cancel URLs
            var domain = "https://localhost:7226/";
            var successUrl = $"{domain}Tickets/TransactionConfirmation?ticketId={ticketId}&quantity={quantity}";
            var cancelUrl = domain;

            // Create the session options
            var options = new SessionCreateOptions
            {
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
        {
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = "Ticket",
                    },
                    UnitAmount = (long)(ticket.Price * 100), // Stripe requires amount in cents
                },
                Quantity = quantity,
            },
        },
                Mode = "payment",
                CustomerEmail = userEmail // Pass the customer's email
            };

            // Create the session
            var service = new SessionService();
            var session = await service.CreateAsync(options);

            // Redirect the user to the checkout session URL
            return Redirect(session.Url);
        }



        public async Task<IActionResult> TransactionConfirmation(int ticketId, int quantity)
        {
            // Get the logged-in user's email
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            // Retrieve ticket from the database
            var ticket = await _context.Ticket.FindAsync(ticketId);

            if (ticket == null)
            {
                return NotFound();
            }

            // Calculate total price
            var totalPrice = ticket.Price * quantity;

            // Update ticket status based on quantity
            if (ticket.Quantity == 0)
            {
                ticket.TicketStatus = "Unavailable";
            }
            else
            {
                ticket.TicketStatus = "Available";
            }

            // Update ticket information in the database
            _context.Update(ticket);

            // Create transaction record
            var transaction = new Transaction
            {
                TicketId = ticketId,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                DateTime = DateTime.Now,
                TotalPrice = totalPrice
            };

            // Add transaction to the database
            _context.Transaction.Add(transaction);

            // Decrease ticket quantity
            ticket.Quantity -= quantity;

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Get match and ticket details asynchronously
            var matchAndTicketDetails = await ticket.GetMatchAndTicketDetailsAsync(_context);

            // Send email confirmation
            string fromMail = "medianeozeir@gmail.com";
            string fromPassword = "cbqgzejodxgnyeiz";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Checkout Confirmation";
            message.To.Add(new MailAddress(userEmail));
            message.Body = $"<html><body> Thank you for your order. <br> {matchAndTicketDetails} </body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp-mail.outlook.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);

            return View();
        }

    }
}