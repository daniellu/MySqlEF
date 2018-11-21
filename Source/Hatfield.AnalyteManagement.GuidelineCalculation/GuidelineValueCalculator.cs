using Hatfield.AnalyteManagement.Domain;
using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WaterResourceData.WaterQuality.Ammonia;

namespace Hatfield.AnalyteManagement.GuidelineCalculation
{
    public class GuidelineValueCalculator : IGuidelineValueCalculator
    {
        private readonly ExccedanceDeciderFactory _factory;

        public GuidelineValueCalculator()
        {
            _factory = ExccedanceDeciderFactory.Initial();
        }

        public IEnumerable<GuidelineCalculationResultViewModel> Calculate(
            IEnumerable<GuidelineCalculationRequestViewModel> dataset, 
            IEnumerable<AnalyteGuideline> guidelines,
            DependentDecision dependentDecision)
        {
            var unitConverter = new UnitValueConverter();
            var data = new List<GuidelineCalculationResultViewModel>();
            for (var i = 0; i < dataset.Count(); i++)
            {
                var currentPoint = dataset.ElementAt(i);
                var result = currentPoint.ToResult();

                var matchedGuideline = guidelines.FirstOrDefault(x => x.AnalyteName== currentPoint.Analyte);
                if (matchedGuideline == null)
                {
                    data.Add(result);
                    continue;
                }

                var isDependentFound = DependentDecider.Decide(ref currentPoint, dataset, dependentDecision, matchedGuideline);
                if (!isDependentFound)
                {
                    data.Add(result);
                    continue;
                }

                var guidelineValue = this.CalculateGuidelineValue(currentPoint, matchedGuideline);
                if (!String.IsNullOrEmpty(guidelineValue.Guideline))
                {
                    result.GuidelineValueString = guidelineValue.GuidelineValueString;
                    var parsedOption = (ComparisonOperation)Enum.Parse(typeof(ComparisonOperation), matchedGuideline.ComparisonOperation);
                    var valueToCompare = unitConverter.Convert(result.Value, result.Unit, matchedGuideline.Unit);
                    result.IsExceedance = CalculateIfExcessGuideline(guidelineValue, valueToCompare, parsedOption);
                    result.GuidelineUnit = matchedGuideline.Unit;
                }
                data.Add(result);
                
            }
            

            return data;            
        }

        public AnalyteGuidelineValue CalculateGuidelineValue(
            GuidelineCalculationRequestViewModel data,             
            AnalyteGuideline guideline)
        {            
            if (String.IsNullOrEmpty(guideline.GuidelineValue))
            {
                throw new ArgumentNullException("System fail to calculate guideline value for " + guideline.AnalyteName);
            }

            if (guideline.ValueType == Constants.DoubleValueType || guideline.ValueType == Constants.RangeValueType)
            {
                return this.CalculateStaticGuidelineValue(guideline);
            }
            else if (guideline.ValueType == Constants.EquationValueType)
            {
                double? parsedValue = EvaluateEquation(guideline.GuidelineValue, data.ToEquationParameters());

                return new SimpleGuidelineValue(guideline.Guideline.Name, guideline.AnalyteName, guideline.ValueType, guideline.Unit, parsedValue);
            }
            else if (guideline.ValueType == Constants.AmmoniaLookupTableValueType)
            {
                var ammoniaGuidelineCalculator = new AmmoniaGuidelineValueCalculator();
                double? parsedValue = ammoniaGuidelineCalculator.CalculateGuidelineValue(guideline.GuidelineValue, data.Value.Value, data.ToEquationParameters());

                return new SimpleGuidelineValue(guideline.Guideline.Name, guideline.AnalyteName, guideline.ValueType, guideline.Unit, parsedValue);
            }
            else
            {
                throw new ArgumentException(guideline.ValueType + " is not a supported value type for standard value calculator.");
            }
        }

        public bool CalculateIfExcessGuideline(AnalyteGuidelineValue guideline, double? value, ComparisonOperation operation)
        {
            var decider = _factory.BuildDecider(guideline.GetType());
            var isExcessed = decider.CalculateIfExcessGuideline(guideline, value, operation);
            return isExcessed;
        }

        private AnalyteGuidelineValue CalculateStaticGuidelineValue(AnalyteGuideline guideline)
        {
            if (String.IsNullOrEmpty(guideline.GuidelineValue))
            {
                throw new ArgumentNullException("System fail to calculate guideline value for " + guideline.AnalyteName);
            }
            switch (guideline.ValueType)
            {
                case Constants.DoubleValueType:
                    return CalculateStaticSimpleValue(guideline);
                case Constants.RangeValueType:
                    return CalculateStaticRangeValue(guideline);
                default:
                    throw new ArgumentException("Sample guideline value calculate function could only support double value type/range value type.");
            }
        }

        private SimpleGuidelineValue CalculateStaticSimpleValue(AnalyteGuideline guideline)
        {
            try
            {
                var parsedValue = double.Parse(guideline.GuidelineValue);
                var data = new SimpleGuidelineValue(guideline.Guideline.Name, guideline.AnalyteName, 
                                                guideline.ValueType, guideline.Unit, parsedValue);
                return data;
            }
            catch (Exception)
            {
                throw new ArgumentException(guideline.GuidelineValue + " could not be parsed as double for guideline value.");
            }
        }

        /// <summary>
        /// expected guideline value string "6.5<=[PH] and [PH]<=9.0"
        /// </summary>
        /// <param name="standardValue"></param>
        /// <returns></returns>
        private RangeGuidelineValue CalculateStaticRangeValue(AnalyteGuideline guideline)
        {
            string pattern = @"[\d]{1,10}(\.[\d]{1,4})?";
            var matches = Regex.Matches(guideline.GuidelineValue, pattern, RegexOptions.IgnoreCase);

            try
            {
                var minValue = Double.Parse(matches[0].Value);
                var maxValue = Double.Parse(matches[1].Value);

                var data = new RangeGuidelineValue(guideline.Guideline.Name, guideline.AnalyteName, 
                                                    guideline.ValueType, guideline.Unit, minValue, maxValue);
                return data;
            }
            catch (Exception)
            {
                throw new SystemException("System fails to parse range value " + guideline.GuidelineValue);
            }

        }

        private double? EvaluateEquation(string equation, Dictionary<string, object> parameterDictionary)
        {
            var expression = new Expression(equation);
            if (parameterDictionary != null && parameterDictionary.Any())
            {
                foreach (var key in parameterDictionary.Keys)
                {                    
                    expression.Parameters[key] = parameterDictionary[key];
                }
            }

            try
            {
                var evaluatedValue = expression.Evaluate();

                var parsedValue = Math.Round(double.Parse(evaluatedValue.ToString()), 3);
                return parsedValue;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Evaluation equartion " + equation + " fail.");
            }

        }
    }
}
