namespace project_new.Dtos
{
    public class TeamDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? LogoUrl { get; set; }
        public int ManagerId { get; set; }
        public int CaptainId { get; set; }
        public int StadiumId { get; set; }

        // You can include only necessary navigation properties if needed
        public string ManagerName { get; set; }
        public string CaptainName { get; set; }
        public string StadiumName { get; set; }
    }

}
