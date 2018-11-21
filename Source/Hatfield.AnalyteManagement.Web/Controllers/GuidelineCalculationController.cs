using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Hatfield.AnalyteManagement.Domain;
using Hatfield.AnalyteManagement.Data;
using System.Data.Entity;
using Hatfield.AnalyteManagement.Web.Models;
using Hatfield.AnalyteManagement.GuidelineCalculation;

namespace Hatfield.AnalyteManagement.Web.Controllers
{
    public class GuidelineCalculationController : ApiController
    {
        private readonly DbContext _dbContext;

        public GuidelineCalculationController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("API/Calculate")]
        public IEnumerable<CalculationResultViewModel> Calculate([FromBody]CalculationRequestViewModel data)
        {
            var guideline = data.guidelineName.ElementAt(0);
            var option = ParseDecisionString(data.option);
            var guidelineOfAnalytes = _dbContext.Set<Guideline>().Where(x => x.ShortCode == guideline).SelectMany(x => x.AnalyteGuidelines).ToList();
            var calculator = new GuidelineValueCalculator();
            var results = calculator.Calculate(data.data, guidelineOfAnalytes, option);

            return results.Select(x => new CalculationResultViewModel
            {
                Analyte = x.Analyte,
                Site = x.Site,
                DateTime = x.DateTime,
                Value = x.Value,
                Unit = x.Unit,
                Guideline = x.GuidelineValueString,
                GuidelineUnit = x.GuidelineUnit,
                IsExceedance = x.IsExceedance
            });
            
        }

        [HttpPost]
        [Route("API/CalculateSingle")]
        public CalculationResultViewModel CalculateSingle([FromBody]SingleCalculationRequestViewModel data)
        {
            var guidelineOfAnalytes = _dbContext.Set<Guideline>().Where(x => x.ShortCode == data.guidelineName).SelectMany(x => x.AnalyteGuidelines).ToList();
            var calculator = new GuidelineValueCalculator();

            var requestData = ConstructRequestData(data);
            var results = calculator.Calculate(requestData, guidelineOfAnalytes, DependentDecision.ExistingValue);

            var actualResult = results.Select(x => new CalculationResultViewModel
            {
                Analyte = x.Analyte,
                Site = x.Site,
                DateTime = x.DateTime,
                Value = x.Value,
                Unit = x.Unit,
                Guideline = x.GuidelineValueString,
                GuidelineUnit = x.GuidelineUnit,
                IsExceedance = x.IsExceedance
            });
            return actualResult.FirstOrDefault();

        }

        private IEnumerable<GuidelineCalculationRequestViewModel> ConstructRequestData(SingleCalculationRequestViewModel data)
        {
            var result = new GuidelineCalculationRequestViewModel
            {
                Site = "Test",
                Analyte = data.analyte,
                DateTime = DateTime.Now,
                MathE = Math.E,
                Value = data.value,
                PH = data.pH,
                Chloride = data.chloride,
                Hardness = data.hardness,
                Unit = data.unit,
                Temperature = data.temperature
            };

            return new List<GuidelineCalculationRequestViewModel> { result };
        }

        private DependentDecision ParseDecisionString(IEnumerable<string> decisions)
        {            
            try
            {
                return (DependentDecision)Enum.Parse(typeof(DependentDecision), decisions.FirstOrDefault());
            }
            catch (Exception)
            {
                return DependentDecision.Automatically;
            }
        }

    }

    
}
