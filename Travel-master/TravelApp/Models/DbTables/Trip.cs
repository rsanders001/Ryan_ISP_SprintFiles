using System;
using System.Collections.Generic;

namespace TravelApp.Models
{
    public partial class Trip
    {
        public Trip()
        {
            Application = new HashSet<Application>();
            Payment = new HashSet<Payment>();
            TripRoster = new HashSet<TripRoster>();
        }

        public int TripId { get; set; }
        public string TripName { get; set; }
        public string StudentOrganization { get; set; }
        public DateTime? DateDeparting { get; set; }
        public DateTime? DateReturning { get; set; }
        public bool? TripApproved { get; set; }
        public string EducationalObjective { get; set; }
        public int? DestinationId { get; set; }
        public bool? RecommendationNeeded { get; set; }
        public bool? TeachingSubNeeded { get; set; }
        public byte? SubNoOfHours { get; set; }
        public int? CourseId { get; set; }
        public int? TransportationId { get; set; }
        public int? FlightId { get; set; }
        public decimal? CostToDistrict { get; set; }
        public bool? FundsAvailable { get; set; }
        public string AccountNumber { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }

        public Course Course { get; set; }
        public Destination Destination { get; set; }
        public Flight Flight { get; set; }
        public ICollection<Application> Application { get; set; }
        public ICollection<Payment> Payment { get; set; }
        public ICollection<TripRoster> TripRoster { get; set; }

    }
}
