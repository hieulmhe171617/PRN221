using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineMedicine.Models
{
    public partial class Account
    {
        public Account()
        {
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public decimal Wallet { get; set; }
        public int RoleId { get; set; }
        public string Address { get; set; } = null!;
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number must contain only digits.")]
        [StringLength(10, ErrorMessage = "Phone number cannot be longer than 10 digits.")]
        public string PhoneNumber { get; set; } = null!;

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
