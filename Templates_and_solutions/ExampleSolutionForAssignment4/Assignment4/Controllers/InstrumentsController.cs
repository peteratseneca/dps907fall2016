using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
// added...
using AutoMapper;

namespace Assignment4.Controllers
{
    public class InstrumentsController : ApiController
    {
        // Reference to the data service operations manager class
        private Manager m = new Manager();

        // GET: api/Instruments
        public IHttpActionResult Get()
        {
            return Ok(m.InstrumentGetAll());
        }

        // Attention 25 - Get one method, WSA500, with content negotiation (conneg)
        // GET: api/Instruments/5
        public IHttpActionResult Get(int? id)
        {
            // Attempt to fetch the object, WITH media
            var o = m.InstrumentGetById(id.GetValueOrDefault());

            // Continue?
            if (o == null) { return NotFound(); }


            // Look for an Accept header that starts with "image"
            var imageHeader = Request.Headers.Accept
                .SingleOrDefault(a => a.MediaType.ToLower().StartsWith("image/"));

            if (imageHeader == null)
            {
                // Normal processing for a JSON result
                return Ok(Mapper.Map<InstrumentWithMediaInfo>(o));
            }
            else
            {
                // Special processing for an image result

                // Confirm that a media item exists
                if (o.PhotoMediaLength > 0)
                {
                    // Return the result, using the custom media formatter
                    return Ok(o.PhotoMedia);

                    // Alternative better-quality conneg processing...
                    /*
                    // Get a reference to the media formatter that handles the content type
                    var formatter = GlobalConfiguration.Configuration.Formatters.FindWriter
                        (typeof(byte[]), new System.Net.Http.Headers.MediaTypeHeaderValue(mediaHeader.MediaType));

                    if (formatter == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        // Return the result, ensuring that it is processed by the media formatter
                        return Content(HttpStatusCode.OK, result.PhotoMedia, formatter, result.PhotoContentType);
                    }
                    */
                }
                else
                {
                    // Otherwise, return "not found"
                    // Yes, this is correct. Read the RFC: https://tools.ietf.org/html/rfc7231#section-6.5.4
                    return NotFound();
                }
            }
        }

        // Attention 26 - Get one method, DPS907, with content negotiation (conneg)
        // GET: api/Instruments/5/DPS907
        [Route("api/instruments/{id}/dps907")]
        public IHttpActionResult GetBSD(int? id)
        {
            // Attempt to fetch the object, WITH media
            var o = m.InstrumentGetById(id.GetValueOrDefault());

            // Continue?
            if (o == null) { return NotFound(); }


            // Look for an Accept header that starts with "image"
            var imageHeader = Request.Headers.Accept
                .SingleOrDefault(a => a.MediaType.ToLower().StartsWith("image/"));

            // Look for an Accept header that starts with "audio"
            var audioHeader = Request.Headers.Accept
                .SingleOrDefault(a => a.MediaType.ToLower().StartsWith("audio/"));

            if (imageHeader == null & audioHeader == null)
            {
                // Normal processing for a JSON result
                return Ok(Mapper.Map<InstrumentWithMediaInfo>(o));
            }
            else
            {
                // Special processing for an image result

                if (imageHeader != null)
                {
                    // Confirm that a media item exists
                    if (o.PhotoMediaLength > 0)
                    {
                        // Return the result, using the custom media formatter
                        return Ok(o.PhotoMedia);
                    }
                }

                if (audioHeader != null)
                {
                    // Confirm that a media item exists
                    if (o.SoundClipMediaLength > 0)
                    {
                        // Return the result, using the custom media formatter
                        return Ok(o.SoundClipMedia);
                    }
                }

                // Otherwise, return "not found"
                // Yes, this is correct. Read the RFC: https://tools.ietf.org/html/rfc7231#section-6.5.4
                return NotFound();
            }
        }

        // POST: api/Instruments
        public IHttpActionResult Post([FromBody]InstrumentAdd newItem)
        {
            // Ensure that the URI is clean (and does not have an id parameter)
            if (Request.GetRouteData().Values["id"] != null) { return BadRequest("Invalid request URI"); }

            // Ensure that a "newItem" is in the entity body
            if (newItem == null) { return BadRequest("Must send an entity body with the request"); }

            // Ensure that we can use the incoming data
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            // Attempt to add the new object
            var addedItem = m.InstrumentAdd(newItem);

            // Continue?
            if (addedItem == null) { return BadRequest("Cannot add the object"); }

            // HTTP 201 with the new object in the entity body
            // Notice how to create the URI for the Location header
            var uri = Url.Link("DefaultApi", new { id = addedItem.Id });

            return Created(uri, addedItem);
        }

        // PUT: api/Instruments/5/SetPhoto
        // Notice the use of the [HttpPut] attribute, which is an alternative to the method name starting with "Put..."
        [Route("api/instruments/{id}/setphoto")]
        [HttpPut]
        public IHttpActionResult InstrumentPhoto(int id, [FromBody]byte[] photo)
        {
            // Attention 27 - Set photo, notice the size limit, and response

            if (photo.Length > 204800)
            {
                // Attention 28 - Sound clip too large? Return HTTP 413
                return StatusCode(HttpStatusCode.RequestEntityTooLarge);
            }

            // Get the Content-Type header from the request
            var contentType = Request.Content.Headers.ContentType.MediaType;

            // Attempt to save
            if (m.InstrumentSetPhoto(id, contentType, photo))
            {
                // By convention, we have decided to return HTTP 204
                // It's a 'success' code, but there's no content for a 'command' task
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                // Uh oh, some error happened, so tell the requestor
                return BadRequest("Unable to set the photo");
            }
        }

        // PUT: api/Instruments/5/SetSoundClip
        // Notice the use of the [HttpPut] attribute, which is an alternative to the method name starting with "Put..."
        [Route("api/instruments/{id}/setsoundclip")]
        [HttpPut]
        public IHttpActionResult InstrumentSoundClip(int id, [FromBody]byte[] soundClip)
        {
            // Attention 29 - Set sound clip, notice the size limit, and response

            if (soundClip.Length > 204800)
            {
                // Attention 30 - Sound clip too large? Return HTTP 413
                return StatusCode(HttpStatusCode.RequestEntityTooLarge);
            }

            // Get the Content-Type header from the request
            var contentType = Request.Content.Headers.ContentType.MediaType;

            // Attempt to save
            if (m.InstrumentSetSoundClip(id, contentType, soundClip))
            {
                // By convention, we have decided to return HTTP 204
                // It's a 'success' code, but there's no content for a 'command' task
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                // Uh oh, some error happened, so tell the requestor
                return BadRequest("Unable to set the sound clip");
            }
        }
        /*
        // PUT: api/Instruments/5
        public void Put(int id, [FromBody]string value)
        {
        }
        */

        // DELETE: api/Instruments/5
        public void Delete(int id)
        {
            // In a controller 'Delete' method, a void return type will
            // automatically generate a HTTP 204 "No content" response
            m.InstrumentDelete(id);
        }
    }
}
