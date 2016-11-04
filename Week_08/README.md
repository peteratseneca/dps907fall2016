### Week 8 code examples

**ManageClaims**

Enables a user account administrator to manage the claims that are allowed in the app.

Features:
* Custom AppClaim class, to define the claim
* Controller support for doing some basic tasks (fetch, add)

**ManageUserAccounts**

Enables a user account administrator to manage user accounts in the app.

Features:
* Works with the ASP.NET Identity UserManager API
* Designed to work in a way similar to all our other work, with manager methods and resource model classes

**ClaimComparer.cs**

Use this with Assignment 8. Add to the Models folder.  

Then, you can use this with the Union method to compare a claim (in a user account) by its ClaimType and ClaimValue.  
