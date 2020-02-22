using System.Linq;
using TravelApp.Models.Coordinator;

namespace TravelApp.Models
{
    public class EFTripRepository : ITripRepository
    {
        private StudentTravelDocsContext context;

        public EFTripRepository(StudentTravelDocsContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Trip> Trips => context.Trip;

        public IQueryable<Course> Courses => context.Course;

        public IQueryable<Destination> Destinations => context.Destination;

        public IQueryable<TripRoster> TripRosters => context.TripRoster;

        public IQueryable<AspNetUsers> AspNetUsers => context.AspNetUsers;

        public IQueryable<Application> Applications => context.Application;

        public IQueryable<ApplicationForms> ApplicationForms => context.ApplicationForms;
        public IQueryable<TripQuestion> TripQuestions => context.TripQuestion;
        public IQueryable<TripQuestionResponse> TripQuestionResponses => context.TripQuestionResponse;

        public IQueryable<Form> Forms => context.Form;

        public void AddTrip(NewTripModel newTrip)
        {
            //to make sure things are related
            //the "main tables goes last"

            //first make the objects that connect from the foreign keys
            //ie., the Trip table has a foreign key to a course object

            Course course = new Course();
            if (newTrip.Crn != null && newTrip.CourseTitle != null)
            {
                course.CourseTitle = newTrip.CourseTitle;
                course.Crn = newTrip.Crn;
                course.ModifiedDate = newTrip.ModifiedDate;

                //then add
                context.Course.Add(course);
            }

            //same thing destination
            Destination destination = new Destination()
            {
                Name = newTrip.Destination,
                AddressLine1 = newTrip.AddressLine1,
                City = newTrip.City,
                State = newTrip.State,
                PostalCode = newTrip.PostalCode,
                Country = newTrip.Country,
                ModifiedDate = newTrip.ModifiedDate
            };
            context.Destination.Add(destination);

            //TODO: Flight table

            //and now the Trip table
            Trip trip = new Trip()
            {
                //Non nullable fields
                TripName = newTrip.TripName,
                EducationalObjective = newTrip.EducationalObjective,
                StudentOrganization = newTrip.StudentOrganization,
                AccountNumber = newTrip.AccountNumber,
                DateDeparting = newTrip.DateDeparting,
                DateReturning = newTrip.DateReturning,
                ModifiedDate = newTrip.ModifiedDate,
                //add more fields, which are nullable...
                FundsAvailable = newTrip.FundsAvailable,
                TeachingSubNeeded = newTrip.TeachingSubNeeded,
                SubNoOfHours = newTrip.SubNoOfHours,
                CreatedBy = newTrip.CreatedBy,

                //now that the Couse table has been populated, call the Course object created and get its ID
                CourseId = course.CourseId,
                //same for destination
                DestinationId = destination.DestinationId

                //TODO: add Flight table
            };

            //lastly add trip
            context.Trip.Add(trip);

            //FINALLY save
            context.SaveChanges();

            foreach (var Question in newTrip.Questions)
            {
                TripQuestion TripQuestion = new TripQuestion()
                {
                    QuestionText = Question,
                    //Temp code to ensure it worked. Will fix later		
                    TripId = trip.TripId
                    //needs to be something like -> TripId = tripQuestion.TripId		
                };
                context.TripQuestion.Add(TripQuestion);
            }

            context.SaveChanges();
        }

        public void DeleteTrip(Trip trip)
        {
            context.Trip.Remove(trip);
            context.SaveChanges();
        }
        public void DeleteTripQuestion(TripQuestion tripQuestion)
        {
            context.TripQuestion.Remove(tripQuestion);
            context.SaveChanges();
        }

        public void ApplyForTrip(Application application)
        {
            context.Application.Add(application);
            context.SaveChanges();
        }

        public void PrintFormSubmit(ApplicationForms appForm)
        {
            //change status then update
            appForm.FormStatusId = 2;
            context.ApplicationForms.Update(appForm);
            context.SaveChanges();
        }

        public void ApproveForm(ApplicationForms appForm)
        {
            //change status then update
            appForm.FormStatusId = 3;
            context.ApplicationForms.Update(appForm);
            context.SaveChanges();
        }

        public void DenyForm(ApplicationForms appForm)
        {
            //change status then update
            appForm.FormStatusId = 4;
            context.ApplicationForms.Update(appForm);
            context.SaveChanges();
        }
        public void DeleteTripQuesionResponseIds(IQueryable<TripQuestionResponse> tqr)
        {
            foreach (var tq in tqr)
            {
                context.Remove(tqr);
                context.SaveChanges();
            }
        }
        public void UndoFormAction(ApplicationForms appForm)
        {
            appForm.FormStatusId = 2;
            context.ApplicationForms.Update(appForm);
            context.SaveChanges();
        }
        public void AddTripQuestion(TripQuestion tripQuestion)
        {
            tripQuestion.QuestionText = "test";
            context.TripQuestion.Update(tripQuestion);
            context.SaveChanges();
        }

        public void AddPayment(Payment payment)
        {
            context.Payment.Update(payment);
            context.SaveChanges();
        }
        public void ConfirmPayment(Payment payment)
        {
            payment.PaymentConfirmed = true;
            //payment.StudentPaid = true;
            context.Update(payment);
            context.SaveChanges();
        }
        public void UpdatePayment(Payment payment)
        {
            context.Payment.Update(payment);
            context.SaveChanges();
        }
    }
}
