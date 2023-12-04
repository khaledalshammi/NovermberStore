using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlTypes;

namespace WebApplication3.Models.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> categoryList { get; set; }
        public List<Gender> genderList { get; set; }
    }
}
