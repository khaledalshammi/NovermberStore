using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Viewcomponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public CartViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = _db.Users.Find(userId);
            Cart cart = _db.Carts.FirstOrDefault(u => u.User == user && u.Available == true);
            if (cart.TotalQuantity > 0)
            {
                return View(cart.TotalQuantity);
            }
            else
            {
                return View(0);
            }
        }
    }
}
