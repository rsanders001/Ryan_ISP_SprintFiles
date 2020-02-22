using System;
using System.Collections.Generic;

namespace TravelApp.Models
{
    public partial class ApplicationForms
    {
        public int ApplicationFormId { get; set; }
        public int ApplicationId { get; set; }
        public int FormId { get; set; }
        public int FormStatusId { get; set; }

        public Application Application { get; set; }
        public Form Form { get; set; }
        public FormStatus FormStatus { get; set; }
    }
}
