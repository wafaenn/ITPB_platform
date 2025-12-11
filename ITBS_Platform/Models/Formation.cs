namespace ITBS_Platform.Models
{
    public class Formation
{
    public int Id { get; set; }
    public string Titre { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }
    public int FormateurId { get; set; }
    public User Formateur { get; set; } = null!;

    public ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();
}
}