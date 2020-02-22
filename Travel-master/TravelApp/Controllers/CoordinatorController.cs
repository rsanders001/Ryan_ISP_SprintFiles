using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelApp.Models;
using TravelApp.Models.Coordinator;

namespace TravelApp.Controllers
{
    //comment to keep this open
    //only users with coordinator or admin permsissions may use these views
    [Authorize(Roles = "Coordinator,Admin")]
    public class CoordinatorController : Controller
    {
        private ITripRepository context;
        private StudentTravelDocsContext DbContext { get; }
        private UserManager<AppUser> userManager;
        private Task<AppUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

        public CoordinatorController(UserManager<AppUser> usrMgr, ITripRepository repository, StudentTravelDocsContext docsContext)
        {
            userManager = usrMgr;
            context = repository;
            DbContext = docsContext;
        }

        //private readonly TravelContext _context;
        //public CoordinatorController(TravelContext context) => _context = context;

        public IActionResult Index()
        {
            //find user for student object and viewbag
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AppUser user = userManager.Users.FirstOrDefault(u => u.Id == userId);
            ViewBag.User = user;

            //get user's student info for modified date in viewbag
            var student = DbContext.Student.FirstOrDefault(s => s.StudentId == user.Id);
            ViewBag.ModDate = student.ModifiedDate;
            ViewBag.DOB = student.BirthDate;

            //viewbag for timespan
            TimeSpan timeSpan = DateTime.Now.Subtract(student.ModifiedDate);
            ViewBag.TimeSpan = Math.Ceiling(timeSpan.TotalDays);

            ViewBag.uid = userId;
            return View(context.Trips.OrderBy(i => i.TripId));

        }

        // Returning page with all coordinator travel documents/forms
        public IActionResult CoordinatorDocsListView() {
            ViewBag.Id = context.Trips.FirstOrDefault();
            return View(); }
        public IActionResult ClubAdvisorTravelChecklist() { return View(); }
        public IActionResult TravelRequestForm() { return View(); }
        public async Task<IActionResult> FieldTripForm(int id)
        {
            AppUser user = await GetCurrentUserAsync();

            //Change id based on selected trip
            // need to access selected value from view and assign it's id to id
            Trip trip = context.Trips.FirstOrDefault(t => t.TripId == id);
            ViewBag.SelectedTrip = trip;
            ViewBag.Today = DateTime.Today;
            ViewBag.FirstName = user.FirstName;
            ViewBag.LastName = user.LastName;
            ViewBag.userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.Trips = context.Trips.OrderBy(i => i.TripId);
            Course course = context.Courses.FirstOrDefault(c => c.CourseId == trip.CourseId);
            Destination destination = context.Destinations.FirstOrDefault(d => d.DestinationId == trip.DestinationId);

            return View(new FieldtripFormViewModel(trip, destination, course));
        }
        public async Task<IActionResult> FieldTripForm2()
        {
            //currently getting the latest input into the database, may be a better selection this is just for test purposes and to show the page
            AppUser user = await GetCurrentUserAsync();

            //Change id based on selected trip
            // need to access selected value from view and assign it's id to id
            Trip trip = context.Trips.LastOrDefault();
            ViewBag.SelectedTrip = trip;
            ViewBag.Today = DateTime.Today;
            ViewBag.FirstName = user.FirstName;
            ViewBag.LastName = user.LastName;
            ViewBag.userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.Trips = context.Trips.OrderBy(i => i.TripId);
            Course course = context.Courses.FirstOrDefault(c => c.CourseId == trip.CourseId);
            Destination destination = context.Destinations.FirstOrDefault(d => d.DestinationId == trip.DestinationId);

            return View(new FieldtripFormViewModel(trip, destination, course));
        }
        public IActionResult RentalCarPreapprovalForm() { return View(); }
        public IActionResult TravelFundingVerification() {
            ViewBag.userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.Trips = context.Trips.OrderBy(i => i.TripId);
            return View(); }

