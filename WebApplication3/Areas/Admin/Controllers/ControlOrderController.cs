using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Data;

namespace WebApplication3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ControlOrderController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ControlOrderController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(string search)
        {
            if (search != null && search == "paid")
            {
                var order = _db.Orders.Where(i => i.Status == search).ToList();
                return View(order);
            }
            if (search != null && search == "arrived")
            {
                var order = _db.Orders.Where(i => i.Status == search).ToList();
                return View(order);
            }
            if (search != null)
            {
                var order = _db.Orders.Where(i => i.OrderNumber.Contains(search)).ToList();
                return View(order);
            }
            var order1 = _db.Orders.ToList();
            return View(order1);
        }
        public IActionResult Arrived(int id)
        {
            var order = _db.Orders.Find(id);
            order.Status = "arrived";
            order.Arrived = true; 
            order.ArrivedAt = DateTimeOffset.Now;
            _db.Orders.Update(order);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
