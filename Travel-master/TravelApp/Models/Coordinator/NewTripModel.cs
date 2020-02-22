using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TravelApp.Models.Coordinator
{
    public class NewTripModel
    {
        public NewTripModel()
        { }
        public NewTripModel(Trip trip, Destination destination, Course course, List<String> tripQuestion) {
            TripId = trip.TripId;
            TripName = trip.TripName;
            EducationalObjective = trip.EducationalObjective;
            StudentOrganization = trip.StudentOrganization;
            TeachingSubNeeded = trip.TeachingSubNeeded;
            SubNoOfHours = trip.SubNoOfHours;
            CostToDistrict = trip.CostToDistrict;

            DestinationId = destination.DestinationId;
            Destination = destination.Name;
            AddressLine1 = destination.AddressLine1;
            City = destination.City;
            State = destination.State;
            PostalCode = destination.PostalCode;
            Country = destination.Country;

            Crn = course.Crn;
            CourseTitle = course.CourseTitle;

            Questions = tripQuestion;

        }

        //public int PersonID { get; set; }
        [Key]
        public int TripId { get; set; }
        public string TripName { get; set; }

        [DataType(DataType.Date)]
        public DateTime TripDate { get; set; }
        public string EducationalObjective { get; set; }
        public string StudentOrganization { get; set; }

        //destination
        public int DestinationId { get; set; }
        public string Destination { get; set; }
        public string AddressLine1 { get; set; }
        //public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        //teaching sub
        public bool? TeachingSubNeeded { get; set; }
        public byte? SubNoOfHours { get; set; }

        //course
        public string Crn { get; set; }
        public string CourseTitle { get; set; }

        //transportation
        public int? TransportationId { get; set; }

        //public int? FlightId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateDeparting { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateReturning { get; set; }

        //other info
        //public bool? RecommendationNeeded { get; set; }

        //accounting
        [DataType(DataType.Currency)]
        public decimal? CostToDistrict { get; set; }
        public bool FundsAvailable { get; set; }
        public string AccountNumber { get; set; }

        public string CreatedBy { get; set; }

        public string AccommodationInformation { get; set; }
        //TripQuestion
        //public int TripQuestionID { get; set; }
        //public string QuestionText { get; set; }
        public List<string> Questions { get; set; }
        public string MyListAsString
        {   
            get
            {
                return string.Join("\n", Questions);
            }
            set
            {
                Questions = value.Split(new char[] { '\n' }).Select(x => x.Trim()).ToList();
            }
        }
        //TripQuestionResponse
        public string Response { get; set; }

        //timestamp form submission
        public DateTime ModifiedDate = DateTime.Now;
    }
}
