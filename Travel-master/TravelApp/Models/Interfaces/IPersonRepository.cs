using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    public interface IPersonRepository
    {
        IQueryable<AspNetUsers> AspNetUsers { get; }
        IQueryable<Student> Students { get; }

        void UpdateStudentInfo(StudentUser su);
    }
}
