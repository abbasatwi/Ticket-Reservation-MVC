using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace project_new.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public string Country { get; set; }

        [ForeignKey("ParentCategoryId")]
        [JsonIgnore]
        public Category? ParentCategory { get; set; }
        public List<Category>? SubCategories { get; set; }
        public List<Match>? Matches { get; set; }
    }
}
