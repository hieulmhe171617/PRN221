﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OnlineMedicine.ViewModels
{
    public class RegisterModel
    {
        [Required, MinLength(5), MaxLength(50)]
        public string Username { get; set; }
        [Required, MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, MinLength(6)]
        [DataType(DataType.Password)]
        [Display(Name = "Repeat Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string RepeatPassword { get; set; }
        [Required]
        public string Address { get; set; }
        [Required, MinLength(8), MaxLength(10)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
