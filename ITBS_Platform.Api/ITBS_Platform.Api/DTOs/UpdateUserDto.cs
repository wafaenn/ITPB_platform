using System.ComponentModel.DataAnnotations;

namespace ITBS_Platform.Api.DTOs
{
    public class UpdateUserDto
    {
        [StringLength(50)]
        public string Nom { get; set; }
        [StringLength(50)]
        public string Prenom { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(100, MinimumLength = 3)]
        public string MotDePasse { get; set; }
        public int RoleId { get; set; }
    }

}
