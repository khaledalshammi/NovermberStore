using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Category type")]
        public string Type { get; set; }
    }
}
