### Week 9 code examples

**IAServerV2**

IA Server, version 2. Identity management, and authentication (it issues cookies and access tokens.) Designed to be used with other apps. Includes user account management, and claims management features.  

Features:
* Identity management, for web app and web service clients
* Authentication for web app clients - credential validation, and cookie issuing
* Authentication for web service clients - credential validation, and access token issuing
* Machine key generator, enabling a multi-app shared security environment
* Master list of "app claims" allowed to be used in user accounts, and management of that master list
* User account management

**SecuredCustomer**

Similar functionality to the long-ago "AssociationsIntro" code example. Simply delivers customer data from the Chinook sample database.  

This app has been configured with the access token validation part of the authentication process. Like other apps, it already includes the ability to perform authorization. 

Features:
* Values controller with a protected "get one" method
* Protected Customers controller

**IAServerV1**

IA Server, version 1. Old version, without user account and claims management. Do not use.
