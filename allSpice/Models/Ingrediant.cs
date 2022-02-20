using System.ComponentModel.DataAnnotations;

namespace allSpice.Models
{
    public class Ingrediant
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int RecipeId { get; set; }
    }
}