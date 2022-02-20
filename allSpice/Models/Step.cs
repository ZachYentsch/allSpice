using System.ComponentModel.DataAnnotations;

namespace allSpice.Models
{
    public class Step
    {
        public int Id { get; set; }

        [Required]
        public int NumberedSteps { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public int RecipeId { get; set; }
        public string creatorId { get; set; }


    }
}