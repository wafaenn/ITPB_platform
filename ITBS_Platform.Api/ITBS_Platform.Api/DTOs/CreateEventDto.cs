using System.ComponentModel.DataAnnotations;

namespace ITBS_Platform.Api.DTOs
{
    public class CreateEventDto
    {
        [Required]
        [StringLength(100)]
        public string Titre { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
