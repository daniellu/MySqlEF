using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NCalc;

namespace Hatfield.AnalyteManagement.GuidelineCalculation
{
    public class SimpleGuidelineValueExccedanceDecider : IExccedanceDecider
    {        
        public bool IsSupported(AnalyteGuidelineValue guideline, ComparisonOperation operation)
        {
            return (guideline is SimpleGuidelineValue) && Constants.LinearOperations.Contains(operation);
        }

        public bool CalculateIfExcessGuideline(AnalyteGuidelineValue guideline, double? value, ComparisonOperation operation)
        {
            if (!IsSupported(guideline, operation))
            {
                throw new ArgumentException("guideine type or the comparison operation is not supported by the " + this.GetType().Name);
            }

            var castedGuideline = (SimpleGuidelineValue)guideline;
            if(value.HasValue && castedGuideline.GuidelineValue.HasValue)
            {
                var operationSymbol = GetOperationSymbol(operation);
                var expressionString = String.Format("{0} {1} {2}", value.Value, operationSymbol, castedGuideline.GuidelineValue.Value);

                var expression = new Expression(expressionString);
                var result = (bool)expression.Evaluate();

                return !result;
            }

            return false;            
        }

        private string GetOperationSymbol(ComparisonOperation operation)
        {
            switch (operation)
            {
                case ComparisonOperation.LessOrEqual:
                    return "<=";
                case ComparisonOperation.Less:
                    return "<";
                case ComparisonOperation.Equal:
                    return "==";
                case ComparisonOperation.LargerOrEqual:
                    return ">=";
                case ComparisonOperation.Larger:
                    return ">";
                default:
                    throw new ArgumentOutOfRangeException(operation.ToString() + " is not a supported linear operation.");
            }
        }
    }
}
