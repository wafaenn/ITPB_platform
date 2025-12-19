namespace ITBS_Platform.Api.DTOs
{
    public class UpdateCourseDto
    {
        public string Titre { get; set; }
        public string Description { get; set; }
        public DateTime DateDebut { get; set; }
        public int FormateurId { get; set; }
    }
}
