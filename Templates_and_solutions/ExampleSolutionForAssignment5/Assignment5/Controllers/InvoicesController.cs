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
    public class InvoicesController : ApiController
    {
        // Reference
        private Manager m = new Manager();

        // GET: api/Invoices
        /// <summary>
        /// All invoices
        /// </summary>
        /// <returns>Collection of invoice objects</returns>
        public IHttpActionResult Get()
        {
            // Fetch collection
            var c = m.InvoiceGetAll();

            // Attention 45 - Invoices, get all, create a hypermedia representation
            InvoicesLinked result = new InvoicesLinked
                (Mapper.Map<IEnumerable<InvoiceWithLink>>(c));

            return Ok(result);
        }

        // GET: api/Invoices/5
        /// <summary>
        /// One customer by its identifier
        /// </summary>
        /// <param name="id">Customer identifier</param>
        /// <returns>(not implemented in this app)</returns>
        public string Get(int id)
        {
            return "not implemented";
        }

        /*
        // POST: api/Invoices
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Invoices/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Invoices/5
        public void Delete(int id)
        {
        }
        */
    }
}
