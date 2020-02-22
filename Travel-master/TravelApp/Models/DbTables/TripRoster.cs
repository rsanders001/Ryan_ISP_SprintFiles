using System;
using System.Collections.Generic;

namespace TravelApp.Models
{
    public partial class TripRoster
    {
        public int TripRosterId { get; set; }
        public string PersonId { get; set; }
        public int TripId { get; set; }

        public AspNetUsers Person { get; set; }
        public Trip Trip { get; set; }
    }
}
