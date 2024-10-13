using System.ComponentModel.DataAnnotations;

namespace HomeGardenWeb.Models
{
    public class PlantsDto
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 10000, ErrorMessage = "Price must be greater than 0")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Category ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Category")]
        [Display(Name = "Category ID")]
        public int CategoryId { get; set; }
    }
}
