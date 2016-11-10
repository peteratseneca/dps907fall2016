using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;

namespace Assignment3.Controllers
{
    // Attention 10 - Employee view model classes, plain, and with reports-to and direct-reports info

    public class EmployeeAdd
    {
        public EmployeeAdd()
        {
            BirthDate = DateTime.Now.AddYears(-30);
            HireDate = DateTime.Now.AddYears(-5);
            ReportsTo = 0;
        }

        // We are including the relevant data annotations from the design model class
        // However, to improve data quality, we are adding "Required" to some properties

        [Required, StringLength(20)]
        public string LastName { get; set; }

        [Required, StringLength(20)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string Title { get; set; }

        // Attention 11 - Allow a "new employee" object to include, or not, the "ReportsTo" value

        // Self-referencing to-one property
        // If YOU were writing this class yourself, you should use a better name
        // The nullable int should be named "ReportsToId"
        public int? ReportsTo { get; set; }

        public DateTime? BirthDate { get; set; }

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
        public string PostalCode { get; set; }

        [Required, StringLength(24)]
        public string Phone { get; set; }

        [Required, StringLength(24)]
        public string Fax { get; set; }

        [Required, StringLength(60)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    public class EmployeeBase : EmployeeAdd
    {
        [Key]
        public int EmployeeId { get; set; }
    }

    public class EmployeeWithDetails : EmployeeBase
    {
        public EmployeeWithDetails()
        {
            Employee1 = new List<EmployeeBase>();
        }

        // Attention 12 - Employee with self-referencing object(s) details

        // Self-referencing to-one property
        // If YOU were writing this class yourself, you should use a better name
        // The EmployeeBase should be named "ReportsTo"
        public virtual EmployeeBase Employee2 { get; set; }

        // Self-referencing to-many property
        // If YOU were writing this class yourself, you should use a better name
        // The collection should be named "DirectReports" (plural)
        public IEnumerable<EmployeeBase> Employee1 { get; set; }
    }

    public class EmployeeSupervisor
    {
        // Attention 13 - For an employee, set the supervisor identifier

        [Key]
        public int EmployeeId { get; set; }

        // Manager/supervisor identifier
        public int ReportsToId { get; set; }
    }

    public class EmployeeDirectReports
    {
        // Attention 14 - For an employee, set the identifiers of the direct-reports
        public EmployeeDirectReports()
        {
            EmployeeIds = new List<int>();
        }

        [Key]
        public int EmployeeId { get; set; }

        // Collection of identifiers for the direct-reports (other employees who report to this employee)
        public IEnumerable<int> EmployeeIds { get; set; }
    }

}
