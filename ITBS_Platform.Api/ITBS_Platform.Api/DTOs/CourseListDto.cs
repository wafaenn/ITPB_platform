namespace ITBS_Platform.Api.DTOs
{
    public class CourseListDto
    {
        public int Id { get; set; }
        public string Titre { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime DateDebut { get; set; }

        public int FormateurId { get; set; }
        public string FormateurNom { get; set; } = ""; // affichage
    }
}
