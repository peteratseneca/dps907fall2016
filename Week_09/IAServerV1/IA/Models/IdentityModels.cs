using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
// added...
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace IA.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        // This is called from the /token endpoint (and a few other places)
        // Its job is to create and return an identity 
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        // DbSet properties
        public DbSet<CustomClaim> CustomClaims { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    // ############################################################
    // Custom claim

    // This class defines a custom claim
    // The type and value properties must be strings
    // Can be used as a lookup list when defining and configuring claims for a new user account
    // The ASP.NET Identity system maintains claims for each user (in the AspNetUserClaims database table)
    // Those are active - this defines a lookup list of claims that are valid at a point in time

    // This class can be used to define the allowable "role" claims too

    public class CustomClaim
    {
        public CustomClaim()
        {
            DateCreated = DateTime.Now;
            DateUpdated = DateCreated;

            // Help configure a property value with the official claim URI
            if (ClaimType.ToLower() == "role")
            {
                ClaimTypeUri = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
            }
        }

        /// <summary>
        /// Custom claim identifier (for storage only)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Date and time that the custom claim was created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Date and time that the custom claim was updated
        /// </summary>
        public DateTime DateUpdated { get; set; }

        /// <summary>
        /// Date and time that the custom claim was retired and removed from use/service
        /// </summary>
        public DateTime? DateRetired { get; set; }

        /// <summary>
        /// Custom claim description
        /// </summary>
        [Required, StringLength(200)]
        public string Description { get; set; } = "(none)";

        /// <summary>
        /// Custom claim type
        /// </summary>
        [Required, StringLength(100)]
        public string ClaimType { get; set; } = "(none)";

        /// <summary>
        /// Custom claim type, as a URI
        /// </summary>
        [Required, StringLength(200)]
        public string ClaimTypeUri { get; set; } = "(none)";

        /// <summary>
        /// Custom claim value
        /// </summary>
        [Required, StringLength(100)]
        public string ClaimValue { get; set; } = "(none)";

        /// <summary>
        /// Is active? Or, has this claim been retired (removed from service)?
        /// </summary>
        public bool IsActive
        {
            get
            {
                return (DateTime.Now > DateRetired.GetValueOrDefault()) ? true : false;
            }
        }

        /// <summary>
        /// Is a role claim?
        /// </summary>
        public bool IsRoleClaim
        {
            get
            {
                return (ClaimType.ToLower() == "role") ? true : false;
            }
        }

    }
}