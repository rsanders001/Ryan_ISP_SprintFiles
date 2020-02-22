using System;
using System.Collections.Generic;

namespace TravelApp.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public string StudentId { get; set; }
        public int TripId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Boolean PaymentConfirmed { get; set; }
        //public Boolean StudentPaid { get; set; }
        public Student Student { get; set; }
        public Trip Trip { get; set; }
    }
}
