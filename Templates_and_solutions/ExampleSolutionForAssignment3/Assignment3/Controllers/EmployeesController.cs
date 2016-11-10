using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment3.Controllers
{
    public class EmployeesController : ApiController
    {
        // Reference to the data manager
        private Manager m = new Manager();

        // GET: api/Employees
        public IHttpActionResult Get()
        {
            return Ok(m.EmployeeGetAll());
        }

        // GET: api/Employees/5
        public IHttpActionResult Get(int? id)
        {
            // Attempt to get the matching object
            var o = m.EmployeeGetByIdWithDetails(id.GetValueOrDefault());

            if (o == null)
            {
                return NotFound();
            }
            else
            {
                // Pass the object to the view
                return Ok(o);
            }
        }

        // POST: api/Employees
        public IHttpActionResult Post([FromBody]EmployeeAdd newItem)
        {
            // Attention 35 - Nothing new here, a repeat of a code example from last week

            // Ensure that the URI is clean (and does not have an id parameter)
            if (Request.GetRouteData().Values["id"] != null) { return BadRequest("Invalid request URI"); }

            // Ensure that a "newItem" is in the entity body
            if (newItem == null) { return BadRequest("Must send an entity body with the request"); }

            // Ensure that we can use the incoming data
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            // Attempt to add the new object
            var addedItem = m.EmployeeAdd(newItem);

            // Continue?
            if (addedItem == null) { return BadRequest("Cannot add the object"); }

            // HTTP 201 with the new object in the entity body
            // Notice how to create the URI for the Location header
            var uri = Url.Link("DefaultApi", new { id = addedItem.EmployeeId });

            return Created(uri, addedItem);
        }

        // PUT: api/Employees/5/SetSupervisor
        [Route("api/employees/{id}/setsupervisor")]
        public void PutSetSupervisor(int id, [FromBody]EmployeeSupervisor item)
        {
            // Attention 36 - Employee set supervisor - command pattern

            // Ensure that an "item" is in the entity body
            if (item == null) { return; }

            // Ensure that the id value in the URI matches the id value in the entity body
            if (id != item.EmployeeId) { return; }

            // Ensure that we can use the incoming data
            if (ModelState.IsValid)
            {
                // Attempt to update the item
                m.EmployeeSetSupervisor(item);
            }
            else
            {
                return;
            }
        }

        // PUT: api/Employees/5/SetDirectReports
        [Route("api/employees/{id}/setdirectreports")]
        public void PutSetDirectReports(int id, [FromBody]EmployeeDirectReports item)
        {
            // Attention 37 - Employee set direct-reports - command pattern

            // Ensure that an "item" is in the entity body
            if (item == null) { return; }

            // Ensure that the id value in the URI matches the id value in the entity body
            if (id != item.EmployeeId) { return; }

            // Ensure that we can use the incoming data
            if (ModelState.IsValid)
            {
                // Attempt to update the item
                m.EmployeeSetDirectReports(item);
            }
            else
            {
                return;
            }
        }

        /*
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
