using System;
namespace ITBS_Platform.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public Role? Role { get; set; }

        // Un étudiant a plusieurs inscriptions

        public ICollection<Inscription> InscriptionsAsEtudiant { get; set; } = new List<Inscription>();
        public ICollection<Formation> FormationsAsFormateur { get; set; } = new List<Formation>();
    }
}
