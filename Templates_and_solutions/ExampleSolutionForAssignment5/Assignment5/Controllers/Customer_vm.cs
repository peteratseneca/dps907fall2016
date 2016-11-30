using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;

namespace Assignment5.Controllers
{
    // Attention 11 - Customer resource models

    public class CustomerAdd
    {
        [Required, StringLength(40)]
        public string FirstName { get; set; }

        [Required, StringLength(20)]
        public string LastName { get; set; }

        [Required, StringLength(80)]
        public string Company { get; set; }

        [Required, StringLength(70)]
        public string Address { get; set; }

        [Required, StringLength(40)]
        public string City { get; set; }

        [Required, StringLength(40)]
        public string State { get; set; }

        [Required, StringLength(40)]
        public string Country { get; set; }

        [Required, StringLength(10)]
        public string PostalCode { get; set; }

        [Required, StringLength(24)]
        public string Phone { get; set; }

        [Required, StringLength(24)]
        public string Fax { get; set; }

        [Required, StringLength(60)]
        public string Email { get; set; }

        public int? SupportRepId { get; set; }
    }

    // Attention 15 - Customer "add new template"
    public class CustomerAddTemplate
    {
        public string FirstName { get { return "First name, string, required, up to 40 characters"; } }
        public string LastName { get { return "Last name, string, required, up to 20 characters"; } }
        public string Company { get { return "Company name, string, required, up to 80 characters"; } }
        public string Address { get { return "Street address, string, required, up to 70 characters"; } }
        public string City { get { return "City, string, required, up to 40 characters"; } }
        public string State { get { return "State / province / region, string, required, up to 40 characters"; } }
        public string Country { get { return "Country, string, required, up to 40 characters"; } }
        public string PostalCode { get { return "Postal code, string, required, up to 10 characters"; } }
        public string Phone { get { return "Phone number, string, required, up to 24 characters"; } }
        public string Fax { get { return "Fax number, string, required, up to 24 characters"; } }
        public string Email { get { return "Email address, string, required, up to 60 characters"; } }
        public string SupportRepId { get { return "Support rep (employee) identifier, integer, optional, will be validated"; } }
    }

    public class CustomerBase : CustomerAdd
    {
        [Key]
        public int CustomerId { get; set; }
    }

    public class CustomerWithData : CustomerBase
    {
        // Attention 12 - From Employee entity, composed property names
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
    }

    public class CustomerEditAddress
    {
        [Key]
        public int CustomerId { get; set; }

        [Required, StringLength(70)]
        public string Address { get; set; }

        [Required, StringLength(40)]
        public string City { get; set; }

        [Required, StringLength(40)]
        public string State { get; set; }

        [Required, StringLength(40)]
        public string Country { get; set; }

        [Required, StringLength(10)]
        public string PostalCode { get; set; }
    }

    // Attention 36 - Resource model class for the "set support rep" command
    public class CustomerSupportRep
    {
        public int CustomerId { get; set; }
        public int SupportRepId { get; set; }
    }

    // ############################################################
    // Attention 13 - For Customer, the Link classes

    public class CustomerWithLink : CustomerWithData
    {
        public Link Link { get; set; }

        // Attention 14 - The link relation factories DEPEND on an identifier property name "Id"
        // So, all the Chinook entities need this additional property
        // When the map task happens, AutoMapper will copy the <whatever>Id value to the new Id property
        public int Id { get; set; }
    }

    public class CustomerLinked : LinkedItem<CustomerWithLink>
    {
        // Constructor - call the base class constructor

        // Attention 07 - Linked item constructor - all use cases except "add new"
        public CustomerLinked(CustomerWithLink item) : base(item) { }

        // Attention 08 - Linked item constructor - add new" use case
        public CustomerLinked(CustomerWithLink item, int id) : base(item, id) { }
    }

    public class CustomersLinked : LinkedCollection<CustomerWithLink>
    {
        // Constructor - call the base class constructor
        public CustomersLinked(IEnumerable<CustomerWithLink> collection) : base(collection)
        {
            Template = new CustomerAddTemplate();
        }

        // Add new template
        public CustomerAddTemplate Template { get; set; }
    }

}
