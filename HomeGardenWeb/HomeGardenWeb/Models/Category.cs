using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeGardenWeb.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int category_id { get; set; }
        public string category_name { get; set; }
    }

}
