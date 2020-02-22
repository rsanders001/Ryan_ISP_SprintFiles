using System;
using System.Collections.Generic;

namespace TravelApp.Models
{
    public partial class Course
    {
        public Course()
        {
            Trip = new HashSet<Trip>();
        }

        public int CourseId { get; set; }
        public string Crn { get; set; }
        public string CourseTitle { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<Trip> Trip { get; set; }
    }
}
