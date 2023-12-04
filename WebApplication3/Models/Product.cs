using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication3.Models;

namespace WebApplication3.Models
{
    public class Product
    {

        [Key]
        public int Id { get; set; }
        [DisplayName("Watch design")]
        public string Type { get; set; }
        [DisplayName("Watch matertial")]
        public string Material { get; set; }
        [DisplayName("Watch color")]
        public string Color { get; set; }
        [DisplayName("Watch price")]
        public double Price { get; set; }
        [DisplayName("Watch size")]
        public string Size { get; set; }
        [DisplayName("Available quantity")]
        public int Quantity { get; set; }
        [DisplayName("Watch description")]
        public string Description { get; set; }
        public List<Gender>? Genders { get; set; } = new List<Gender>();
        public string? Images { get; set; }
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

    }

}
