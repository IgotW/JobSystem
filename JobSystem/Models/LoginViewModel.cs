using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JobSystem.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        //[EmailAddress(ErrorMessage = "Please Enter Valid Email.")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