        public IActionResult NewTrip(int id)
        {
            ViewBag.Today = DateTime.Now;
            if (id == 0)
            {
                //not coming from an old trip, just straight up new trip
                return View();
            }
            else
            {
                
                //get info from old trip and put it here
                //below is info needed for creating a trip
                        //trip table
                        Trip refTrip = context.Trips.FirstOrDefault(t => t.TripId == id);
                                //public int TripId { get; set; }
                                //public string TripName { get; set; }
                                //public DateTime TripDate { get; set; }
                                //public string EducationalObjective { get; set; }
                                //public string StudentOrganization { get; set; }
                                //public DateTime? DateDeparting { get; set; }
                                //public DateTime? DateReturning { get; set; }
                                //public decimal? CostToDistrict { get; set; }
                                //public bool FundsAvailable { get; set; }
                                //public string AccountNumber { get; set; }
                                //public bool? TeachingSubNeeded { get; set; }
                                //public byte? SubNoOfHours { get; set; }

                       //destination table
                        Destination refDestination = context.Destinations.FirstOrDefault(d => d.DestinationId == refTrip.DestinationId);
                                //public int DestinationId { get; set; }
                                //public string Destination { get; set; }
                                //public string AddressLine1 { get; set; }
                                ////public string AddressLine2 { get; set; }
                                //public string City { get; set; }
                                //public string State { get; set; }
                                //public string PostalCode { get; set; }
                                //public string Country { get; set; }
                                

                     //course
                           Course refCourse = context.Courses.FirstOrDefault(c => c.CourseId == refTrip.CourseId);
                //public string Crn { get; set; }
                //public string CourseTitle { get; set; }

                //transportation
                //public int? TransportationId { get; set; }
                ////public int? FlightId { get; set; }          doesnt exist



                ////other info                  doesnt exits
                ////public bool? RecommendationNeeded { get; set; } doesnt exit


                //public string CreatedBy { get; set; } auto set, not needed

                IEnumerable<TripQuestion> tripquestion = context.TripQuestions.Where(tq => tq.TripId == refTrip.TripId);
                List<String> tripQuestion = new List<String>();
                foreach (var question in tripquestion)
                {
                    tripQuestion.Add(question.QuestionText);
                }
                
                return View(new NewTripModel(refTrip, refDestination, refCourse, tripQuestion));
            }
        }
      
