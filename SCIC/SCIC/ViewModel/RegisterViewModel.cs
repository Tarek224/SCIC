using Microsoft.AspNetCore.Mvc;
using SCIC.Models;
using System.ComponentModel.DataAnnotations;

namespace SCIC.ViewModel
{
    public class RegisterViewModel
    {

        [EmailAddress]
        [Required(ErrorMessage = "Email is required.!!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Name is required.!!")]
        [Remote(action: "CheckName", controller: "Account", ErrorMessage = "Name is required.!!")]
        [RegularExpression(@"^[a-zA-Z0-9ء-ي''-,!'\s]{1,50}$", ErrorMessage = "Invalid Characters.!!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "DateOfBirth is required.!!"), DataType(DataType.Date)]
        [Remote(action: "CheckDate", controller: "Account", ErrorMessage = "Incorrect Date.!!")]
        public DateTime? DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public UserType UserType { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The Password is required.!!")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "The Confirm Password is require.!!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The Password and Confirmation Password doesn't match.!!")]
        public string? ConfirmPassword { get; set; }
    }
}
