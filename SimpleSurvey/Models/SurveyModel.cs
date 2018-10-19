using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SimpleSurvey.Models
{
    public class SurveyModel
    {
        public List<List<SurveyItem>> Items { get; set; }

        public List<Option> Options { get; set; }

        public bool SurveyTaken { get; set; }
    }

    public class Option
    {
        public int Id { get; set; }

        public string OptionText { get; set; }

    }
    public class SurveyItem
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public int? ResponseId { get; set; }
    }

    public enum SurveyOption
    {
        [Description("Strongly Agree")]
        StronglyAgree = 1,
        [Description("Agree")]
        Agree = 2,
        [Description("Undecided")]
        Undecided = 3,
        [Description("Disagree")]
        Disagree = 4,
        [Description("Strongly Disagree")]
        StronglyDisagree = 5

    }

    public static class Util
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

    }

}
