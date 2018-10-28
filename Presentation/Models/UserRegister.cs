using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public class UserRegister
    {
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        [StringLength(100, MinimumLength = 1,
        ErrorMessage = "Firstname Should be minimum 1 characters and a maximum of 100 characters")]
        [DataType(DataType.Text)]
        public string Firstname { get; set; }


        [Required(ErrorMessage = "Lastame is required")]
        [StringLength(100, MinimumLength = 1,
        ErrorMessage = "Lastname Should be minimum 1 characters and a maximum of 100 characters")]
        [DataType(DataType.Text)]
        public string Lastname { get; set; }


        [Required]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$", ErrorMessage = "Password must be minimum eight characters and have at least one letter and one number")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}