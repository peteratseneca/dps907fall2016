using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
// added...
using AutoMapper;

// Attention 01 - This source code file has "Api" inserted into the file name

// Attention 02 - Add ".Api" to the namespace statement

namespace WebAndApiControllers.Controllers.Api
{
    public class EmployeesController : ApiController
    {
        // Reference to the Manager object
        private Manager m = new Manager();

        // GET: api/Employees
        public IHttpActionResult Get()
        {
            var c = m.EmployeeGetAll();

            // Create a hypermedia representation
            EmployeesLinked result = new EmployeesLinked
                (Mapper.Map<IEnumerable<EmployeeWithLink>>(c));

            return Ok(result);
        }

        /*
        // GET: api/Employees/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Employees
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Employees/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Employees/5
        public void Delete(int id)
        {
        }
        */
    }
}
