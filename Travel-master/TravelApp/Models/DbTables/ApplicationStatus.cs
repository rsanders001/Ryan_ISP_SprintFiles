using System;
using System.Collections.Generic;

namespace TravelApp.Models
{
    public partial class ApplicationStatus
    {
        public ApplicationStatus()
        {
            Application = new HashSet<Application>();
        }

        public int AppStatusId { get; set; }
        public string StatusName { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<Application> Application { get; set; }
    }
}
