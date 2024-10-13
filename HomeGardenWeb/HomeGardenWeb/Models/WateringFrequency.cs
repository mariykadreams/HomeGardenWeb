using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeGardenWeb.Models
{
    [Table("WateringFrequency")]
    public class WateringFrequency
    {
        [Key]
        public int frequency_id { get; set; }

        [Required]
        [Display(Name = "Frequency Name")]
        public string frequency_name { get; set; }

        [Required]
        [Display(Name = "Water Volume (ml)")]
        public int water_volume { get; set; }

        [Required]
        [Display(Name = "Watering Interval (days)")]
        public int watering_interval_days { get; set; }

        [Display(Name = "Notes")]
        public string notes { get; set; }

        [ForeignKey("Plant")]
        [Display(Name = "Plant ID")]
        public int plant_id { get; set; }

        public Plants Plant { get; set; }
    }
}
