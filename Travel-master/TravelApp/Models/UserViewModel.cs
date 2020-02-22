using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelApp.Models
{
    //TODO: Modify this class to reflect what a new student should register with
    //Will have to run new migration afterwards
    public class CreateModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [UIHint("email")]
        public string Email { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
        [Required]
        public string WctcID { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [UIHint("email")] // ensures the taghelper renders the appropriate form field
        public string Email { get; set; }

        [Required]
        [UIHint("password")] // ensures the taghelper renders the appropriate form field
        public string Password { get; set; }
    }

    public class RoleEditModel
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<AppUser> Members { get; set; }
        public IEnumerable<AppUser> NonMembers { get; set; }
    }

    public class RoleModificationModel
    {
        [Required]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
}
