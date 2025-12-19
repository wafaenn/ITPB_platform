using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITBS_Platform.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public DateTime DateDebut { get; set; }
        public int FormateurId { get; set; }
    }

}
