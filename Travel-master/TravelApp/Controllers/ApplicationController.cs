using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelApp.Models;

namespace TravelApp.Controllers
{
    //TODO: This likely needs updating or reworking entirely
    //[Authorize]
    public class ApplicationController : Controller
    {
        private IPersonRepository context;
        public ApplicationController(IPersonRepository repository) => context = repository;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Application()
        {
            ViewBag.Today = DateTime.Now;
            return View();
        }

        [HttpPost]
        public IActionResult Application(StudentUser su)
        {
            if (ModelState.IsValid)
            {
                context.UpdateStudentInfo(su);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}