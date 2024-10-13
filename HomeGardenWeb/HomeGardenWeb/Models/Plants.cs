using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeGardenWeb.Models
{
    [Table("Plants")]
    public class Plants
    {
        [Key]
        public int plant_id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Price")]
        public decimal price { get; set; }

        [ForeignKey("Category")]
        [Display(Name = "Category ID")]
        public int category_id { get; set; }

        public Category Category { get; set; }
    }
}
