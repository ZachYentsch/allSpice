using System.ComponentModel.DataAnnotations;

namespace allSpice.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Subtitle { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string CreatorId { get; set; }

        public Profile Chef { get; set; }

    }
}