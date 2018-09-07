using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hatfield.AnalyteManagement.Domain;

namespace Hatfield.AnalyteManagement.GuidelineCalculation
{
    public interface IExccedanceDecider
    {
        bool IsSupported(AnalyteGuidelineValue guideline, ComparisonOperation operation);
        bool CalculateIfExcessGuideline(AnalyteGuidelineValue guideline, double? value, ComparisonOperation operation);
    }
}
