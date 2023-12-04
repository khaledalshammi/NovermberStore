using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication3.Data;
using WebApplication3.Models;
using Stripe.Checkout;

namespace WebApplication3.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;
        public OrderController(ApplicationDbContext db)
        {
            _db = db;

        }
        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            List<Order> orders = _db.Orders.Where(i => i.Paid == true).Include(o => o.Cart.ShoppingCarts)
            .ThenInclude(sc => sc.Product).ToList();
            return View(orders);
        }
        [HttpGet]
        public IActionResult Order()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Order(Order order)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = _db.Users.FirstOrDefault(u => u.Id == userId);
            Cart cart = _db.Carts.Include(i => i.User).FirstOrDefault(i => i.UserId == user.Id && i.Available == true);
            Order myorder = _db.Orders.FirstOrDefault(u => u.Cart == cart);
            if (myorder == null)
            {
                order.OrderNumber = GenerateOrderNumber();
                order.Cart = cart;
                order.User = cart.User;
                order.Status = "Procussed";
                order.TotalPrice = cart.TotalPrice;
                _db.Orders.Add(order);
                _db.SaveChanges();
                return RedirectToAction("PayNow");
            }
            else
            {
                myorder.PhoneNumber = order.PhoneNumber;
                myorder.City = order.City;
                myorder.Address = order.Address;
                myorder.Email = order.Email;
                myorder.Cart = cart;
                myorder.TotalPrice = cart.TotalPrice;
                _db.Orders.Update(myorder);
                _db.SaveChanges();
                return RedirectToAction("PayNow");
            }
        }
        public IActionResult PayNow()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = _db.Users.FirstOrDefault(u => u.Id == userId);
            Cart cart = _db.Carts.Include(c => c.User).Include(c => c.ShoppingCarts).ThenInclude(sc => sc.Product)
           .FirstOrDefault(c => c.UserId == user.Id && c.Available == true);
            Order myorder = _db.Orders.FirstOrDefault(u => u.Cart == cart);
            //stripe logic
            var domain = "https://localhost:44337/";
            var options = new SessionCreateOptions
            {
                SuccessUrl = domain,
                CancelUrl = domain,
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
            };

            var totalAmountInCents = (long)(myorder.TotalPrice * 100);

            var sessionLineItem = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = totalAmountInCents,
                    Currency = "USD",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = cart.User.Name
                    }
                },
                Quantity = 1
            };
            options.LineItems.Add(sessionLineItem);
            var service = new SessionService();
            Session session = service.Create(options);
            myorder.SessionId = session.Id;
            myorder.PaymentIntentId = session.PaymentIntentId;
            myorder.Status = "unpaid";
            _db.Orders.Update(myorder);
            _db.SaveChanges();

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);

        }
        public IActionResult PaymentConfirmation()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = _db.Users.FirstOrDefault(u => u.Id == userId);
            var cart = _db.Carts.Include(c => c.User).Include(c => c.ShoppingCarts).ThenInclude(sc => sc.Product)
            .FirstOrDefault(c => c.UserId == user.Id && c.Available == true);
            List<ShoppingCart> ourshoppingCart = _db.ShoppingCarts.Include(sc => sc.Product)
            .Where(sc => sc.Cart == cart && sc.User == user).ToList();

            Order myorder = _db.Orders.FirstOrDefault(u => u.Cart == cart);
            var service = new SessionService();
            Session session = service.Get(myorder.SessionId);
            if (session.PaymentStatus.ToLower() == "paid")
            {
                myorder.SessionId = session.Id;
                myorder.PaymentIntentId = session.PaymentIntentId;
                myorder.Status = "paid";
                myorder.Paid = true;
                myorder.PaidAt = DateTimeOffset.Now;
                myorder.ExpectedArrival = DateTimeOffset.Now.AddDays(2);
                _db.Orders.Update(myorder);
                _db.SaveChanges();

                foreach (ShoppingCart shopping in ourshoppingCart)
                {
                    shopping.Product.Quantity -= shopping.Quantity;
                    _db.Products.Update(shopping.Product);
                    _db.SaveChanges();
                }
                cart.Available = false;
                _db.Carts.Update(cart);
                _db.SaveChanges();
                Cart newcart = new Cart()
                {
                    User = user,
                    Available = true,
                };
                _db.Carts.Add(newcart);
                _db.SaveChanges();
            }
            HttpContext.Session.Clear();
            return View();

        }

        [Area("Admin")]
        [Authorize(Roles = "Admin")]

        public IActionResult Arrive(int id)
        {
            Order order = _db.Orders.Include(c => c.Cart.ShoppingCarts).ThenInclude(sc => sc.Product).
            FirstOrDefault(i => i.Id == id);
            order.Arrived = true;
            order.ArrivedAt = DateTimeOffset.Now;
            _db.Orders.Update(order);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public static string GenerateOrderNumber()
        {
            var random = new Random();
            string orderId = "";
            for (int i = 0; i < 5; i++)
            {
                orderId += random.Next();
            }
            orderId += DateTime.Now.ToString("yyyyMMddHHmmss");
            for (int i = 0; i < 5; i++)
            {
                orderId += (char)random.Next('A', 'Z' + 1);
            }
            return orderId;
        }
    }
}