        [HttpPost]
        public async Task<IActionResult> NewTrip(NewTripModel newTrip)
        {
            if (ModelState.IsValid)
            {
                AppUser usr = await GetCurrentUserAsync();
                newTrip.CreatedBy = usr.Id;
                context.AddTrip(newTrip);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Trips()
        {
            //get userID to pass to ViewBag
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.uid = userId;
            return View(context.Trips.OrderBy(i => i.TripId));
        }

        //controller to access trip by ID
        public IActionResult TripDetail(int id)
        {

            //find the corresponding trip in the DB
            Trip trip = context.Trips.FirstOrDefault(t => t.TripId == id);
            //then course
            Course course = context.Courses.FirstOrDefault(c => c.CourseId == trip.CourseId);
            //then destination
            Destination destination = context.Destinations.FirstOrDefault(d => d.DestinationId == trip.DestinationId);
            AspNetUsers user = context.AspNetUsers.FirstOrDefault();

            //get user ID and determine if it is in Trip
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId == trip.CreatedBy)
            {
                ViewBag.IsCreator = true;
            }
            else
            {
                ViewBag.IsCreator = false;
            }

            //get user's UserRoles, users can have multiple roles
            IEnumerable<AspNetUserRoles> userRoles = DbContext.AspNetUserRoles.Where(u => u.UserId == userId);
            //get role names
            string[] roles = new string[userRoles.Count()];
            for (var i = 0; i < userRoles.Count(); i++)
            {
                roles[i] = DbContext.AspNetRoles.FirstOrDefault(r => r.Id == userRoles.ElementAt(i).RoleId).Name;
            }

            //if in an admin role, add to viewbag
            bool isAdmin = false;
            for (var i = 0; i < roles.Length; i++)
            {
                if (roles[i] == "Admin")
                {
                    isAdmin = true;
                }
            }
            ViewBag.IsAdmin = isAdmin;
            //get payment list form tripId
            IEnumerable<Payment> payment = DbContext.Payment.Where(p => p.TripId == id);


            /*  DateTime departDate = trip.DateDeparting ?? trip.ModifiedDate ?? DateTime.Now;
              DateTime threeWeeksBefore = departDate.AddDays(-21); */
            //get list of applications by tripID
            IEnumerable<Application> applications = context.Applications.Where(a => a.TripId == id);

            //iterate through rosters for students, accumulate found matches in users list
            IList<AspNetUsers> users = new List<AspNetUsers>();

            if (applications.Count() > 0)
            {
                foreach (var app in applications)
                {
                    users.Add(context.AspNetUsers.FirstOrDefault(u => u.Id == app.StudentId));
                }
            }
            //viewbags
            /*  ViewBag.Id = id;
              ViewBag.DueDate = threeWeeksBefore;*/
            ViewBag.Course = course;
            ViewBag.Destination = destination;

            //Find all application forms related to the application & trip
            IEnumerable<ApplicationForms> appForms = context.ApplicationForms.Where(af => af.Application.TripId == id);

            return View(new StudentListViewModel(trip, users, applications, appForms, payment));
        }

        //delete trip by tripId
        public async Task<IActionResult> DeleteTrip(int id)
        {
            //find the corresponding trip in the DB
            Trip trip = context.Trips.FirstOrDefault(t => t.TripId == id);
            IQueryable<TripQuestion> tripQuestions = context.TripQuestions.Where(q => q.TripId == id);
            Payment payment = DbContext.Payment.FirstOrDefault(p => p.TripId == id);
            //TripQuestion tq = context.TripQuestions.First(q => q.TripId == id);
            //get curent user
            AppUser user = await GetCurrentUserAsync();
            //get user's UserRoles, users can have multiple roles
            IEnumerable<AspNetUserRoles> userRoles = DbContext.AspNetUserRoles.Where(u => u.UserId == user.Id);
            //get role names
            string[] roles = new string[userRoles.Count()];
            for (var i = 0; i < userRoles.Count(); i++)
            {
                roles[i] = DbContext.AspNetRoles.FirstOrDefault(r => r.Id == userRoles.ElementAt(i).RoleId).Name;
            }

            //delete if user is the creator of the trip or has an Admin role
            bool canDelete = false;
            for (var i = 0; i < roles.Length; i++)
            {
                if (user.Id == trip.CreatedBy || roles[i] == "Admin")
                {
                    canDelete = true;
                }
            }

            if (canDelete)
            {
                //remvoe all trip question
                //foreach (TripQuestion tq in tripQuestions)
                //{
                //    DbContext.TripQuestion.Remove(tq);

                //}
                ////remove the payment associated
                //DbContext.Payment.Remove(payment);

                //DbContext.SaveChanges();
                context.DeleteTrip(trip);

            }

            return RedirectToAction("Index");
        }

        //Car Rental Form
        public IActionResult RentalForm(int id)
        {
            //find the corresponding trip in the DB
            Trip trip = context.Trips.FirstOrDefault(t => t.TripId == id);
            return View(trip);
        }

        //Funding Form
        public IActionResult FundingForm(int id)
        {
            //find the corresponding trip in the DB
            Trip trip = context.Trips.FirstOrDefault(t => t.TripId == id);
            //then destination
            Destination destination = context.Destinations.FirstOrDefault(d => d.DestinationId == trip.DestinationId);

            //viewbags
            ViewBag.Id = id;
            ViewBag.Destination = destination;

            return View(trip);
        }

        //get applications by tripId
        public IActionResult StudentList(int id)
        {
            //store trip id to viewbag
            ViewBag.id = id;

            //get trip by tripID
            Trip trip = context.Trips.FirstOrDefault(t => t.TripId == id);
            //get payment list form tripId
            IEnumerable<Payment> payment = DbContext.Payment.Where(p => p.TripId == id);

            //get user ID and determine if it is in Trip
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId == trip.CreatedBy)
            {
                ViewBag.IsCreator = true;
            }
            else
            {
                ViewBag.IsCreator = false;
            }

            //get user's UserRoles, users can have multiple roles
            IEnumerable<AspNetUserRoles> userRoles = DbContext.AspNetUserRoles.Where(u => u.UserId == userId);
            //get role names
            string[] roles = new string[userRoles.Count()];
            for (var i = 0; i < userRoles.Count(); i++)
            {
                roles[i] = DbContext.AspNetRoles.FirstOrDefault(r => r.Id == userRoles.ElementAt(i).RoleId).Name;
            }

            //if in an admin role, add to viewbag
            bool isAdmin = false;
            for (var i = 0; i < roles.Length; i++)
            {
                if (roles[i] == "Admin")
                {
                    isAdmin = true;
                }
            }
            ViewBag.IsAdmin = isAdmin;

            //get list of applications by tripID
            IEnumerable<Application> applications = context.Applications.Where(a => a.TripId == id);

            //iterate through rosters for users, accumulate found matches in users list
            IList<AspNetUsers> users = new List<AspNetUsers>();


            IEnumerable<Trip> trips = context.Trips.OrderBy(i => i.TripId);


            if (applications.Count() > 0)
            {
                foreach (var app in applications)
                {
                    users.Add(context.AspNetUsers.FirstOrDefault(u => u.Id == app.StudentId));
                }
            }
            //Find all application forms related to the application & trip
            IEnumerable<ApplicationForms> appForms = context.ApplicationForms.Where(af => af.Application.TripId == id);

            return View(new StudentListViewModel(trip, users, applications, appForms, payment));
        }

