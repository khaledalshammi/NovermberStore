using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using WebApplication3.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string? Address { get; set; }

    }
}