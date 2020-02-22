using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelApp.Models
{
    public class SignatureFormViewModel
    {
        public int TripId { get; set; }
        public string TripName { get; set; }
        public string StudentOrganization { get; set; }
        public Course Course { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateDeparting { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateReturning { get; set; }
        public Destination Destination { get; set; }
        public string EducationalObjective { get; set; }
        public int ApplicationFormId { get; set; }


        public ApplicationForms AppForm { get; set; }
        public Trip Trip { get; set; }
        public List<ApplicationForms> AppFormList { get; set; }
        public Payment Payment { get; set; }
        public Application Application { get; internal set; }
    }
}
