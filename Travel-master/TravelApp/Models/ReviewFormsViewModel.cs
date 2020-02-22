using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    public class ReviewFormsViewModel
    {
        public ReviewFormsViewModel(Payment pay, Application app, IEnumerable<Form> forms, IEnumerable<ApplicationForms> appForms, IQueryable<TripQuestion> tripQuestions,IQueryable<TripQuestionResponse>tripQuestionResponses,IQueryable<Payment> payment)
        {
            Pay = pay;
            App = app;
            Forms = forms;
            AppForms = appForms;
            TripQuestions = tripQuestions;
            TripQuestionResponses = tripQuestionResponses;
            Payment = payment;

        }
        public Payment Pay { get; set; }
        public Application App { get; set; }
        public IEnumerable<Form> Forms { get; set; }
        public IEnumerable<ApplicationForms> AppForms { get; set; }
        public IQueryable<TripQuestion> TripQuestions { get; set; }
        public IQueryable<TripQuestionResponse> TripQuestionResponses { get; set; }
        public IQueryable<Payment> Payment { get; set; }
        
    }
}
