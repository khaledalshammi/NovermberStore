﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication3.Models;

namespace WebApplication3.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public User? User { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public double? TotalPrice { get; set; }
        public string OrderNumber { get; set; }
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }
        public bool Paid { get; set; } = false;
        public bool Arrived { get; set; } = false;
        public DateTimeOffset? PaidAt { get; set; }
        public DateTimeOffset? ArrivedAt { get; set; }
        public DateTimeOffset? ExpectedArrival { get; set; }
        public string? Status { get; set; }
        public int? CartId { get; set; }
        [ForeignKey("CartId")]
        [ValidateNever]
        public Cart? Cart { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
