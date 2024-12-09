using System.ComponentModel.DataAnnotations;

namespace project_new.Models
{
    public class Stadium
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Capacity is required.")]
        [Range(1, float.MaxValue, ErrorMessage = "Capacity must be a positive number.")]
        public float Capacity { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }

        public string? PicUrl { get; set; }

        public string? BackPicUrl { get; set; }
    }
}
