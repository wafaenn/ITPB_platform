
namespace ITBS_Platform.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Admin, Formateur, Etudiant

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
