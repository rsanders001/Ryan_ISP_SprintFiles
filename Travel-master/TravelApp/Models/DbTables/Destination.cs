using System;
using System.Collections.Generic;

namespace TravelApp.Models
{
    public partial class Destination
    {
        public Destination()
        {
            Trip = new HashSet<Trip>();
        }

        public int DestinationId { get; set; }
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<Trip> Trip { get; set; }
    }
}
