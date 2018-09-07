using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.AnalyteManagement.GuidelineCalculation
{
    public class UnitValueConverter
    {
        //private static readonly ILog log = LogManager.GetLogger("Application");

        public double? Convert(double? originalValue, string originalUnit, string resultUnit)
        {
            if (originalValue == null || !originalValue.HasValue)
            {
                return null;
            }
            else if (string.IsNullOrEmpty(originalUnit) || string.IsNullOrEmpty(resultUnit))
            {
                return originalValue;
            }
            else if (string.Compare(originalUnit, resultUnit, false) == 0)
            {
                return originalValue;
            }
            else if (string.Compare(originalUnit, "unknown", true) == 0)
            {
                return originalValue;
            }
            else if (string.Compare(originalUnit, "%", true) == 0)
            {
                return originalValue;
            }
            else if (string.Compare(resultUnit, "ph", true) == 0)
            {
                return originalValue;
            }
            else if (string.Compare(originalUnit, "meq/L", true) == 0)
            {
                return originalValue;
            }
            else
            {
                var unitTuple = new Tuple<string, string>(originalUnit, resultUnit);

                if (UnitValueDictionary.ContainsKey(unitTuple))
                {
                    var multiplier = UnitValueDictionary[unitTuple];
                    return originalValue * multiplier;
                }
                else
                {

                    throw new ArgumentOutOfRangeException(
                        string.Format("System does not contain unit convertion combination for {0} and {1}.", originalUnit, resultUnit));
                }
            }

        }

        public static Dictionary<Tuple<string, string>, double> UnitValueDictionary = new Dictionary<Tuple<string, string>, double>
        {
            {new Tuple<string, string>("ug/L", "mg/L"), 0.001},
            {new Tuple<string, string>("mg/L", "ug/L"), 1000.0},
            {new Tuple<string, string>("µg/L", "mg/L"), 0.001},
            {new Tuple<string, string>("mg/L", "µg/L"), 1000.0},
            {new Tuple<string, string>("ug/L", "µg/L"), 1.0},
            {new Tuple<string, string>("µg/L", "ug/L"), 1.0},
            {new Tuple<string, string>("µS/cm", "uS/cm"), 1.0},
            {new Tuple<string, string>("uS/cm", "µS/cm"), 1.0},
            {new Tuple<string, string>("N/A", "%"), 1.0},

        };
    }
}
