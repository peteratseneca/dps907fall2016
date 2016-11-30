using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;

namespace Assignment5.Controllers
{
    // Attention 41 - Invoice resource model, could do Add, Base, and WithData
    // Or, just WithData alone, to satisfy the one/single "get all" use case
    public class InvoiceWithData
    {
        [Key]
        public int InvoiceId { get; set; }

        public int CustomerId { get; set; }

        public DateTime InvoiceDate { get; set; }

        [StringLength(70)]
        public string BillingAddress { get; set; }

        [StringLength(40)]
        public string BillingCity { get; set; }

        [StringLength(40)]
        public string BillingState { get; set; }

        [StringLength(40)]
        public string BillingCountry { get; set; }

        [StringLength(10)]
        public string BillingPostalCode { get; set; }

        public decimal Total { get; set; }

        // Attention 42 - Composed properties, extra data
        public string CustomerLastName { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerCompany { get; set; }
    }

    // ############################################################
    // Attention 46 - For Invoice, the link classes

    public class InvoiceWithLink : InvoiceWithData
    {
        public Link Link { get; set; }

        // The link relation factories DEPEND on an identifier property name "Id"
        // So, all the Chinook entities need this additional property
        // When the map task happens, AutoMapper will copy the <whatever>Id value to the new Id property
        public int Id { get; set; }
    }

    public class InvoiceLinked : LinkedItem<InvoiceWithLink>
    {
        // Constructor - call the base class constructor

        // Attention 07 - Linked item constructor - all use cases except "add new"
        public InvoiceLinked(InvoiceWithLink item) : base(item) { }

        // Attention 08 - Linked item constructor - add new" use case
        public InvoiceLinked(InvoiceWithLink item, int id) : base(item, id) { }
    }

    public class InvoicesLinked : LinkedCollection<InvoiceWithLink>
    {
        // Constructor - call the base class constructor
        public InvoicesLinked(IEnumerable<InvoiceWithLink> collection) : base(collection) { }
    }

}
