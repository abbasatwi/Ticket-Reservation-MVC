namespace project_new.Models
{
    public class Stadium
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Capacity { get; set; }
        public string Location { get; set; }
        public string? PicUrl { get; set; }
        public string? BackPicUrl { get; set; }
    }
}
