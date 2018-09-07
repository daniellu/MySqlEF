using Hatfield.AnalyteManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hatfield.AnalyteManagement.GuidelineCalculation
{
    public class DependentDecider
    {
        public static bool Decide(
            ref GuidelineCalculationRequestViewModel data,
            IEnumerable<GuidelineCalculationRequestViewModel> dataset,
            DependentDecision decision,
            AnalyteGuideline guideline)
        {
            if (decision == DependentDecision.ExistingValue)
            {
                return true;
            }

            if (guideline.ValueType == Constants.EquationValueType)
            {
                foreach (var analyte in ExtractDependAnalyteNames(guideline.GuidelineValue))
                {
                    var result = FindDependValue(
                            ref data, dataset,
                            analyte,
                            dic[analyte]
                        );
                    if (!result)
                    {
                        return false;
                    }
                }
                

                return true;
            }

            return true;
        }

        private static Dictionary<string, Func<double, GuidelineCalculationRequestViewModel, GuidelineCalculationRequestViewModel>> dic = new Dictionary<string, Func<double, GuidelineCalculationRequestViewModel, GuidelineCalculationRequestViewModel>>
        {
            {
                Constants.MathE,
                (double value, GuidelineCalculationRequestViewModel point) => {
                    point.MathE = Math.E;
                    return point;
                }
            },
            {
                Constants.PH_AnalyteName,
                (double value, GuidelineCalculationRequestViewModel point) => {
                    point.PH = value;
                    return point;
                }
            },
            {
                Constants.Hardness_AnalyteName,
                (double value, GuidelineCalculationRequestViewModel point) => {
                    point.Hardness = value;
                    return point;
                }
            },
            {
                Constants.Chloride_AnalyteName,
                (double value, GuidelineCalculationRequestViewModel point) => {
                    point.Chloride = value;
                    return point;
                }
            },
        };



        private static bool FindDependValue(ref GuidelineCalculationRequestViewModel data,
            IEnumerable<GuidelineCalculationRequestViewModel> dataset,
            string dependAnalyteName, Func<double, GuidelineCalculationRequestViewModel, GuidelineCalculationRequestViewModel> callback)
        {
            if (dependAnalyteName == Constants.MathE)
            {
                data = callback(Math.E, data);
                return true;
            }

            var pointToMatch = data;
            var depend = dataset.FirstOrDefault(x => x.DateTime.Date == pointToMatch.DateTime.Date && 
                                                    x.Site == pointToMatch.Site && 
                                                    String.Compare(x.Analyte, dependAnalyteName, true) == 0);

            if (depend == null || depend.Value == null)
            {
                return false;
            }
            data = callback(depend.Value.Value, data);
            return true;
        }

        private static IEnumerable<string> ExtractDependAnalyteNames(string equation)
        {
            string pattern = @"\[(.*?)\]";
            var matches = Regex.Matches(equation, pattern, RegexOptions.IgnoreCase);

            var extractedAnalyteNames = new List<string>();
            foreach (Match match in matches)
            {
                extractedAnalyteNames.Add(match.Value.Substring(1, match.Value.Length - 2));
            }

            return extractedAnalyteNames.Distinct();
        }
    }
}
