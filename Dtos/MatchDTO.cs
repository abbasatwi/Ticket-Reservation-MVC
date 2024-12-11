namespace project_new.Dtos
{
    public class MatchDTO
    {
        public int Id { get; set; }
        public string MatchUrl { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public DateTime? MatchDate { get; set; }
        public string CategoryName { get; set; }
        public string StadiumLocation { get; set; }
    }
}
