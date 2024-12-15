using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace JobSystem.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class CompanyAccount
    {
        [Key]
        public int CompanyID { get; set; }

        [Required(ErrorMessage = "Company name is required.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Industry is required.")]
        public string Industry { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Company website is required.")]
        public string CompanyWebsite { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string CompanyDescription { get; set; }
    }
}
