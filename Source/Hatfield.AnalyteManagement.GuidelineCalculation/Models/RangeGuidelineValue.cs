using Hatfield.AnalyteManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.AnalyteManagement.GuidelineCalculation
{
    public class RangeGuidelineValue : AnalyteGuidelineValue
    {
        private readonly static string GuidelineValueStringFormat = "{0} - {1}";
        public double Max { get; protected set; }
        public double Min { get; protected set; }

        public override string GuidelineValueString
        {
            get
            {
                return string.Format(GuidelineValueStringFormat, Min.ToString(valueFormat), Max.ToString(valueFormat));
            }
        }

        public RangeGuidelineValue(string guideline, string analyte, string equationType, string unit, double minValue, double maxValue) : 
            base(guideline, analyte, equationType, unit)
        {
            if(minValue > maxValue)
            {
                throw new ArgumentException("Min must not larger than Max value");
            }

            Max = maxValue;
            Min = minValue;
        }
    }
}
