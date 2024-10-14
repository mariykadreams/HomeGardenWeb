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
        [Display(Name = "Price")]
        [Range(0.01, 10000, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Category")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Frequency Name is required")]
        [Display(Name = "Frequency Name")]
        public string FrequencyName { get; set; }

        [Required(ErrorMessage = "Water Volume is required")]
        [Display(Name = "Water Volume")]
        [Range(0.01, 10000, ErrorMessage = "Water Volume must be greater than 0")]
        public int WaterVolume { get; set; }

        [Display(Name = "Watering Interval Days")]
        [Required(ErrorMessage = "Watering Interval is required")]
        [Range(1, 365, ErrorMessage = "Watering Interval must be greater than 0")]
        public int WateringIntervalDays { get; set; }

        public string Notes { get; set; }
    }

}