        //review a form by Application ID
        public IActionResult ReviewForms(int id)
        {
            //find application by Application ID
            Application application = context.Applications.FirstOrDefault(a => a.ApplicationId == id);

            //find user by Student ID from the found application
            AspNetUsers user = context.AspNetUsers.FirstOrDefault(u => u.Id == application.StudentId);
            
            ViewBag.UserId = user.Id;
            //place user's full name in viewbag
            ViewBag.User = user.FirstName + " " + user.LastName;
            ViewBag.Email = user.Email;
            ViewBag.Gender = user.Gender;
            ViewBag.Phone = user.PhoneNumber;

            //find trip by Trip ID from the found application
            Trip trip = context.Trips.FirstOrDefault(t => t.TripId == application.TripId);

            //find all Questions for this trip
            IQueryable<TripQuestion> tripQuestions = DbContext.TripQuestion.Where(tq => tq.TripId == trip.TripId);

            //find all Question Responses for the trip
            IQueryable<TripQuestionResponse> tripQuestionResponses;
            try
            {
                 tripQuestionResponses = DbContext.TripQuestionResponse.Where(tqr => tqr.ApplicationId == id);
            }
            catch
            {
                tripQuestionResponses = null;
            }
            IQueryable<Payment> payments = DbContext.Payment.Where(p => p.TripId == trip.TripId);
            //place trip in viewbag
            ViewBag.Trip = trip;

            //Find all application forms related to the application
            IEnumerable<ApplicationForms> appForms = context.ApplicationForms.Where(af => af.ApplicationId == application.ApplicationId);

            //get all forms
            IEnumerable<Form> forms = context.Forms;

            //start a blank list of forms
            List<Form> newForms = new List<Form>();

            //iterate through each application form and each form, add matches to blank list of forms
            foreach (ApplicationForms a in appForms)
            {
                foreach (Form form in forms)
                {
                    if (a.FormId == form.FormId)
                    {
                        newForms.Add(form);
                    }
                }
            }
            
          //  var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Payment payment = DbContext.Payment.FirstOrDefault(p => p.TripId == trip.TripId && p.StudentId == application.StudentId);
           

            return View(new ReviewFormsViewModel(payment,application, newForms, appForms,tripQuestions, tripQuestionResponses,payments));
        }
        public IActionResult ConfirmPayment(int id)
        {
            Application app = DbContext.Application.FirstOrDefault(a => a.ApplicationId == id);
            Payment payment = DbContext.Payment.Single(p => p.StudentId == app.StudentId && p.TripId == app.TripId);

            context.ConfirmPayment(payment);
            return RedirectToAction("TripDetail/" + app.TripId);
        }
        public IActionResult CreatePayment(int id)
        {
            ////get correct application from id
            Application app = DbContext.Application.FirstOrDefault(a => a.ApplicationId == id);

            Payment pay = new Payment();
            pay.StudentId = app.StudentId;
            pay.TripId = app.TripId;
            pay.PaymentDate = DateTime.Now;
            pay.Amount = 50;
            pay.ModifiedDate = DateTime.Now;
            pay.PaymentConfirmed = true;

            //update database
            context.AddPayment(pay);
            return RedirectToAction("TripDetail/" + app.TripId);
        }
        //approve form based on application ID and form ID
        public IActionResult ApproveForm(int id)
        {
            //find the correct application form by searching using both the application and form id's
            ApplicationForms appForm = context.ApplicationForms.FirstOrDefault(af => af.ApplicationFormId == id);
            context.ApproveForm(appForm);

            return RedirectToAction("ReviewForms", new { id = appForm.ApplicationId });
        }
        public IActionResult UndoFormAction(int id)
        {
            //find the correct application form by searching using both the application and form id's
            ApplicationForms appForm = context.ApplicationForms.FirstOrDefault(af => af.ApplicationFormId == id);
            context.UndoFormAction(appForm);

            return RedirectToAction("ReviewForms", new { id = appForm.ApplicationId });
        }

        //deny form based on application ID and form ID
        public IActionResult DenyForm(int id)
        {
            //find the correct application form by searching using both the application and form id's
            ApplicationForms appForm = context.ApplicationForms.FirstOrDefault(af => af.ApplicationFormId == id);
            context.DenyForm(appForm);

            return RedirectToAction("ReviewForms", new { id = appForm.ApplicationId });
        }
        public IActionResult DropStudent(int id)
        {
            var row = DbContext.Application.FirstOrDefault(a => a.ApplicationId == id);
            var tqrs = DbContext.TripQuestionResponse.Where(tqr => tqr.ApplicationId == id);

            foreach (var tq in tqrs)
            {
                DbContext.Remove(tq);  
            }
            DbContext.SaveChanges();
            DbContext.Remove(row);
            DbContext.SaveChanges();

            return Json(Url.Action("TripDetail/" + row.TripId, "Coordinator"));
        }
    }
}