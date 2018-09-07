using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hatfield.AnalyteManagement.GuidelineCalculation
{
    public class RangeGuidelineValueExccedanceDecider : IExccedanceDecider
    {        
        public bool IsSupported(AnalyteGuidelineValue guideline, ComparisonOperation operation)
        {
            return (guideline is RangeGuidelineValue) && Constants.RangeOperations.Contains(operation);
        }

        public bool CalculateIfExcessGuideline(AnalyteGuidelineValue guideline, double? value, ComparisonOperation operation)
        {
            if (!IsSupported(guideline, operation))
            {
                throw new ArgumentException("guideine type or the comparison operation is not supported by the" + this.GetType().Name);
            }

            var castedGuideline = (RangeGuidelineValue)guideline;
            if (value.HasValue)
            {
                var expressionFormat = GetComparisonExpressionFormat(operation);
                var expressionString = String.Format(expressionFormat, castedGuideline.Min, value.Value, castedGuideline.Max);

                var expression = new Expression(expressionString);
                var result = (bool)expression.Evaluate();

                return !result;
            }

            return false;  
        }

        private string GetComparisonExpressionFormat(ComparisonOperation operation)
        {
            switch (operation)
            {
                case ComparisonOperation.Between:
                    return "{0} <= {1} and {1} <= {2}";
                case ComparisonOperation.NotBetween:
                    return "{0} > {1} or {1} > {2}";
                default:
                    throw new ArgumentOutOfRangeException(operation.ToString() + " is not a supported linear operation.");
            }
        }
    }
}
