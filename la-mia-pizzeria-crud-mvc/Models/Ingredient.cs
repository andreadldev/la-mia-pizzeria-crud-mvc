using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_crud_mvc.Models
{
    [Table("Ingredients")]
    public class Ingredient
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        public List<Pizza>? Pizzas { get; set; }
    }
}
