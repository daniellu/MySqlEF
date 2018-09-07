using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.AnalyteManagement.GuidelineCalculation
{
    public class Constants
    {
        public const string PH_AnalyteName = "PH";
        public const string Hardness_AnalyteName = "Hardness (as CaCO3)";
        public const string Chloride_AnalyteName = "Chloride (Cl)";
        public const string MathE = "Math.E";

        public const string DoubleValueType = "double";
        public const string EquationValueType = "equation";
        public const string RangeValueType = "range";

        public static readonly ComparisonOperation[] LinearOperations =
        {
            ComparisonOperation.Less,
            ComparisonOperation.LessOrEqual,
            ComparisonOperation.Equal,
            ComparisonOperation.LargerOrEqual,
            ComparisonOperation.Larger
        };

        public static readonly ComparisonOperation[] RangeOperations =
        {
            ComparisonOperation.Between,
            ComparisonOperation.NotBetween
        };
    }
}
