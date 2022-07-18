using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataBaseLayer.UserModels
{
    public class UserModel
    {
        [Required]
        [DefaultValue("Firstname")]
        [RegularExpression("[A-Z]{1}[a-z]{3,}", ErrorMessage = "Please Enter for Firstname Atleast 5 character with first letter capital")]
        public string FirstName { get; set; }

        [Required]
        [DefaultValue("Lastname")]
        [RegularExpression("[A-Z]{1}[a-z]{3,}", ErrorMessage = "Please Enter for LastName Atleast 5 character with first letter capital")]
        public string LastName { get; set; }

        [Required]
        [DefaultValue("sample@gmail.com")]
        [RegularExpression("^([A-Za-z0-9]{3,20})([.][A-Za-z0-9]{1,10})*([@][A-Za-z]{2,5})+[.][A-Za-z]{2,3}([.][A-Za-z]{2,3})?$", ErrorMessage = "Please Enter Valid Email")]
        public string Email { get; set; }

        [Required]
        [DefaultValue("Password@123")]
        [RegularExpression("(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$_])[a-zA-Z0-9@#$_]{8,}", ErrorMessage = "Please Enter Atleast 8 character with Alteast one numeric,special character")]
        public string Password { get; set; }
    }
}
