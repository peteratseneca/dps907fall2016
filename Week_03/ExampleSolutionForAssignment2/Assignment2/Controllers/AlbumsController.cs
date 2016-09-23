using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment2.Controllers
{
    public class AlbumsController : ApiController
    {
        // Attention 35 - Reference to the manager object
        private Manager m = new Manager();

        // GET: api/Albums
        public IHttpActionResult Get()
        {
            return Ok(m.AlbumGetAll());
        }

        // GET: api/Albums/5
        public IHttpActionResult Get(int? id)
        {
            // Attempt to fetch the object
            var o = m.AlbumGetById(id.GetValueOrDefault());

            // Continue?
            if (o == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(o);
            }
        }

        // Attention 36 - Get one, with associated objects, use attribute routing

        // GET: api/Albums/5/WithArtist
        [Route("api/albums/{id}/withartist")]
        public IHttpActionResult GetWithArtist(int? id)
        {
            // Attempt to fetch the object
            var o = m.AlbumGetByIdWithArtist(id.GetValueOrDefault());

            // Continue?
            if (o == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(o);
            }
        }

        // POST: api/Albums
        public IHttpActionResult Post([FromBody]AlbumAdd newItem)
        {
            // Ensure that the URI is clean (and does not have an id parameter)
            if (Request.GetRouteData().Values["id"] != null) { return BadRequest("Invalid request URI"); }

            // Ensure that a "newItem" is in the entity body
            if (newItem == null) { return BadRequest("Must send an entity body with the request"); }

            // Ensure that we can use the incoming data
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            // Attempt to add the new object
            var addedItem = m.AlbumAdd(newItem);

            // Continue?
            if (addedItem == null) { return BadRequest("Cannot add the object"); }

            // HTTP 201 with the new object in the entity body
            // Notice how to create the URI for the Location header
            var uri = Url.Link("DefaultApi", new { id = addedItem.AlbumId });

            return Created(uri, addedItem);
        }

        // PUT: api/Albums/5
        public IHttpActionResult PutTitle(int? id, [FromBody]AlbumEditTitle editedItem)
        {
            // Ensure that an "editedItem" is in the entity body
            if (editedItem == null)
            {
                return BadRequest("Must send an entity body with the request");
            }

            // Ensure that the id value in the URI matches the id value in the entity body
            if (id.GetValueOrDefault() != editedItem.AlbumId)
            {
                return BadRequest("Invalid data in the entity body");
            }

            // Ensure that we can use the incoming data
            if (ModelState.IsValid)
            {
                // Attempt to update the item
                var changedItem = m.AlbumEditTitle(editedItem);

                // Notice the ApiController convenience methods
                if (changedItem == null)
                {
                    // HTTP 400
                    return BadRequest("Cannot edit the object");
                }
                else
                {
                    // HTTP 200 with the changed item in the entity body
                    return Ok<AlbumBase>(changedItem);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/Albums/5
        public void Delete(int id)
        {
            // In a controller 'Delete' method, a void return type will
            // automatically generate a HTTP 204 "No content" response
            m.AlbumDelete(id);
        }
    }
}
