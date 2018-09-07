using Hatfield.AnalyteManagement.GuidelineCalculation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hatfield.AnalyteManagement.Web.Models
{
    public class CalculationResultViewModel
    {
        public string Analyte { get; set; }
        public string Site { get; set; }
        public DateTime DateTime { get; set; }
        public double? Value { get; set; }
        public string Unit { get; set; }

        public string Guideline { get; set; }
        public string GuidelineUnit { get; set; }
        public bool IsExceedance { get; set; }
    }
}