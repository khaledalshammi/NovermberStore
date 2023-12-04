using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication3.Models;

namespace WebApplication3.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; }
        public Cart Cart { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public double TotalPrice { get; set; } = 00.00;
    }
}
