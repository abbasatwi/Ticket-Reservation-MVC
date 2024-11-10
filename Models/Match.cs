using System.ComponentModel.DataAnnotations.Schema;

namespace project_new.Models
{
    public class Match
    {
        public int Id { get; set; }
        //[ForeignKey("HomeTeam"), Column(Order = 0)]
        public int? HomeTeamId { get; set; }
        //[ForeignKey("GuestTeam"), Column(Order = 1)]
        public int? AwayTeamId { get; set; }
        public string? MatchUrl { get; set; }
        public DateTime? MatchDate { get; set; }
        public int StadiumId { get; set; }
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
