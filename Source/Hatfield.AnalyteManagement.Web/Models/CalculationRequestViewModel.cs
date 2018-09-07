using Hatfield.AnalyteManagement.GuidelineCalculation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hatfield.AnalyteManagement.Web.Models
{
    public class CalculationRequestViewModel
    {
        public IEnumerable<GuidelineCalculationRequestViewModel> data { get; set; }
        public IEnumerable<string> guidelineName { get; set; }
        public IEnumerable<string> option { get; set; }
    }
}