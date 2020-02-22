using System;
using System.Collections.Generic;

namespace TravelApp.Models
{
    public partial class FormStatus
    {
        public FormStatus()
        {
            ApplicationForms = new HashSet<ApplicationForms>();
        }

        public int FormStatusId { get; set; }
        public string Status { get; set; }

        public ICollection<ApplicationForms> ApplicationForms { get; set; }
    }
}
