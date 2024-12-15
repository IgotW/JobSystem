using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace JobSystem.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class CandidateAccount
    {
        [Key]
        public int CandidateID { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Profession is required.")]
        public string Profession { get; set; }

        [Required(ErrorMessage = "Years is required.")]
        public string YearsofExperience { get; set; }

        [Required(ErrorMessage = "Summary is required.")]
        public string ProfessionSummary { get; set; }
    }
}
