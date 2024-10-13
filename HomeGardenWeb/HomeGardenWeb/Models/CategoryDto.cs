using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeGardenWeb.Models
{
    [Table("Categories")]
    public class CategoryDto
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string category_name { get; set; }
    }
}
