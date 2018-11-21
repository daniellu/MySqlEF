using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterResourceData.WaterQuality.Ammonia
{
    public class AmmoniaGuidelineValueCalculator
    {
        public static string TemperatureAnalyteName = "Temp";
        private static double defaultTemp = 20.0;
        public double? CalculateGuidelineValue(string guidelineRawData, double analyteValue, Dictionary<string, object> dependencyValues)
        {
            var temp = ExtractDependValue(TemperatureAnalyteName, dependencyValues);
            var ph = ExtractDependValue("PH", dependencyValues);
            var lookupTable = ExtractLookupTable(guidelineRawData);
            var phValueToLookup = DecidePHValueToLookup(ph, lookupTable);
            var tempValueToLookup = DecideTempValueToLookup(temp, lookupTable);

            if (!phValueToLookup.HasValue || !tempValueToLookup.HasValue)
            {
                return null;
            }
            else
            {
                var matchedCell = lookupTable.Cells.FirstOrDefault(x => x.PH == phValueToLookup && x.Temp == tempValueToLookup);
                return matchedCell == null ? null : (double?)matchedCell.Guideline;
            }
        }

        private double ExtractDependValue(string key, Dictionary<string, object> dependencyValues)
        {
            if (dependencyValues.ContainsKey(key))
            {
                double t = defaultTemp;
                if (Double.TryParse(dependencyValues[key].ToString(), out t))
                {
                    return t;
                }
                else
                {
                    throw new ArgumentException("Depend data is not a valid numeric value, system fails to calculate Ammonia guideline.");
                }
            }
            else
            {
                return defaultTemp;
            }
        }

        private AmmoniaLookupTable ExtractLookupTable(string guidelineRawData)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<AmmoniaLookupTable>(guidelineRawData);
        }

        private double? DecidePHValueToLookup(double ph, AmmoniaLookupTable table)
        {
            var phValues = table.Cells.Select(x => x.PH).OrderBy(x => x).ToList();
            if (ph < phValues.Min() || ph > phValues.Max())
            {
                return null;
            }
            else
            {
                for (var i = 0; i < phValues.Count; i++)
                {
                    if (ph == phValues[i])
                    {
                        return phValues[i];
                    }
                    if (ph > phValues[i] && ph < phValues[i + 1])
                    {
                        return phValues[i + 1];
                    }
                }
                throw new ArgumentOutOfRangeException("System fails to decide PH value to look up for ph: " + ph);
            }
        }

        private double? DecideTempValueToLookup(double temp, AmmoniaLookupTable table)
        {
            var tempValues = table.Cells.Select(x => x.Temp).OrderBy(x => x).ToList();
            if (temp < tempValues.Min() || temp > tempValues.Max())
            {
                return null;
            }
            else
            {
                for (var i = 0; i < tempValues.Count; i++)
                {
                    if (temp == tempValues[i])
                    {
                        return tempValues[i];
                    }
                    if (temp > tempValues[i] && temp < tempValues[i + 1])
                    {
                        return tempValues[i + 1];
                    }
                }
                throw new ArgumentOutOfRangeException("System fails to decide temp value to look up for temp: " + temp);
            }
        }
    }
}
