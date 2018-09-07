using Hatfield.AnalyteManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.AnalyteManagement.GuidelineCalculation
{
    public class SimpleGuidelineValue : AnalyteGuidelineValue
    {        
        public double? GuidelineValue { get; protected set; }

        public SimpleGuidelineValue(string guideline, string analyte, string equationType, string unit, double? guidlineValue) : 
            base(guideline, analyte, equationType, unit)
        {            
            GuidelineValue = guidlineValue;
        }

        public override string GuidelineValueString
        {
            get 
            {            
                if(!GuidelineValue.HasValue)
                {
                    return String.Empty;
                }
                return GuidelineValue.Value.ToString(valueFormat);
            }
        }
    }
}
