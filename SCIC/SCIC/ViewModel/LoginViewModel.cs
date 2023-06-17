using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SCIC.ViewModel
{
    public class LoginViewModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required.!!")]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The Password is require.!!")]
        public string? Password { get; set; }

        [Display(Name = "Remember Me ?")]
        public bool RememberMe { get; set; }
    }
}
