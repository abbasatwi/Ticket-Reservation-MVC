using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_new.Models
{
    public class Favorites
    {
        public int Id { get; set; }

        // Foreign key for User
        public string UserId { get; set; }
        public  IdentityUser User { get; set; }
        public ICollection<Match>? Matches { get; set; }
    }
}

