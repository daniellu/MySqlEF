using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Domain;
using Data;
using System.Data.Entity;

namespace ProjectTemplate.Controllers
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
        public IEnumerable<string> Get()
        {
            var allBooks = _dbContext.Set<person>().SelectMany(x => x.books.Select(l => l.Name));
            return allBooks;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
