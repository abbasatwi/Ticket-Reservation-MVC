using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_new.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public Ticket? Ticket { get; set; }
        public string UserId { get; set; } // Changed from int to string for UserId
        [ForeignKey("UserId")]
        public IdentityUser? User { get; set; }
        public DateTime DateTime { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalPrice { get; set; }

    }
}
