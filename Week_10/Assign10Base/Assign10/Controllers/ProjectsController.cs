using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
// added...
using AutoMapper;

namespace Assign10.Controllers
{
    [Authorize]
    public class ProjectsController : ApiController
    {
        // Reference
        private Manager m = new Manager();

        // GET: api/Projects
        /// <summary>
        /// Information for all Projects
        /// </summary>
        /// <returns>Collection of Project objects</returns>
        public IHttpActionResult Get()
        {
            // Get all
            var c = m.ProjectGetAll();

            ProjectsLinked result = new ProjectsLinked
                (Mapper.Map<IEnumerable<ProjectWithLink>>(c));

            return Ok(result);
        }

        // GET: api/Projects/5
        /// <summary>
        /// Information for one Project
        /// </summary>
        /// <param name="id">Project identifier (int)</param>
        /// <returns>Project object</returns>
        public IHttpActionResult Get(int? id)
        {
            // Fetch the object
            var o = m.ProjectGetById(id.GetValueOrDefault());

            // Continue?
            if (o == null) { return NotFound(); }

            // Normal processing for a JSON result
            ProjectLinked result =
                new ProjectLinked(Mapper.Map<ProjectWithLink>(o));

            return Ok(result);
        }

        // POST: api/Projects
        /// <summary>
        /// Add a new Project object
        /// </summary>
        /// <param name="newItem">New Project object (the template has the schema)</param>
        /// <returns>New Project object</returns>
        public IHttpActionResult Post([FromBody]ProjectAdd newItem)
        {
            // Ensure that the URI is clean (and does not have an id parameter)
            if (Request.GetRouteData().Values["id"] != null) { return BadRequest("Invalid request URI"); }

            // Ensure that a "newItem" is in the entity body
            if (newItem == null) { return BadRequest("Must send an entity body with the request"); }

            // Ensure that we can use the incoming data
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            // TODO - Fix this logic in the future
            // This is a clumsy way to do it, but it's quick
            var visibilityChoices = new List<string> { "Private", "Public", "Shared" };
            var match = visibilityChoices
                .SingleOrDefault(v => v.ToLower() == newItem.Visibility.Trim().ToLower());
            if (match == null)
            {
                return BadRequest("Invalid value for the Visibility property");
            }
            else
            {
                // Overwrite the visibility property with the proper-case version
                newItem.Visibility = match;
            }

            // Attempt to add the new object
            var addedItem = m.ProjectAdd(newItem);

            // Continue?
            if (addedItem == null) { return BadRequest("Cannot add the object"); }

            // HTTP 201 with the new object in the entity body
            // Notice how to create the URI for the Location header
            var uri = Url.Link("DefaultApi", new { id = addedItem.Id });

            // Use the factory constructor for the "add new" use case
            ProjectLinked result = new ProjectLinked
                (Mapper.Map<ProjectWithLink>(addedItem), addedItem.Id);

            return Created(uri, result);
        }

        /*
        // PUT: api/Projects/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Projects/5
        public void Delete(int id)
        {
        }
        */
    }
}
