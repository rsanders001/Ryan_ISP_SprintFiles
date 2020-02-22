using System;
using System.Collections.Generic;

namespace TravelApp.Models
{
    public partial class Student
    {
        public Student()
        {
            Application = new HashSet<Application>();
            Payment = new HashSet<Payment>();
        }

        public string StudentId { get; set; }
        public int? ProgramId { get; set; }
        public DateTime? BirthDate { get; set; }
        public string ExpectedGradMonth { get; set; }
        public short? ExpectedGradYear { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Program Program { get; set; }
        public AspNetUsers StudentNavigation { get; set; }
        public ICollection<Application> Application { get; set; }
        public ICollection<Payment> Payment { get; set; }
    }
}
