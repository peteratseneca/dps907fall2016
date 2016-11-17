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
    public class MediasController : ApiController
    {        
        // Reference
        private Manager m = new Manager();

        // GET: api/Medias
        /// <summary>
        /// Information for all Media objects
        /// </summary>
        /// <returns>Collection of Media objects</returns>
        public IHttpActionResult Get()
        {
            // Get all
            var c = m.MediaGetAll();

            MediasLinked result = new MediasLinked
                (Mapper.Map<IEnumerable<MediaWithLink>>(c));

            return Ok(result);
        }

        // GET: api/Medias/5
        /// <summary>
        /// Information for one Media
        /// </summary>
        /// <param name="id">Media identifier (int)</param>
        /// <returns>Media object</returns>
        public IHttpActionResult Get(int? id)
        {
            // This method DOES use content negotiation (aka conneg) 

            // Fetch the object
            var fetchedObject = m.MediaGetById(id.GetValueOrDefault());

            // Continue?
            if (fetchedObject == null) { return NotFound(); }

            // Check whether this is a request for a media item
            // Look for the source code in the Utility.cs file
            var isRequestForMediaItem = Utility.IsRequestForMediaItem(Request.Headers);

            if (isRequestForMediaItem)
            {
                // Return media item
                return Ok(fetchedObject.ItemMedia);
            }
            else
            {
                // Normal processing for a JSON result
                MediaLinked result =
                    new MediaLinked(Mapper.Map<MediaWithLink>(fetchedObject));

                return Ok(result);
            }
        }

        // POST: api/Medias
        /// <summary>
        /// Add a new Media object
        /// </summary>
        /// <param name="newItem">New Media object (the template has the schema)</param>
        /// <returns>New Media object</returns>
        public IHttpActionResult Post([FromBody]MediaAdd newItem)
        {
            // Ensure that the URI is clean (and does not have an id parameter)
            if (Request.GetRouteData().Values["id"] != null) { return BadRequest("Invalid request URI"); }

            // Ensure that a "newItem" is in the entity body
            if (newItem == null) { return BadRequest("Must send an entity body with the request"); }

            // Ensure that we can use the incoming data
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            // Attempt to add the new object
            var addedItem = m.MediaAdd(newItem);

            // Continue?
            if (addedItem == null) { return BadRequest("Cannot add the object"); }

            // HTTP 201 with the new object in the entity body
            // Notice how to create the URI for the Location header
            var uri = Url.Link("DefaultApi", new { id = addedItem.Id });

            // Use the factory constructor for the "add new" use case
            MediaLinked result = new MediaLinked
                (Mapper.Map<MediaWithLink>(addedItem), addedItem.Id);

            return Created(uri, result);
        }

        // PUT: api/Medias/5/SetMediaItem
        /// <summary>
        /// Add a media item to the Media object
        /// </summary>
        /// <param name="id">Media object identifier (int)</param>
        /// <param name="media">Media item bytes</param>
        /// <returns>Success: HTTP 204 or 400</returns>
        [Route("api/medias/{id}/setmediaitem")]
        public IHttpActionResult PutMediaItem(int id, [FromBody]byte[] media)
        {
            // Get the Content-Type header from the request
            var contentType = Request.Content.Headers.ContentType.MediaType;

            // Attempt to save
            if (m.MediaItemAdd(id, contentType, media))
            {
                // By convention, we have decided to return HTTP 204
                // It's a 'success' code, but there's no content for a 'command' task
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                // Uh oh, some error happened, so tell the user
                return BadRequest("Unable to set the media item");
            }
        }

        /*
        // PUT: api/Medias/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Medias/5
        public void Delete(int id)
        {
        }
        */
    }
}
