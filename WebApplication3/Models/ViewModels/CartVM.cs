using Stripe;
using WebApplication3.Models;

namespace WebApplication3.Models.ViewModels
{
    public class CartVM
    {
        public Cart Cart { get; set; }
        public List<ShoppingCart> ShoppingCarts { get; set; }
        public User User { get; set; }

    }
}
