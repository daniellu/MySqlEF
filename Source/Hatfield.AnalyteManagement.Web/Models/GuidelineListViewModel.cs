using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hatfield.AnalyteManagement.Web.Models
{
    public class GuidelineListViewModel
    {
        public string Name { get; set; }
        public string ShortCode { get; set; }
        public IEnumerable<LabAnalyteGuideline> AnalyteGuidelines { get; set; }
    }

    public class LabAnalyteGuideline
    {
        public string Anlayte { get; set; }
        public string GuidelineValue { get; set; }
        public string Unit { get; set; }
    }
}