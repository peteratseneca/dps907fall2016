### Week 2 code examples

**AllHttpMethods**

Based on the new "Web service project v1" template. Has an Employees controller. All relevant HTTP methods are supported. Demonstrates the best practice way to handle requests.  
* Get all
* Get one
* Add new
* Edit existing
* Delete item

In Visual Studio, configure it to locate/recognize "Attention" comment tokens, and show them on the Task List.  

**DebuggingIntro**

A web service version of the January 2016 web app that introduced you to the Visual Studio debugging experience. All the existing web app code is there. We just added another web service controller.  

In Visual Studio, configure it to locate/recognize "Attention" comment tokens, and show them on the Task List.  

Use Fiddler as the client/requestor when you're debugging.  

Features:
* Get all customers will throw a series of errors, logic, and missing AutoMapper map
* Edit existing customer will throw an error

**ExampleSolutionForAssignment1**

Example solution for Assignment 1. Implements the specifications, and best practices.  

In Visual Studio, look at the Task List, and go through the comment tokens.  

**AssociationsIntro**

Shows how to handle associated data in a web service, for a one-to-many association.  

Features:
* Employee (one) to Customer (many)
* Get one, and include associated object(s)
* Add new, for the dependent end (e.g. Customer, which requires an Employee identifier)
