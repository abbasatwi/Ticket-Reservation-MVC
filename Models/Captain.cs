namespace project_new.Models
{
    public class Captain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Team>? Teams { get; set; }
    }
}
