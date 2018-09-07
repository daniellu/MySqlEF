using Hatfield.AnalyteManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.AnalyteManagement.GuidelineCalculation
{
    public interface IGuidelineValueCalculator
    {
        IEnumerable<GuidelineCalculationResultViewModel> Calculate(
            IEnumerable<GuidelineCalculationRequestViewModel> dataset, 
            IEnumerable<AnalyteGuideline> guidelines, 
            DependentDecision dependentDecision);

        AnalyteGuidelineValue CalculateGuidelineValue(
            GuidelineCalculationRequestViewModel data,             
            AnalyteGuideline guideline);

        bool CalculateIfExcessGuideline(AnalyteGuidelineValue guideline, double? value, ComparisonOperation operation);
    }
}
