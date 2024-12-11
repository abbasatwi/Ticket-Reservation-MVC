using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace project_new.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? LogoUrl { get; set; }
        public int ManagerId { get; set; }
        public int CaptainId { get; set; }
        public int StadiumId { get; set; }
        [JsonIgnore]
        public List<Match>? HomeMatches { get; set; }
        [JsonIgnore]
        public List<Match>? AwayMatches { get; set; }

        [ForeignKey("ManagerId")]
        public Manager? Manager { get; set; }
        [ForeignKey("CaptainId")]
        public Captain? Captain { get; set; }
        [ForeignKey("StadiumId")] // Corrected foreign key attribute
        public Stadium? Stadium { get; set; }
    }
}
