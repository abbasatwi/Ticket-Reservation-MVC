using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_new.Models
{
    public class Match
    {
        public int Id { get; set; }
        //[ForeignKey("HomeTeam"), Column(Order = 0)]
        [Required(ErrorMessage = "Home team is required.")]
        public int? HomeTeamId { get; set; }
        //[ForeignKey("GuestTeam"), Column(Order = 1)]
        [Required(ErrorMessage = "Away team is required.")]
        public int? AwayTeamId { get; set; }
        public string? MatchUrl { get; set; }
        [Required(ErrorMessage = "Match date is required.")]
        [DataType(DataType.Date)]
        public DateTime? MatchDate { get; set; }
        [Required(ErrorMessage = "Stadium is required.")]
        public int StadiumId { get; set; }
        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        [ForeignKey("StadiumId")]
        public Stadium? Stadium { get; set; }
        public Team? HomeTeam { get; set; }
        public Team? AwayTeam { get; set; }

        public List<Ticket>? Tickets { get; set; }
        public ICollection<Favorites>? Favorites { get; set; }
    }
}
