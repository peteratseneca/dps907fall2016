using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;

namespace Assignment3.Controllers
{
    // Attention 02 - Invoice resource models

    public class InvoiceBase
    {
        public InvoiceBase()
        {
            InvoiceDate = DateTime.Now;
        }

        // Attention 03 - Remember, [Key] must be used
        // In this app, Invoice is read-only (delivered to the requestor), so it does not need the Required data annotation (does it need StringLength?)

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
    }

    public class InvoiceWithCustomerInfo : InvoiceBase
    {
        // Attention 04 - Invoice with composite properties from Customer and Employee

        // Customer info
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerState { get; set; }
        public string CustomerEmployeeFirstName { get; set; }
        public string CustomerEmployeeLastName { get; set; }
    }

    public class InvoiceWithLinesInfo : InvoiceWithCustomerInfo
    {
        public InvoiceWithLinesInfo()
        {
            InvoiceLines = new List<InvoiceLineWithDetail>();
        }

        // Invoice line items
        public IEnumerable<InvoiceLineWithDetail> InvoiceLines { get; set; }
    }
}
