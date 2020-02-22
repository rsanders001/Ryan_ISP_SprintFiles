using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    public class StudentListViewModel
    {
        //  technically should be tripDetailViewModel
            public StudentListViewModel(Trip trip, IEnumerable<AspNetUsers> users, IEnumerable<Application> apps, IEnumerable<ApplicationForms> appForms, IEnumerable<Payment>payment)

        {
            Trip = trip;
            Users = users;
            Apps = apps;
            ApplicationForms = appForms;
            Payment = payment;

        }

        public Trip Trip { get; set; }
        public IEnumerable<AspNetUsers> Users { get; set; }
        public IEnumerable<Application> Apps { get; set; }
        public IEnumerable<ApplicationForms> ApplicationForms { get; set; }
        public IEnumerable<Payment> Payment { get; set; }
    }

}
