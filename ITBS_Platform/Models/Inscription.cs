namespace ITBS_Platform.Models
{
    public class Inscription
    {
        public int Id { get; set; }
        public int FormationId { get; set; }
        public Formation Formation { get; set; } = null!;
        public int EtudiantId { get; set; }
        public User Etudiant { get; set; } = null!;
        public string Statut { get; set; } = "En attente"; // En attente / Approuvée / Refusée
        public DateTime DateInscription { get; set; } = DateTime.Now;
    }
}