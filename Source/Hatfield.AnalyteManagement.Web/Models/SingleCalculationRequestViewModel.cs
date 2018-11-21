using Hatfield.AnalyteManagement.GuidelineCalculation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hatfield.AnalyteManagement.Web.Models
{
    public class SingleCalculationRequestViewModel
    {
        public string guidelineName { get; set; }
        public string analyte { get; set; }
        public double value { get; set; }
        public string unit { get; set; }

        public double? hardness { get; set; }
        public double? pH { get; set; }
        public double? chloride { get; set; }
        public double? temperature { get; set; }
    }
}