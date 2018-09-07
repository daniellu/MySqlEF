using Hatfield.AnalyteManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.AnalyteManagement.GuidelineCalculation
{
    public abstract class AnalyteGuidelineValue
    {
        public virtual string Guideline { get; protected set; }
        public virtual string Analyte { get; protected set; }
        public string EquationType { get; protected set; }
        public string Unit { get; protected set; }
        public abstract string GuidelineValueString { get; }

        protected static string valueFormat = "0.######";

        protected AnalyteGuidelineValue(string guideline, string analyte, string equationType, string unit)
        {
            Guideline = guideline;
            Analyte = analyte;
            EquationType = equationType;
            Unit = unit;
        }
    }
}
