﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ManageClaims.Models
{
    public static class IdentityInitialize
    {
        // Load user accounts
        public static async void LoadUserAccounts()
        {
            // Get a reference to the objects we need
            var ds = new ApplicationDbContext();
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(ds));

            // Add the user(s) that the app needs when loaded for the first time
            // Change any of the data below to better match your app's needs
            if (userManager.Users.Count() == 0)
            {
                var user = new ApplicationUser { UserName = "admin@example.com", Email = "admin@example.com" };
                var result = await userManager.CreateAsync(user, "Password123!");
                if (result.Succeeded)
                {
                    // Add claims
                    await userManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Email, "admin@example.com"));
                    await userManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Role, "UserAccountAdministrator"));
                    await userManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.GivenName, "User Account"));
                    await userManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Surname, "Administrator"));
                }
            }
        }

        // Load app claims
        // (write your code here)
        // (get a reference to the manager object, and then call its methods)


    }
}
