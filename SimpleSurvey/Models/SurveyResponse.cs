using System;
using System.Collections.Generic;

namespace SimpleSurvey.Models
{
    public partial class SurveyResponse
    {
        public int EntryId { get; set; }
        public int SurveyId { get; set; }
        public int ResponseId { get; set; }
        public DateTime? ResponseDate { get; set; }
        public string UserEmail { get; set; }

        public SurveyEntry Survey { get; set; }
    }
}
