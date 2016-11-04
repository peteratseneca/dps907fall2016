using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using Microsoft.AspNet.Identity.EntityFramework;

namespace Assignment8.Models
{
    // Reference...
    // https://msdn.microsoft.com/en-us/library/bb358407(v=vs.110).aspx

    public class ClaimComparer : IEqualityComparer<IdentityUserClaim>
    {
        public bool Equals(IdentityUserClaim x, IdentityUserClaim y)
        {
            // Null?
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null)) { return false; }

            // Same?
            if (Object.ReferenceEquals(x, y)) { return true; }

            // Equal if both the ClaimType and ClaimValue match
            return x.ClaimType == y.ClaimType && x.ClaimValue==y.ClaimValue;
        }

        public int GetHashCode(IdentityUserClaim claim)
        {
            // Null?
            if (Object.ReferenceEquals(claim, null)) { return 0; }

            // Get property hash codes
            int hashType = claim.ClaimType == null ? 0 : claim.ClaimType.GetHashCode();
            int hashValue = claim.ClaimValue == null ? 0 : claim.ClaimValue.GetHashCode();

            // Calculate hash for the claim
            return hashType ^ hashValue;
        }
    }
}
