using System;
using System.Collections.Generic;

namespace TravelApp.Models
{
    public partial class Flight
    {
        public Flight()
        {
            Trip = new HashSet<Trip>();
        }

        public int FlightId { get; set; }
        public string FlightNumber { get; set; }
        public string Airline { get; set; }
        public string AirportCode { get; set; }
        public DateTime DepartDate { get; set; }
        public TimeSpan DepartTime { get; set; }
        public DateTime ReturnDate { get; set; }
        public TimeSpan ReturnTime { get; set; }
        public decimal Cost { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<Trip> Trip { get; set; }
    }
}
