using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Shop.Domain.Models
{
    //Model for DB
    public class Category
    {
        //Add properties to the model
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Category Name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1,500)]
        public int DisplayOrder { get; set; }
    }
}