using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Baldr.Models;
using Microsoft.Extensions.Logging;
using Core;

namespace Baldr.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        protected IUnitOfWork UnitOfWork;
        protected ILogger Logger;

        public ValuesController(IUnitOfWork unitOfWork, ILogger<ValuesController> logger) {
            this.UnitOfWork = unitOfWork;
            this.Logger = logger;
        }
        
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            this.Logger.LogInformation("Getting values");
            return new string[] { "value1", "value2", "value3", "value4" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {


            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            var institution = new Institution
            {
                ContactInfo = new Contact {
                    Name = "Wells Fargo",
                    Address1 = "123 Easy St.",
                    Address2 = "St. 12301",
                    Address3 = "Bob",
                    City = ""

                }
            };
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
