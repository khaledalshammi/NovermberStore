using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Areas.Customer.Controllers
{

    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            var db = _db.Products.Include(i => i.Category).ToList();
            return View(db);
        }

        public IActionResult Details(int id)
        {
            var db = _db.Products.Include(i => i.Category).First(i => i.Id == id);
            if (User.Identity.IsAuthenticated)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                var user = _db.Users.Find(userId);
                Cart cart = _db.Carts.FirstOrDefault(i => i.UserId == user.Id && i.Available == true);
                ShoppingCart shoppingCart = _db.ShoppingCarts.FirstOrDefault(i => i.ProductId == id && i.User == user && i.Cart == cart);
                if (shoppingCart != null)
                {
                    ViewBag.exists = true;
                }
                else
                {
                    ViewBag.exists = false;
                }
            }
            return View(db);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
