using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Core;
using Baldr.Models;

namespace Baldr.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        protected IUnitOfWork UnitOfWork;

        public ValuesController(IUnitOfWork unitOfWork) {
            this.UnitOfWork = unitOfWork;
        }
        
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2", "value3" };
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
