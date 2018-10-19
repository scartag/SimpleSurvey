using System;
using System.Collections.Generic;

namespace SimpleSurvey.Models
{
    public partial class SurveyEntry
    {
        public SurveyEntry()
        {
            SurveyResponses = new HashSet<SurveyResponse>();
        }

        public int Id { get; set; }
        public string SurveyQuestion { get; set; }

        public ICollection<SurveyResponse> SurveyResponses { get; set; }
    }
}
