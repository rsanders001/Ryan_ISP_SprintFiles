using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    public class StudentTripViewModel
    {
        public StudentTripViewModel(IEnumerable<Trip> trips, IEnumerable<Application> apps, IEnumerable<Payment> payment, IEnumerable<ApplicationForms> applicationForms) {
            Trips = trips;
            Apps = apps;
            Payment = payment;
            ApplicationForms = applicationForms;
        }

        public IEnumerable<Trip> Trips { get; set; }
        public IEnumerable<Application> Apps { get; set; }
        public IEnumerable<Payment> Payment { get; set; }
        public IEnumerable<ApplicationForms> ApplicationForms { get; set; }


    }
}
