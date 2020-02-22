using System;
using System.Collections.Generic;

namespace TravelApp.Models
{
    public partial class Program
    {
        public Program()
        {
            Student = new HashSet<Student>();
        }

        public int ProgramId { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<Student> Student { get; set; }
    }
}
