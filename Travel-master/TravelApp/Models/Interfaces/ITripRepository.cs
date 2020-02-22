using System;
using System.Linq;
using TravelApp.Models.Coordinator;

namespace TravelApp.Models
{
    public interface ITripRepository
    {
        IQueryable<Trip> Trips { get; }
        IQueryable<Course> Courses { get; }
        IQueryable<Destination> Destinations { get; }
        IQueryable<TripQuestion> TripQuestions { get; }
        IQueryable<TripQuestionResponse> TripQuestionResponses { get;  }
        IQueryable<TripRoster> TripRosters { get; }
        IQueryable<AspNetUsers> AspNetUsers { get; }
        IQueryable<Application> Applications { get; }
        IQueryable<ApplicationForms> ApplicationForms { get; }
        IQueryable<Form> Forms { get; }

        void AddTrip(NewTripModel newTrip);
        void DeleteTrip(Trip trip);
        void DeleteTripQuestion(TripQuestion tripQuestion);
        void ApplyForTrip(Application application);
        void PrintFormSubmit(ApplicationForms applicationForms);
        void DeleteTripQuesionResponseIds(IQueryable<TripQuestionResponse> tqr);
        void ApproveForm(ApplicationForms applicationForms);
        void DenyForm(ApplicationForms applicationForms);
        void UndoFormAction(ApplicationForms applicationForms);
        void AddTripQuestion(TripQuestion tripQuestion);
        
        void AddPayment(Payment payment);
        void ConfirmPayment(Payment payment);
        void UpdatePayment(Payment payment);
    }


}
