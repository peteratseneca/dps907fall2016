﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
// added...
using AutoMapper;

namespace LinkRelationsIntro.Controllers
{
    public class EmployeesController : ApiController
    {
        // Reference to the data service operations manager class
        private Manager m = new Manager();

        // Attention 15 - Get all
        // GET: api/Employees
        /// <summary>
        /// All employees, ordered by employee name
        /// </summary>
        /// <returns>Collection of EmployeeBase (linked) objects</returns>
        public IHttpActionResult Get()
        {
            // Get all
            var c = m.EmployeeGetAll();

            // Create a hypermedia representation
            EmployeesLinked result = new EmployeesLinked
                (Mapper.Map<IEnumerable<EmployeeWithLink>>(c));

            return Ok(result);
        }

        // Attention 16 - Get one by its identifier
        // GET: api/Employees/5
        /// <summary>
        /// One specific employee, fetched using its identifier
        /// </summary>
        /// <param name="id">Employee identifier</param>
        /// <returns>EmployeeBase (linked) object</returns>
        public IHttpActionResult Get(int? id)
        {
            // Attempt to fetch the object
            var o = m.EmployeeGetById(id.GetValueOrDefault());

            // Continue?
            if (o == null)
            {
                return NotFound();
            }
            else
            {
                // Create a hypermedia representation
                EmployeeLinked result = new EmployeeLinked
                    (Mapper.Map<EmployeeWithLink>(o));

                return Ok(result);
            }
        }

        // Attention 17 - Add new
        // POST: api/Employees
        /// <summary>
        /// Add a new employee object
        /// </summary>
        /// <param name="newItem">Fully-configured employee</param>
        /// <returns>New employee object, with store-genereated identifier</returns>
        public IHttpActionResult Post([FromBody]EmployeeAdd newItem)
        {
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

            // Create a hypermedia representation
            // Attention 18 - This "add new" use case must use the second constructor
            EmployeeLinked result = new EmployeeLinked
                (Mapper.Map<EmployeeWithLink>(addedItem), addedItem.EmployeeId);

            return Created(uri, result);
        }

        // Attention 20 - Edit existing
        // PUT: api/Employees/5
        /// <summary>
        /// Edit an employee (contact info only)
        /// </summary>
        /// <param name="id">Employee identifier</param>
        /// <param name="editedItem">Updated contact info</param>
        /// <returns>Employee object with all updated values</returns>
        public IHttpActionResult Put(int? id, [FromBody]EmployeeEditContactInfo editedItem)
        {
            // Ensure that an "editedItem" is in the entity body
            if (editedItem == null)
            {
                return BadRequest("Must send an entity body with the request");
            }

            // Ensure that the id value in the URI matches the id value in the entity body
            if (id.GetValueOrDefault() != editedItem.EmployeeId)
            {
                return BadRequest("Invalid data in the entity body");
            }

            // Ensure that we can use the incoming data
            if (ModelState.IsValid)
            {
                // Attempt to update the item
                var changedItem = m.EmployeeEditContactInfo(editedItem);

                // Notice the ApiController convenience methods
                if (changedItem == null)
                {
                    // HTTP 400
                    return BadRequest("Cannot edit the object");
                }
                else
                {
                    // Create a hypermedia representation
                    EmployeeLinked result = new EmployeeLinked
                        (Mapper.Map<EmployeeWithLink>(changedItem));

                    // HTTP 200 with the changed item in the entity body
                    return Ok(result);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // Delete item
        // DELETE: api/Employees/5
        /// <summary>
        /// Delete an employee
        /// </summary>
        /// <param name="id">Employee identifier</param>
        public void Delete(int id)
        {
            // In a controller 'Delete' method, a void return type will
            // automatically generate a HTTP 204 "No content" response
            m.EmployeeDelete(id);
        }
    }
}
