using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models.ViewModels;
using WebApplication3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebApplication3.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;

        }
      
        public IActionResult Index()
        {
            List<Product> products = _db.Products.Include(u => u.Category).ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                categoryList = _db.Categories.ToList().Select(i => new SelectListItem
                {
                    Text = i.Type,
                    Value = i.Id.ToString()
                }),

                genderList = _db.Genders.ToList()
            };
            return View(productVM);
        }
        [HttpPost]
        public IActionResult Create(ProductVM products, List<int>? GenderIds, IFormFile? image)
        {
            foreach (int i in GenderIds)
            {
                Gender gender = _db.Genders.Find(i);
                products.Product.Genders.Add(gender);
            }
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (image != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string categoryPath = Path.Combine(wwwRootPath, @"images/products");
                    if (!Directory.Exists(categoryPath))
                        Directory.CreateDirectory(categoryPath);
                    if (!string.IsNullOrEmpty(products.Product.Images))
                    {
                        // delete the old image
                        var oldImagePath = Path.Combine(wwwRootPath, products.Product.Images.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(categoryPath, fileName), FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }
                    products.Product.Images = @"images/products/" + fileName;
                }
                _db.Products.Add(products.Product);
            _db.SaveChanges();


            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _db.Products.FirstOrDefault(u => u.Id == id);

            if (productFromDb == null)
            {
                return NotFound();
            }
            ProductVM productVM = new ProductVM()
            {
                Product = productFromDb,
                categoryList = _db.Categories.ToList().Select(i => new SelectListItem
                {
                    Text = i.Type,
                    Value = i.Id.ToString()
                }),

                genderList = _db.Genders.ToList()
            };
            return View(productVM);
        }

      
     
        [HttpPost]
        public IActionResult Edit(ProductVM obj, int id, List<int>? GenderIds, IFormFile? image)
        {
            Product product = _db.Products.Find(id);
            foreach (int i in GenderIds)
            {
                Gender gender = _db.Genders.Find(i);
                obj.Product.Genders.Add(gender);
            }

            product.Type = obj.Product.Type;
            product.Price = obj.Product.Price;
            product.Material = obj.Product.Material;
            product.Color = obj.Product.Color;
            product.Size = obj.Product.Size;
            product.Description = obj.Product.Description;
            product.CategoryId = obj.Product.CategoryId;
            product.Genders = obj.Product.Genders;
            product.Quantity = obj.Product.Quantity;


            if (image != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                string categoryPath = Path.Combine(wwwRootPath, @"images/products");
                if (!Directory.Exists(categoryPath))
                    Directory.CreateDirectory(categoryPath);
                if (!string.IsNullOrEmpty(product.Images))
                {
                    // delete the old image
                    var oldImagePath = Path.Combine(wwwRootPath, product.Images.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStream = new FileStream(Path.Combine(categoryPath, fileName), FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
                product.Images = @"images/products/" + fileName;
            }

            _db.Products.Update(product);
            TempData["success"] = "Category updated successfully";
            _db.SaveChanges();
            return RedirectToAction("Index");
            return View();

        }

       
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _db.Products.FirstOrDefault(u => u.Id == id);

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? obj = _db.Products.FirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            if (obj.Images != null)
            {
                var oldImagePath =
                   Path.Combine(_webHostEnvironment.WebRootPath,
                   obj.Images.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            _db.Products.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "تم حذف العنصر بنجاح";
            return RedirectToAction("Index");
        }

    }
}