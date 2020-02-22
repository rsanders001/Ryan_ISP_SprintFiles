using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    public partial class TripQuestionResponse
    {
        public TripQuestionResponse()
        { }

        public int TripQuestionResponseID { get; set; }
        public int TripQuestionID { get; set; }
        public int ApplicationId { get; set; }
        public string Response { get; set; }
    }
}
