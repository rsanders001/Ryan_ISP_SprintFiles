using System;
using System.ComponentModel.DataAnnotations;
//comment
namespace TravelApp.Models
{
    //viewmodel for a user completing student info
    //used to update their AspNetUser and Student profiles
    public class StudentUser
    {
        public StudentUser() { }
        
        //AspNetUser fields
        public string UserID { get; set; }
        public string WctcID { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$", ErrorMessage = "First name needs to begin with a capital letter")]
        [Required(ErrorMessage = "First Name Required")]
        public string FirstName { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$", ErrorMessage = "Last name needs to begin with a capital letter")]
        [Required(ErrorMessage = "Last Name Required")]
        public string LastName { get; set; }
        //[RegularExpression(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")]
        [EmailAddress]
        [Required(ErrorMessage = "Valid Email Required")]
        public string Email { get; set; }
        //[RegularExpression(@"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$")]
        [Phone]
        [Required(ErrorMessage = "Phone Number Required")]
        public string Phone { get; set; }
        public char Gender { get; set; }

        //student fields
        //studentID is the same as UserID
        public int ProgramID { get; set; }
        [DataType(DataType.Date)]

        //[RegularExpression(@"^(0[1 - 9] | 1[012])[- /.](0[1 - 9] |[12][0 - 9] | 3[01])[- /.](19 | 20)\d\d$")]
        public DateTime BirthDate { get; set; }
        public string ExpectedGradMonth { get; set; }

        [Range(2000, 2500)]
        public short ExpectedGradYear { get; set; }

        //tripID
        public int? TripId { get; set; }

        //timestamp form submission
        public DateTime ModifiedDate = DateTime.Now;

        //complete objects for referencing
        public AppUser AppUser { get; set; }
        public Student Student { get; set; }
    }
}