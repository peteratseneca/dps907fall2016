### Week 7 code examples

**ProjectWithSecurity**

Visual Studio web service project, with security.  

Study the "ATTENTION" comment tokens as you go through the class notes.  

Open the EntityBodyData.txt source code file for examples of data package format that you can send to the web service. Please note that the user accounts in that file have already been created (so if you attempt to create them again, an error will be returned).  

Features:
* Simple non-enhanced web service project with security
* An "AuthInfo" controller that will return access token data (if you are authenticated)

**CustomAuthorizeAttribute.cs**

This is a custom filter, to authorize a custom claim.  

Add this to any project, in the Controllers folder, and edit its namespace. Then you can use it any controller.  

**SimpleClaims**

Enhances the ProjectWithSecurity code example (above).  

Adds claims processing. When you register a new user account, you must provide a given name, a surname, and one or more role claims. See the EntityBodyData.txt source code file for examples.  

Features:
* Test controller that will enable you to test various user account scenarios
* AuthInfo controller that will decrypt/decode the access token, and display its contents
