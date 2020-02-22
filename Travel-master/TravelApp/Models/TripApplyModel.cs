using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    //viewmodel for ApplyForTrip
    //includes fields relating to Trip, Application, Student, and User
    public class TripApplyModel
    {
        public TripApplyModel()
        { }

        [Key]
        public int ApplicationId { get; set; }
        public string StudentId { get; set; }
        public int TripId { get; set; }
        [DataType(DataType.Date)]
        public DateTime ApplicationDate { get; set; }
        public decimal Gpa { get; set; }
        public string Rationale { get; set; }
        public int? AppStatusId { get; set; }
        public int AcademicStatusId { get; set; }
        [DataType(DataType.Date)]
        public DateTime ModifiedDate { get; set; }

        public IQueryable<TripQuestion> TripQuestions { get; set; }

        public AppUser User { get; set; }
        public Student Student { get; set; }
        public Trip Trip { get; set; }

    }
}
