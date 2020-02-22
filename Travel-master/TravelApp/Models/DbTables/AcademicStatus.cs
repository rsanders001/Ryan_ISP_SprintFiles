using System;
using System.Collections.Generic;

namespace TravelApp.Models
{
    public partial class AcademicStatus
    {
        public AcademicStatus()
        {
            Application = new HashSet<Application>();
        }

        public int AcademicStatusId { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<Application> Application { get; set; }
    }
}
