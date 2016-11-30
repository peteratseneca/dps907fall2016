using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
// added...
using AutoMapper;

namespace Assignment5.Controllers
{
    public class CustomersController : ApiController
    {
        // Reference
        private Manager m = new Manager();

        // GET: api/Customers
        /// <summary>
        /// All customers
        /// </summary>
        /// <returns>Collection of customer objects</returns>
        public IHttpActionResult Get()
        {
            var c = m.CustomerGetAll();

            // Attention 18 - Customer, get all, create a hypermedia representation
            CustomersLinked result = new CustomersLinked
                (Mapper.Map<IEnumerable<CustomerWithLink>>(c));

            return Ok(result);
        }

        // GET: api/Customers/5
        /// <summary>
        /// One customer, by its identifier
        /// </summary>
        /// <param name="id">Customer identifier</param>
        /// <returns>Customer object</returns>
        public IHttpActionResult Get(int? id)
        {
            // Attempt to fetch the object
            var o = m.CustomerGetById(id.GetValueOrDefault());

            // Continue?
            if (o == null)
            {
                return NotFound();
            }
            else
            {
                // Attention 22 - Customer, get one, create a hypermedia representation
                CustomerLinked result = new CustomerLinked
                    (Mapper.Map<CustomerWithLink>(o));

                return Ok(result);
            }
        }

        // POST: api/Customers
        /// <summary>
        /// Add new customer
        /// </summary>
        /// <param name="newItem">New customer object</param>
        /// <returns>Customer object</returns>
        public IHttpActionResult Post([FromBody]CustomerAdd newItem)
        {
            // Ensure that the URI is clean (and does not have an id parameter)
            if (Request.GetRouteData().Values["id"] != null) { return BadRequest("Invalid request URI"); }

            // Ensure that a "newItem" is in the entity body
            if (newItem == null) { return BadRequest("Must send an entity body with the request"); }

            // Ensure that we can use the incoming data
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            // Attempt to add the new object
            var addedItem = m.CustomerAdd(newItem);

            // Continue?
            if (addedItem == null) { return BadRequest("Cannot add the object"); }

            // HTTP 201 with the new object in the entity body
            // Notice how to create the URI for the Location header
            var uri = Url.Link("DefaultApi", new { id = addedItem.CustomerId });

            // Create a hypermedia representation
            // Attention 27 - This "add new" use case must use the second constructor
            CustomerLinked result = new CustomerLinked
                (Mapper.Map<CustomerWithLink>(addedItem), addedItem.CustomerId);

            return Created(uri, result);
        }

        // PUT: api/Customers/5
        /// <summary>
        /// Edit customer address
        /// </summary>
        /// <param name="id">Customer identifier</param>
        /// <param name="editedItem">Address data</param>
        /// <returns>Customer object</returns>
        public IHttpActionResult Put(int? id, [FromBody]CustomerEditAddress editedItem)
        {
            // Ensure that an "editedItem" is in the entity body
            if (editedItem == null)
            {
                return BadRequest("Must send an entity body with the request");
            }

            // Ensure that the id value in the URI matches the id value in the entity body
            if (id.GetValueOrDefault() != editedItem.CustomerId)
            {
                return BadRequest("Invalid data in the entity body");
            }

            // Ensure that we can use the incoming data
            if (ModelState.IsValid)
            {
                // Attempt to update the item
                var changedItem = m.CustomerEditAddress(editedItem);

                // Notice the ApiController convenience methods
                if (changedItem == null)
                {
                    // HTTP 400
                    return BadRequest("Cannot edit the object");
                }
                else
                {
                    // Attention 32 - Edit address result, create a hypermedia representation
                    CustomerLinked result = new CustomerLinked
                        (Mapper.Map<CustomerWithLink>(changedItem));

                    // HTTP 200 with the changed item in the entity body
                    return Ok(result);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // Attention 38 - Set support rep action, returns nothing
        /// <summary>
        /// Set/configure a customer's support rep
        /// </summary>
        /// <param name="id">Customer identifier</param>
        /// <param name="item">Customer and support rep data</param>
        [HttpPut]
        [Route("api/customers/{id}/setsupportrep")]
        public void CustomerSetSupportRep(int? id, [FromBody]CustomerSupportRep item)
        {
            // Ensure that an "editedItem" is in the entity body
            if (item == null) { return; }

            // Ensure that the id value in the URI matches the id value in the entity body
            if (id.GetValueOrDefault() != item.CustomerId) { return; }

            // Ensure that we can use the incoming data
            if (ModelState.IsValid)
            {
                // Attempt to update the item
                m.CustomerSetSupportRep(item);
            }
            else
            {
                return;
            }
        }

        /*
        // DELETE: api/Customers/5
        public void Delete(int id)
        {
        }
        */

    }
}
