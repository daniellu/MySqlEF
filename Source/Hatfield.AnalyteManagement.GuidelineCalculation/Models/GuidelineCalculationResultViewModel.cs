using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.AnalyteManagement.GuidelineCalculation
{
    public class GuidelineCalculationResultViewModel : GuidelineCalculationRequestViewModel
    {
        public string GuidelineValueString { get; set; }
        public bool IsExceedance { get; set; }

        public string GuidelineUnit { get; set; }
    }
}
