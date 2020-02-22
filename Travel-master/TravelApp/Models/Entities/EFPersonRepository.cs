using System.Linq;

namespace TravelApp.Models
{
    public class EFPersonRepository : IPersonRepository
    {
        private StudentTravelDocsContext context;

        public EFPersonRepository(StudentTravelDocsContext ctx)
        {
            context = ctx;
        }

        public IQueryable<AspNetUsers> AspNetUsers => context.AspNetUsers;
        public IQueryable<Student> Students => context.Student;

        //complete a user's student info
        public void UpdateStudentInfo(StudentUser su)
        {
            //update Student info
            Student student = new Student()
            {
                StudentId = su.UserID,
                ProgramId = su.ProgramID,
                BirthDate = su.BirthDate,
                ExpectedGradMonth = su.ExpectedGradMonth,
                ExpectedGradYear = su.ExpectedGradYear,
                ModifiedDate = su.ModifiedDate
            };
            context.Student.Update(student);
            context.SaveChanges();
        }
    }
}
