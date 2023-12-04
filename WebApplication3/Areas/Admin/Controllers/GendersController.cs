using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GendersController : Controller
    {
        private readonly ApplicationDbContext _db;
        public GendersController(ApplicationDbContext db)
        {
            _db = db;


        }
        public IActionResult Index()
        {
            List<Gender> genders = _db.Genders.ToList();
            return View(genders);
        }
    }
}
