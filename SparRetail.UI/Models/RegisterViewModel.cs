using System.ComponentModel.DataAnnotations;

namespace BeyondThemes.BeyondAdmin.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Family")]
        public string Family { get; set; }

        [Display(Name = "Month")]
        public string Month { get; set; }

        [Display(Name = "Year")]
        public string Year { get; set; }

        [Display(Name = "Day")]
        public string Day { get; set; }
    }
}