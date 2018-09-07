using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hatfield.AnalyteManagement.GuidelineCalculation
{
    public class GuidelineCalculationRequestViewModel
    {
        public string Analyte { get; set; }
        public string Site { get; set; }
        public DateTime DateTime { get; set; }
        public double? Value { get; set; }
        public string Unit { get; set; }

        public double? MathE { get; set; }
        public double? PH { get; set; }
        public double? Hardness { get; set; }
        public double? Chloride { get; set; }

        public GuidelineCalculationResultViewModel ToResult()
        {
            var serializedParent = JsonConvert.SerializeObject(this);
            var result = JsonConvert.DeserializeObject<GuidelineCalculationResultViewModel>(serializedParent);
            return result;
        }

        public Dictionary<string, object> ToEquationParameters()
        {
            var dic = new Dictionary<string, object>();
            if (MathE.HasValue)
            {
                dic.Add(Constants.MathE, MathE);
            }
            if (PH.HasValue)
            {
                dic.Add(Constants.PH_AnalyteName, PH);
            }
            if (Hardness.HasValue)
            {
                dic.Add(Constants.Hardness_AnalyteName, Hardness);
            }
            if (Chloride.HasValue)
            {
                dic.Add(Constants.Chloride_AnalyteName, Chloride);
            }

            return dic;
        }
    }
}