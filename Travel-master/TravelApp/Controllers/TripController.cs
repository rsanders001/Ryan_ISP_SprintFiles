using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelApp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelApp.Controllers
{
    [Route("api/[controller]")]

    public class TripController : Controller
    {

        private ITripRepository tripRepository;


        public TripController(ITripRepository repository) => tripRepository = repository;

        [HttpGet]
        // returns all events (unsorted)
        public IEnumerable<Trip> Get() => tripRepository.Trip;
            //.Include(e => e.TripName);

        [HttpGet("{id}")]
        // return specific event
        public Trip Get(int id) => tripRepository.Trip
            .Include(e => e.TripName)
            .FirstOrDefault(e => e.TripId == id);
    }
}
