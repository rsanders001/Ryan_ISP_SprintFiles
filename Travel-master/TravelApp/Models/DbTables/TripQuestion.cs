using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    public partial class TripQuestion
    {
        public TripQuestion()
            {}

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TripQuestionID { get; set; }
        public int TripId { get; set; }
        public string QuestionText { get; set; }
    }
}
