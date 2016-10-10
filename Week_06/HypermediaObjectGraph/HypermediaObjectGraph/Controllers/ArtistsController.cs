using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
// added...
using AutoMapper;
using System.Web.Http.Cors;

namespace HypermediaObjectGraph.Controllers
{
    // Attention 22 - Enable CORS for this controller

    [EnableCors(origins: "*", headers: "*", methods: "GET")]
    public class ArtistsController : ApiController
    {
        // Reference to the manager object
        private Manager m = new Manager();

        // GET: api/Artists
        public IHttpActionResult Get()
        {
            var c = m.ArtistGetAll();

            var result = new ArtistsLinked(Mapper.Map<IEnumerable<ArtistWithLink>>(c));

            return Ok(result);
        }

        // Method that does NOT create links for the nested object(s)
        /*
        // GET: api/Artists/5
        public IHttpActionResult Get(int? id)
        {
            // Attempt to fetch the object
            var o = m.ArtistGetById(id.GetValueOrDefault());

            // Continue?
            if (o == null)
            {
                return NotFound();
            }
            else
            {
                // Create a hypermedia representation
                ArtistLinked result = new ArtistLinked
                    (Mapper.Map<ArtistWithLink>(o));

                return Ok(result);
            }
        }
        */

        // Attention 15 - Get artist with its albums collection
        // GET: api/Artists/5
        public IHttpActionResult Get(int? id)
        {
            // Attempt to fetch the object
            var o = m.ArtistGetById(id.GetValueOrDefault());

            // Continue?
            if (o == null)
            {
                return NotFound();
            }
            else
            {
                // Attention 16 - Notice that it starts the same way as before, by creating an ArtistLinked object

                // Create a hypermedia representation
                ArtistLinked result = new ArtistLinked
                    (Mapper.Map<ArtistWithLink>(o));

                // Attention 17 - Then, we have go deeper, and add a link relation for each album in its collection

                // Next, go back into the nested Albums collection,
                // and add a link for each of those items
                // Comment out this foreach block if ArtistWithAlbums includes an AlbumBase collection
                foreach (var item in result.Item.Albums)
                {
                    // Add album link
                    item.Link = new Link
                    {
                        Href = "api/albums/" + item.AlbumId,
                        Method = "GET",
                        Rel = "item"
                    };
                }

                return Ok(result);
            }
        }

        /*
        // POST: api/Artists
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Artists/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Artists/5
        public void Delete(int id)
        {
        }
        */
    }
}
