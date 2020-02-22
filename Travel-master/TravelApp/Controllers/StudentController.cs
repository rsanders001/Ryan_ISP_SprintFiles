using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TravelApp.Models;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelApp.Controllers
{
    //users with all three roles may use these views
    [Authorize(Roles = "Student,Coordinator,Admin")]
    public class StudentController : Controller
    {
        private ITripRepository tripContext;
        private IPersonRepository personContext;
        private UserManager<AppUser> userManager;
        private Task<AppUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);
        private StudentTravelDocsContext DbContext { get; }

        public StudentController(ITripRepository tripRepository, IPersonRepository personRepository, UserManager<AppUser> usrMgr, StudentTravelDocsContext docsContext)
        {
            tripContext = tripRepository;
            personContext = personRepository;
            userManager = usrMgr;
            DbContext = docsContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            //find user for student object and viewbag
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AppUser user = userManager.Users.FirstOrDefault(u => u.Id == userId);
            ViewBag.User = user;
            ViewBag.UserId = userId;

            //get user's student info for modified date in viewbag
            var student = DbContext.Student.FirstOrDefault(s => s.StudentId == user.Id);
            ViewBag.ModDate = student.ModifiedDate;
            ViewBag.DOB = student.BirthDate;

            //viewbag for timespan
            TimeSpan timeSpan = DateTime.Now.Subtract(student.ModifiedDate);
            ViewBag.TimeSpan = Math.Ceiling(timeSpan.TotalDays);
            
            //get ordered list of trips, don't include it if they created it
            IEnumerable<Trip> trips = tripContext.Trips.OrderBy(i => i.TripId).Where(t => t.CreatedBy != userId);

            //if application(s) exists for the current user, return view with list of them
            if (DbContext.Application.FirstOrDefault(a => a.StudentId == user.Id) != null)
            {
                IEnumerable<Application> apps = DbContext.Application.Where(a => a.StudentId == user.Id);   
                IEnumerable<Payment> payment = DbContext.Payment.Where(p => p.StudentId == user.Id);
                IEnumerable<ApplicationForms> appForms = DbContext.ApplicationForms;
                return View(new StudentTripViewModel(trips, apps, payment, appForms));
            }
            else //else return blank StudentTripViewModel object
            {
                IEnumerable<Application> apps = new List<Application>();
                IEnumerable<ApplicationForms> appForms = new List<ApplicationForms>();
                IEnumerable<Payment> payment = new List<Payment>();
                return View(new StudentTripViewModel(trips, apps, payment,appForms));
            }
        }

        //Complete Student Account, takes a nullable a trip ID
        public IActionResult StudentInfo(int? id)
        {
            //put list of programs in viewbag
            ViewBag.programs = DbContext.Program.OrderBy(p => p.Name);

            //find user and student objects by user id
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AppUser user = userManager.Users.FirstOrDefault(u => u.Id == userId);
            Student student = DbContext.Student.FirstOrDefault(s => s.StudentId == userId);
            ViewBag.Year = DateTime.Now.Year;

            //place user and student objects in StudentUser
            StudentUser su = new StudentUser
            {
                UserID = user.Id,
                AppUser = user,
                Student = student,
                TripId = id
            };

            return View(su);
        }

        [HttpPost] //form post request from StudentInfo view
        public async Task<IActionResult> StudentInfo(StudentUser su)
        {
            if (ModelState.IsValid)
            {
                //get current user object, set new form data to fields
                AppUser appUser = await GetCurrentUserAsync();
                appUser.UserName = su.FirstName[0] + su.LastName;
                appUser.WctcID = su.WctcID;
                appUser.FirstName = su.FirstName;
                appUser.LastName = su.LastName;
                appUser.Email = su.Email;
                appUser.Gender = su.Gender;
                appUser.PhoneNumber = su.Phone;

                //update AspNetUser
                IdentityResult result = await userManager.UpdateAsync(appUser);

                //if update worked...
                if (result.Succeeded)
                {
                    //update new student info
                    su.UserID = appUser.Id;
                    personContext.UpdateStudentInfo(su);
                    if (su.TripId != null)
                    {
                        // Update to a redirect to AcceptedTripDetail/Trip.Id
                        return RedirectToAction("TripDetail", new { id = su.TripId });
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            if (su.TripId != null)
            {
                // Update to a redirect to AcceptedTripDetail/Trip.Id
                return RedirectToAction("TripDetail", new { id = su.TripId });
            }
            else
            {
                //return RedirectToAction("Index");
                //if there are errors in the student model, this is what will run
                //return RedirectToAction("StudentInfo");
                //return View(su.TripId);
                return StudentInfo(su.TripId);
            }
        }

        //view trip by ID
        public IActionResult TripDetail(int id)
        {
            //find user
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AppUser user = userManager.Users.FirstOrDefault(u => u.Id == userId);

            IQueryable<TripQuestion> questions = DbContext.TripQuestion.Where(tp => tp.TripId == id);
            ViewBag.Questions = questions;

            //get user's student info for modified date in viewbag
            var student = DbContext.Student.FirstOrDefault(s => s.StudentId == user.Id);
            ViewBag.ModDate = student.ModifiedDate;
            ViewBag.DOB = student.BirthDate;

            //viewbag for timespan
            TimeSpan timeSpan = DateTime.Now.Subtract(student.ModifiedDate);
            ViewBag.TimeSpan = Math.Ceiling(timeSpan.TotalDays);

            //find the corresponding trip in the DB
            Trip trip = tripContext.Trips.FirstOrDefault(t => t.TripId == id);

            //then course
            trip.Course = tripContext.Courses.FirstOrDefault(c => c.CourseId == trip.CourseId);

            //then destination
            trip.Destination = tripContext.Destinations.FirstOrDefault(d => d.DestinationId == trip.DestinationId);

            //return View(trip);


            TripApplyModel tam = new TripApplyModel();
            tam.Trip = trip;

            return View(tam);


        }

        //view accepted trip by applicationID
        public IActionResult AcceptedTripDetail(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //get application by passed in ID in order to get trip for viewmodel
            Application app = tripContext.Applications.FirstOrDefault(a => a.ApplicationId == id);
            Trip trip = tripContext.Trips.FirstOrDefault(t => t.TripId == app.TripId);
            Payment payment;
            try
            {
                payment = DbContext.Payment.FirstOrDefault(p => p.TripId == trip.TripId && p.StudentId == userId);
            }
            catch
            {
                payment = null;
            }
            //get destination using trip
            Destination destination = tripContext.Destinations.FirstOrDefault(d => d.DestinationId == trip.DestinationId);

            //get list of appForms by appId
            List<ApplicationForms> appForms = tripContext.ApplicationForms.Where(f => f.ApplicationId == app.ApplicationId).ToList();

            //viewbags
            var coordinator = personContext.AspNetUsers.FirstOrDefault(c => c.Id == trip.CreatedBy);
            ViewBag.Coordinator = coordinator;

            //build viewmodel
            SignatureFormViewModel view = new SignatureFormViewModel
            {
                Trip = trip,
                Destination = destination,
                AppFormList = appForms,
                Application = app,
                Payment = payment
            };

            return View(view);
        }
        public IActionResult StudentDocs() { return View(); }
        //view code of conduct form by appFormId
        public IActionResult CodeOfConductForm(int id)
        {
            //build viewmodel, start with applicationForm
            SignatureFormViewModel view = new SignatureFormViewModel
            {
                AppForm = tripContext.ApplicationForms.FirstOrDefault(af => af.ApplicationFormId == id)
            };

            //get application in order to get trip for viewmodel
            Application app = tripContext.Applications.FirstOrDefault(a => a.ApplicationId == view.AppForm.ApplicationId);
            Trip trip = tripContext.Trips.FirstOrDefault(t => t.TripId == app.TripId);
            view.Trip = trip;

            //find user for viewbag for form
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AppUser user = userManager.Users.FirstOrDefault(u => u.Id == userId);
            ViewBag.User = user;

            //today's date for viewbag in form
            ViewBag.Today = DateTime.Now.ToShortDateString();

            return View(view);
        }
        public IActionResult cocForm()
        {
            //find user for viewbag for form
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AppUser user = userManager.Users.FirstOrDefault(u => u.Id == userId);
            ViewBag.User = user;

            //today's date for viewbag in form
            ViewBag.Today = DateTime.Now.ToShortDateString();

            return View();
        }

        //view release of liability form by appFormId
        public IActionResult ReleaseOfLiabilityForm(int id)
        {
            //build viewmodel, start with applicationForm
            SignatureFormViewModel view = new SignatureFormViewModel
            {
                AppForm = tripContext.ApplicationForms.FirstOrDefault(af => af.ApplicationFormId == id)
            };

            //get application in order to get trip for viewmodel
            Application app = tripContext.Applications.FirstOrDefault(a => a.ApplicationId == view.AppForm.ApplicationId);
            Trip trip = tripContext.Trips.FirstOrDefault(t => t.TripId == app.TripId);
            view.Trip = trip;

            //get destination
            Destination destination = tripContext.Destinations.FirstOrDefault(d => d.DestinationId == trip.DestinationId);
            view.Destination = destination;

            //get course
            Course course = tripContext.Courses.FirstOrDefault(c => c.CourseId == trip.CourseId);
            view.Course = course;

            //find user for viewbag for form
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AppUser user = userManager.Users.FirstOrDefault(u => u.Id == userId);
            ViewBag.User = user;

            //today's date for viewbag in form
            ViewBag.Today = DateTime.Now.ToShortDateString();

            return View(view);
        }
        public IActionResult LiabFormGeneral()
        {
            //find user for viewbag for form
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AppUser user = userManager.Users.FirstOrDefault(u => u.Id == userId);
            ViewBag.User = user;

            //today's date for viewbag in form
            ViewBag.Today = DateTime.Now.ToShortDateString();

            return View();
        }

        //view release form by appFormId
        public IActionResult StandardModelReleaseForm(int id)
        {
            //build viewmodel, start with applicationForm
            SignatureFormViewModel view = new SignatureFormViewModel
            {
                AppForm = tripContext.ApplicationForms.FirstOrDefault(af => af.ApplicationFormId == id)
            };

            //get application in order to get trip for viewmodel
            Application app = tripContext.Applications.FirstOrDefault(a => a.ApplicationId == view.AppForm.ApplicationId);
            Trip trip = tripContext.Trips.FirstOrDefault(t => t.TripId == app.TripId);
            view.Trip = trip;

            //get destination
            Destination destination = tripContext.Destinations.FirstOrDefault(d => d.DestinationId == trip.DestinationId);
            view.Destination = destination;

            //find user for viewbag for form
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AppUser user = userManager.Users.FirstOrDefault(u => u.Id == userId);
            ViewBag.User = user;

            //get today's date for viewbag in form
            ViewBag.Today = DateTime.Now.ToShortDateString();

            return View(view);
        }

        public IActionResult smrForm()
        {
            //find user for viewbag for form
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AppUser user = userManager.Users.FirstOrDefault(u => u.Id == userId);
            ViewBag.User = user;

            //get today's date for viewbag in form
            ViewBag.Today = DateTime.Now.ToShortDateString();
            return View();
        }

        //TODO: Needs an Entity Framework Class and to call an EF Class method
        [HttpPost]
        public async Task<IActionResult> SubmitApplicationForm(ApplicationForms af)
        {

            return RedirectToAction("AcceptedTripDetail", new { id = af.Application.TripId });
        }
        /*
         * Moved to trip detail       
        //apply for a trip by trip ID
        public IActionResult ApplyForTrip(int id)
        {
            Trip trip = tripContext.Trips.FirstOrDefault(t => t.TripId == id);
            TripApplyModel tam = new TripApplyModel();
            tam.Trip = trip;
            return View(tam);
        }
        */

        [HttpPost] //form post request from ApplyForTrip
        public async Task<IActionResult> SubmitTripApplication(TripApplyModel model, List<string> responses)
        {
            if (ModelState.IsValid)
            {
                Application app = new Application();
                AppUser usr = await GetCurrentUserAsync();
                app.StudentId = usr.Id;
                app.TripId = model.TripId;
                app.ApplicationDate = DateTime.Now;
                app.Gpa = model.Gpa;
                app.Rationale = model.Rationale;
                app.AppStatusId = model.AppStatusId;
                app.AcademicStatusId = 1; // hard-coding for now
                app.ModifiedDate = DateTime.Now;

                //save to db to create appId
                DbContext.Application.Add(app);
                DbContext.SaveChanges();

                //get TripQuestionId from TripId
                IQueryable<TripQuestion> tripQuestions = DbContext.TripQuestion.Where(tq => tq.TripId == model.TripId);
                int i = 0;
                foreach (var tq in tripQuestions)
                {
                    TripQuestionResponse tqr = new TripQuestionResponse();
                    tqr.Response = responses[i];
                    tqr.ApplicationId = app.ApplicationId;
                    tqr.TripQuestionID = tq.TripQuestionID;
                    DbContext.TripQuestionResponse.Add(tqr);
                    i++;
                }
                //tqr.Response = model.Response;
                DbContext.SaveChanges();


                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost] //print form based on applicationform ID
        public IActionResult PrintFormSubmit(int id)
        {
            //find the correct application form by searching using both the application and form id's
            ApplicationForms appForm = tripContext.ApplicationForms.FirstOrDefault(af => af.ApplicationFormId == id);
            tripContext.PrintFormSubmit(appForm);

            return RedirectToAction("AcceptedTripDetail", new { id = appForm.ApplicationId });
        }

        //result errors
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        public IActionResult Paid(int id)
        {
            ////get correct application from id
            Application app = DbContext.Application.FirstOrDefault(a => a.ApplicationId == id);

            //Payment payment = DbContext.Payment.Single(p => p.StudentId == app.StudentId && p.TripId == app.TripId);

            //payment.StudentPaid = true;
            //tripContext.UpdatePayment(payment);
            Payment pay = new Payment();
            pay.StudentId = app.StudentId;
            pay.TripId = app.TripId;
            pay.PaymentDate = DateTime.Now;
            pay.Amount = 50;
            pay.ModifiedDate = DateTime.Now;
            pay.PaymentConfirmed = false;

            //update database
            tripContext.AddPayment(pay);

            return RedirectToAction("Index");
        }
        public IActionResult DropTrip(int id)
        {
            var row = DbContext.Application.FirstOrDefault(a => a.ApplicationId == id);
            var appIds = DbContext.TripQuestionResponse.Where(tqr => tqr.ApplicationId == id);

            foreach (var app in appIds)
            {
                DbContext.Remove(app);
            }
            DbContext.SaveChanges();
            DbContext.Remove(row);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
