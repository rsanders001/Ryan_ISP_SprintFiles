using System;
using System.Collections.Generic;

namespace TravelApp.Models
{
    public partial class Application
    {
        public Application()
        {
            ApplicationForms = new HashSet<ApplicationForms>();
        }

        public int ApplicationId { get; set; }
        public string StudentId { get; set; }
        public int TripId { get; set; }
        public DateTime ApplicationDate { get; set; }
        public decimal Gpa { get; set; }
        public string Rationale { get; set; }
        public int? AppStatusId { get; set; }
        public int AcademicStatusId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public AcademicStatus AcademicStatus { get; set; }
        public ApplicationStatus AppStatus { get; set; }
        public Student Student { get; set; }
        public Trip Trip { get; set; }
        public ICollection<ApplicationForms> ApplicationForms { get; set; }
    }
}
