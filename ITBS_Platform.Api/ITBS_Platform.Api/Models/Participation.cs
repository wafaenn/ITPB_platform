using ITBS_Platform.Models;

namespace ITBS_Platform.Api.Models
{
    public class Participation
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int? EventId { get; set; }
        public Event Event { get; set; }

        public int? CourseId { get; set; }
        public Course Course { get; set; }

        public DateTime DateInscription { get; set; }

        public string Type { get; set; } // "Event" ou "Course"

        public string Statut { get; set; } = "En attente";
    }

}
