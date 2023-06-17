using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCIC.Models
{
    #region Gender
    public enum Gender
    {
        Male,
        Female
    }
    #endregion

    #region UserType
    public enum UserType
    {
        Doctor,
        Patient
    }
    #endregion

    #region ApplicationUSer
    [Table(name: "ApplicationUser")]
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public Gender? Gender { get; set; }

        public UserType? UserType { get; set; }
    }
    #endregion
}
