using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebAndApiControllers.Controllers
{
    public class EmployeeAdd
    {
        [Required, StringLength(20)]
        [Display(Name ="Last name")]
        public string LastName { get; set; }

        [Required, StringLength(20)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string Title { get; set; }

        [Display(Name = "Birth date")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Date hired")]
        public DateTime? HireDate { get; set; }

        [Required, StringLength(70)]
        public string Address { get; set; }

        [Required, StringLength(40)]
        public string City { get; set; }

        [Required, StringLength(40)]
        public string State { get; set; }

        [Required, StringLength(40)]
        public string Country { get; set; }

        [Required, StringLength(10)]
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }

        [Required, StringLength(24)]
        public string Phone { get; set; }

        [Required, StringLength(24)]
        public string Fax { get; set; }

        [Required, StringLength(60)]
        public string Email { get; set; }
    }

    public class EmployeeBase : EmployeeAdd
    {
        [Key]
        public int EmployeeId { get; set; }
    }

    // ############################################################
    // Linked classes

    public class EmployeeWithLink : EmployeeBase
    {
        public Link Link { get; set; }

        // Duplicated identity field
        [JsonIgnore]
        public int Id { get; set; }
    }

    public class EmployeeLinked : LinkedItem<EmployeeWithLink>
    {
        // Constructor - call the base class constructor
        public EmployeeLinked(EmployeeWithLink item) : base(item) { }
    }

    public class EmployeesLinked : LinkedCollection<EmployeeWithLink>
    {
        // Constructor - call the base class constructor
        public EmployeesLinked(IEnumerable<EmployeeWithLink> collection) : base(collection) { }
    }

}
