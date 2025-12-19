using System.ComponentModel.DataAnnotations;

namespace ITBS_Platform.Api.DTOs
{
    public class RegisterDto
    {
        [Required]
        [StringLength(50)]
        public string Nom { get; set; }

        [Required]
        [StringLength(50)]
        public string Prenom { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string MotDePasse { get; set; }

        public int RoleId { get; set; }
    }

}
