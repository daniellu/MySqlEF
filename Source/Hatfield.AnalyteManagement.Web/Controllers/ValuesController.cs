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

namespace Hatfield.AnalyteManagement.Web.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        private readonly DbContext _dbContext;

        public ValuesController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/values
        [Route("API/Guidelines")]
        public IEnumerable<GuidelineListViewModel> Get()
        {
            var guidelines = _dbContext.Set<Guideline>()
                .Select(x => 
                    new GuidelineListViewModel
                    {
                        Name = x.Name,
                        ShortCode = x.ShortCode,
                        AnalyteGuidelines = x.AnalyteGuidelines.Select(l => new LabAnalyteGuideline {
                            Anlayte = l.AnalyteName,
                            GuidelineValue = l.GuidelineValue,
                            Unit = l.Unit
                        })
                    }).ToList();
            return guidelines;
        }

        // GET api/values/5
        [Route("API/Analytes")]
        public IEnumerable<AnalyteListViewModel> GetAnalytes()
        {
            return _dbContext.Set<LabAnalyte>().Select(x => new AnalyteListViewModel { Name = x.Name, CAS = x.CAS_Code }).ToList();
        }
    }
}
