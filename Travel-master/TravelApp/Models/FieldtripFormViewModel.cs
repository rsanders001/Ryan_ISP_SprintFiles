using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    public class FieldtripFormViewModel
    {
        public FieldtripFormViewModel(Trip trips, Destination destinations, Course courses)
        {
            Trip = trips;
            Destination = destinations;
            Course = courses;
        }

        public Trip Trip { get; set; }
        public Destination Destination { get; set; }
        public Course Course { get; set; }
    }
}
