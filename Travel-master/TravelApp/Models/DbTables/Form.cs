using System;
using System.Collections.Generic;

namespace TravelApp.Models
{
    public partial class Form
    {
        public Form()
        {
            ApplicationForms = new HashSet<ApplicationForms>();
        }

        public int FormId { get; set; }
        public string Name { get; set; }

        public ICollection<ApplicationForms> ApplicationForms { get; set; }
    }
}
