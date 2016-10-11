using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;

namespace LinkRelationsIntro.Controllers
{
    // Resource model classes
    // We need three of them:
    // 1. Add
    // 2. Base
    // 3. Edit

    // Remember - tip/hint - to ease the process of writing resource model classes,
    // copy the design model class, then make changes

    public class EmployeeAdd
    {
        public EmployeeAdd()
        {
            BirthDate = DateTime.Now.AddYears(-30);
            HireDate = DateTime.Now.AddYears(-1);
        }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string Title { get; set; }

        // We will leave this property as-is, so entering a value will be optional
        public int? ReportsTo { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? HireDate { get; set; }

        // Added "Required" to the postal address related properties
        // The design model class doesn't have this annotation
        // However, adding it here improves the quality of the incoming data

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

        [StringLength(24)]
        public string Phone { get; set; }

        [StringLength(24)]
        public string Fax { get; set; }

        [StringLength(60)]
        public string Email { get; set; }
    }

    public class EmployeeAddTemplate
    {
        public string LastName { get { return "Last name, string, required, up to 20 characters"; } }
        public string FirstName { get { return "First name, string, required, up to 20 characters"; } }
        public string Title { get { return "Job title, string, optional, up to 30 characters"; } }
        public string ReportsTo { get { return "Identifier of this employee's supervisor, integer, optional, will be validated"; } }
        public string BirthDate { get { return "Birth date, date type, optional, if empty, will be set to an initial value"; } }
        public string HireDate { get { return "Date hired, date type, optional, if empty, will be set to an initial value"; } }
    }

    // Inherits from EmployeeAdd
    public class EmployeeBase : EmployeeAdd
    {
        [Key]
        public int EmployeeId { get; set; }
    }

    // Allow editing of the contact info ONLY
    public class EmployeeEditContactInfo
    {
        [Key]
        public int EmployeeId { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        [StringLength(24)]
        public string Fax { get; set; }

        [StringLength(60)]
        public string Email { get; set; }
    }

    // ############################################################
    // Attention 05 - Classes that have link relations

    public class EmployeeWithLink : EmployeeBase
    {
        public Link Link { get; set; }

        // Attention 06 - The link relation factories DEPEND on an identifier property name "Id"
        // So, all the Chinook entities need this additional property
        // When the map task happens, AutoMapper will copy the <whatever>Id value to the new Id property
        public int Id { get; set; }
    }

    public class EmployeeLinked : LinkedItem<EmployeeWithLink>
    {
        // Constructor - call the base class constructor

        // Attention 07 - Linked item constructor - all use cases except "add new"
        public EmployeeLinked(EmployeeWithLink item) : base(item) { }

        // Attention 08 - Linked item constructor - add new" use case
        public EmployeeLinked(EmployeeWithLink item, int id) : base(item, id) { }
    }

    public class EmployeesLinked : LinkedCollection<EmployeeWithLink>
    {
        // Constructor - call the base class constructor
        public EmployeesLinked(IEnumerable<EmployeeWithLink> collection) : base(collection)
        {
            Template = new EmployeeAddTemplate();
        }

        // Add new template
        public EmployeeAddTemplate Template { get; set; }
    }
}
