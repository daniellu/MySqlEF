using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.AnalyteManagement.GuidelineCalculation
{
    public class GuidelineValueRange
    {
        public double? Max { get; set; }
        public double? Min { get; set; }
        public string ValueString { get; set; }
        public Type Type { get; set; }

        public GuidelineValueRange(double? max, double? min, string valueString, Type type)
        {
            Max = max;
            Min = min;
            ValueString = valueString;
            Type = type;
        }
    }
}
