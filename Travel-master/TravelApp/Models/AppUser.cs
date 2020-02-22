using System;
using Microsoft.AspNetCore.Identity;

namespace TravelApp.Models
{
    //TravelApp's implementation of IdentityUser, complete with additional fields
    public class AppUser : IdentityUser
    {
        //additional fields to add to AspNetUsers
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WctcID { get; set; }
        public char Gender { get; set; }
    }
}
