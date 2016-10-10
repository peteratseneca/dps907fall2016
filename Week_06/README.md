### Week 6 code examples

**Web service project v2**

Located in the "Templates and solutions" folder.  

Adds several useful features to the "version 1" project template, which were covered in the past several weeks:  
* Media type formatter for byte-oriented content
* HTTP OPTIONS handler
* Hypermedia-aware link relations generator
* Root controller to handle requests to /api
* Minor improvement for exceptions/errors

Copy the zip file to the following folder. Do NOT un-zip the file - leave it as-is:  
%userprofile%\Documents\Visual Studio 2015\Templates\ProjectTemplates\Visual C#\Web  

**BetterErrorHandling**

Implements a slightly better error-handling mechanism. Will display a friendlier error message to all, and stack trace details to the programmer.  

**HypermediaObjectGraph**

Shows how to deliver an object that has link relations. And, that object has a nested collection of objects, each of which get a link relation generated.  

**WebAndApiControllers**

Shows how to support both web app controllers (MVC) and web service controllers (Web API) in the same project, with nice URIs.  

**HTML5Client**

Simple HTML5 app that uses the "HypermediaObjectGraph" web service (above). Shows how to call out to a web service from JavaScript.  
