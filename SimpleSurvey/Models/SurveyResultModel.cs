using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleSurvey.Models
{
    public class SurveyResultModel
    {
        public List<SurveyResult> Items { get; set; }

        public List<Option> Options { get; set; }
    }
    public class SurveyResult
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public int SAG { get; set; }

        public int AG { get; set; }

        public int UND { get; set; }

        public int DAG { get; set; }

        public int SDAG { get; set; }
    }
}
