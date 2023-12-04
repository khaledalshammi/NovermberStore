using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models.ViewModels;
using System.Collections.Generic;
using WebApplication3.Data;

namespace WebApplication3.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CartController(ApplicationDbContext db)
        {
            _db = db;

        }
        [HttpPost]
        public IActionResult AddToCart(int PID)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = _db.Users.Find(userId);
            Cart cart = _db.Carts.FirstOrDefault(i => i.UserId == user.Id && i.Available == true);
            Product product = _db.Products.Find(PID);
            ShoppingCart shoppingCart = _db.ShoppingCarts.FirstOrDefault(i => i.ProductId == product.Id && i.User.Id == user.Id && i.Cart.Id == cart.Id);
            if (product != null && shoppingCart == null)
            {
                if (product.Quantity > 1)
                {
                    ShoppingCart newshoppingCart = new ShoppingCart()
                    {
                        User = user,
                        Cart = cart,
                        Product = product,
                        ProductId = product.Id,
                        Quantity = 1,
                        TotalPrice = product.Price
                    };
                    _db.ShoppingCarts.Add(newshoppingCart);
                    _db.SaveChanges();
                    cart.ShoppingCarts.Add(newshoppingCart);
                    cart.TotalQuantity += 1;
                    cart.TotalPrice += product.Price;
                    _db.Carts.Update(cart);
                    _db.SaveChanges();
                    return RedirectToAction("Details", "Home", new { id = product.Id });
                }
            }
            return RedirectToAction("Details", "Home", new { id = product.Id });
        }
        public IActionResult Cart(string valid)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = _db.Users.Find(userId);
            Cart cart = _db.Carts.Include(c => c.ShoppingCarts).FirstOrDefault(i => i.UserId == user.Id && i.Available == true);
            List<ShoppingCart> ourshoppingCart = _db.ShoppingCarts.Include(c => c.Product).Where(i => i.Cart == cart && i.User == user).ToList();

            int totalQuantity = 0;
            double totalPrice = 0;
            foreach (ShoppingCart shoppingCart in ourshoppingCart)
            {
                int quantity = 0;
                if (shoppingCart.Product.Quantity < shoppingCart.Quantity)
                {
                    quantity += shoppingCart.Product.Quantity;
                }
                else
                {
                    quantity += shoppingCart.Quantity;
                }

                if (quantity > 0)
                {
                    shoppingCart.Quantity = quantity;
                    shoppingCart.TotalPrice = quantity * shoppingCart.Product.Price;
                    _db.ShoppingCarts.Update(shoppingCart);
                    _db.SaveChanges();
                    totalQuantity += shoppingCart.Quantity;
                    totalPrice += shoppingCart.TotalPrice;
                }
                else
                {
                    _db.ShoppingCarts.Remove(shoppingCart);
                    _db.SaveChanges();
                }
            }
            cart.TotalQuantity = totalQuantity;
            cart.TotalPrice = totalPrice;
            _db.Carts.Update(cart);
            _db.SaveChanges();
            CartVM cartVM = new CartVM()
            {
                Cart = cart,
                ShoppingCarts = _db.ShoppingCarts.Include(i => i.Product)
                .Where(i => i.Cart == cart && i.User == user).ToList(),
                User = user
            };
            return View(cartVM);
        }

        public IActionResult Plus(int PID)
        {
            if (PID != 0 || PID != null)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                User user = _db.Users.Find(userId);
                Cart cart = _db.Carts.FirstOrDefault(i => i.UserId == user.Id && i.Available == true);
                Product product = _db.Products.Find(PID);
                if (product != null)
                {
                    ShoppingCart shoppingCart = _db.ShoppingCarts.FirstOrDefault(i => i.ProductId == PID && i.Cart == cart && user == user);
                    shoppingCart.Quantity += 1;
                    shoppingCart.TotalPrice += product.Price;
                    _db.ShoppingCarts.Update(shoppingCart);
                    _db.SaveChanges();
                    return RedirectToAction("Cart");
                }
                return RedirectToAction("Cart");
            }
            return RedirectToAction("Cart");
        }
        public IActionResult Minus(int PID)
        {
            if (PID != 0 || PID != null)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                User user = _db.Users.Find(userId);
                Cart cart = _db.Carts.FirstOrDefault(i => i.UserId == user.Id && i.Available == true);
                Product product = _db.Products.Find(PID);
                if (product != null)
                {
                    ShoppingCart shoppingCart = _db.ShoppingCarts.FirstOrDefault(i => i.ProductId == PID && i.Cart == cart && user == user);
                    if (shoppingCart.Quantity == 1)
                    {
                        _db.ShoppingCarts.Remove(shoppingCart);
                        _db.SaveChanges();
                        return RedirectToAction("Cart");
                    }
                    else
                    {
                        shoppingCart.Quantity -= 1;
                        shoppingCart.TotalPrice -= product.Price;
                        _db.ShoppingCarts.Update(shoppingCart);
                        _db.SaveChanges();
                        return RedirectToAction("Cart");
                    }
                }
                return RedirectToAction("Cart");
            }
            return RedirectToAction("Cart");
        }
        public IActionResult Delete(int PID)
        {
            if (PID != 0 || PID != null)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                User user = _db.Users.Find(userId);
                Cart cart = _db.Carts.FirstOrDefault(i => i.UserId == user.Id && i.Available == true);
                Product product = _db.Products.Find(PID);
                if (product != null)
                {
                    ShoppingCart shoppingCart = _db.ShoppingCarts.FirstOrDefault(i => i.ProductId == PID && i.Cart == cart && user == user);
                    _db.ShoppingCarts.Remove(shoppingCart);
                    _db.SaveChanges();
                    return RedirectToAction("Cart");
                }
                return RedirectToAction("Cart");
            }
            return RedirectToAction("Cart");
        }
    }
}
