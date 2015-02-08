using System;
using System.ComponentModel.DataAnnotations;

namespace BeyondThemes.BeyondAdmin.Models
{
    public class LoginModel
    {
        [Display(Name = "Email", Order = 1)]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [DataType(DataType.Password)]
        [Display(Order = 2)]
        public string Password { get; set; }

        [Display(Name = "Remember me next time.", Order = 3)]
        public bool RememberMe { get; set; }

    }
}