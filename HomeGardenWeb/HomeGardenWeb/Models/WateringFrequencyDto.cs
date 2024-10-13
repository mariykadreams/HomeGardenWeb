using System.ComponentModel.DataAnnotations;

namespace HomeGardenWeb.Models
{
    public class WateringFrequencyDto
    {
        [Required(ErrorMessage = "Frequency Name is required")]
        [Display(Name = "Frequency Name")]
        public string FrequencyName { get; set; }

        [Required(ErrorMessage = "Water Volume is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Water Volume must be positive")]
        [Display(Name = "Water Volume (ml)")]
        public int WaterVolume { get; set; }

        [Required(ErrorMessage = "Watering Interval is required")]
        [Range(1, 365, ErrorMessage = "Watering Interval must be between 1 and 365 days")]
        [Display(Name = "Watering Interval (days)")]
        public int WateringIntervalDays { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "Plant ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Plant")]
        [Display(Name = "Plant ID")]
        public int PlantId { get; set; }
    }
}
