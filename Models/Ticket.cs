using Microsoft.AspNetCore.Identity;
using project_new.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace project_new.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public Match? Match { get; set; }
        public string TicketStatus { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public string Section { get; set; }
        public byte[]? QRCode { get; set; }

        public async Task<string> GetMatchAndTicketDetailsAsync(ApplicationDbContext _context)
        {
            var ticket = await _context.Ticket.FindAsync(this.Id);
            if (ticket != null)
            {
                var match = await _context.Match.FindAsync(ticket.MatchId);
                if (match != null)
                {
                    var stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine($"Match Details:");
                    stringBuilder.AppendLine($"Home Team: {match.HomeTeam?.Name}");
                    stringBuilder.AppendLine($"Away Team: {match.AwayTeam?.Name}");
                    stringBuilder.AppendLine($"Match Date: {match.MatchDate}");
                    stringBuilder.AppendLine($"Stadium: {match.Stadium?.Name}");
                    stringBuilder.AppendLine($"Location: {match.Stadium?.Location}");
                    stringBuilder.AppendLine($"Match URL: {match.MatchUrl}");

                    stringBuilder.AppendLine($"Ticket Details:");
                    stringBuilder.AppendLine($"Ticket Status: {ticket.TicketStatus}");
                    stringBuilder.AppendLine($"Quantity: {ticket.Quantity}");
                    stringBuilder.AppendLine($"Price: {ticket.Price:C}");
                    stringBuilder.AppendLine($"Section: {ticket.Section}");

                    return stringBuilder.ToString();
                }
            }
            return string.Empty;
        }
    }
}
