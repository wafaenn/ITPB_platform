using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITBS_Platform.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }

}
