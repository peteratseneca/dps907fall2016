### Week 11 code examples

**IAServerV2**

IA Server, version 2. Identity management, and authentication (it issues cookies and access tokens.) Designed to be used with other apps. Includes user account management, and claims management features.  

Features:
* Identity management, for web app and web service clients
* Authentication for web app clients - credential validation, and cookie issuing
* Authentication for web service clients - credential validation, and access token issuing
* Machine key generator, enabling a multi-app shared security environment
* Master list of "app claims" allowed to be used in user accounts, and management of that master list
* User account management

**Manager.cs**

This is a partially-complete Manager class that you can learn from. 

Features:
* Correct and complete HttpClient factory
* Partially-complete method to get an access token from an IA Server
* Sample GET method for fetching an object or collection

**manager.js**

This is a partially-complete implementation of a Manager class in JavaScript, which will help you learn the coding design pattern for interacting with a web service from pure JavaScript. 

Features:
* Partially-complete method to get an access token from an IA Server
* Sample GET method for fetching an object or collection
