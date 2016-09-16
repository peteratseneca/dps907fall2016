using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using AssociationsIntro.Models;

namespace AssociationsIntro.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        public Manager()
        {
            // If necessary, add constructor code here

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()

        // ############################################################
        // Employee

        // Attention 15 - Employee get all and get one methods - very familiar

        public IEnumerable<EmployeeBase> EmployeeGetAll()
        {
            // c is "collection" (more than one)
            // o is "object" (one only)

            var c = ds.Employees.OrderBy(e => e.LastName).ThenBy(e => e.FirstName);

            return Mapper.Map<IEnumerable<EmployeeBase>>(c);
        }

        public EmployeeBase EmployeeGetById(int id)
        {
            // Attempt to fetch the object
            var o = ds.Employees.Find(id);

            return (o == null) ? null : Mapper.Map<EmployeeBase>(o);
        }

        // Attention 16 - Employee get one with associated objects, new
        
        public EmployeeWithCustomers EmployeeGetByIdWithCustomers(int id)
        {
            // Attempt to fetch the object
            
            // Attention 17 - Notice that you must use SingleOrDefault() - cannot use Find()
            // See this - https://petermcintyre.com/bti420/notes/jan29-feb01/ 

            var o = ds.Employees.Include("Customers")
                .SingleOrDefault(e => e.EmployeeId == id);

            return (o == null) ? null : Mapper.Map<EmployeeWithCustomers>(o);
        }

        // Attention 18 - Employee add one

        public EmployeeBase EmployeeAdd(EmployeeAdd newItem)
        {
            // Attempt to add the object
            var addedItem = ds.Employees.Add(Mapper.Map<Employee>(newItem));
            ds.SaveChanges();

            // Return the result, or null if there was an error
            return (addedItem == null) ? null : Mapper.Map<EmployeeBase>(addedItem);
        }

        // Attention 19 - Employee edit existing

        public EmployeeBase EmployeeEditContactInfo(EmployeeEditContactInfo editedItem)
        {
            // Ensure that we can continue
            if (editedItem == null) { return null; }

            // Attempt to fetch the object
            var storedItem = ds.Employees.Find(editedItem.EmployeeId);

            if (storedItem == null)
            {
                return null;
            }
            else
            {
                // Fetch the object from the data store - ds.Entry(storedItem)
                // Get its current values collection - .CurrentValues
                // Set those to the edited values - .SetValues(editedItem)
                ds.Entry(storedItem).CurrentValues.SetValues(editedItem);
                // The SetValues() method ignores missing properties and navigation properties
                ds.SaveChanges();

                return Mapper.Map<EmployeeBase>(storedItem);
            }
        }

        // ############################################################
        // Customer

        // Attention 25 - Customer get all and get one methods - very familiar

        public IEnumerable<CustomerBase> CustomerGetAll()
        {
            var c = ds.Customers.OrderBy(cu => cu.Company).ThenBy(cu => cu.LastName).ThenBy(cu => cu.FirstName);

            return Mapper.Map<IEnumerable<CustomerBase>>(c);
        }

        public CustomerBase CustomerGetById(int id)
        {
            // Attempt to fetch the object
            var o = ds.Customers.Find(id);

            return (o == null) ? null : Mapper.Map<CustomerBase>(o);
        }

        // Attention 26 - Customer get one with associated object, new

        public CustomerWithEmployee CustomerGetByIdWithEmployee(int id)
        {
            // Attempt to fetch the object
            var o = ds.Customers.Include("Employee")
                .SingleOrDefault(c=>c.CustomerId == id);

            return (o == null) ? null : Mapper.Map<CustomerWithEmployee>(o);
        }

        // Attention 27 - Customer add one

        public CustomerBase CustomerAdd(CustomerAdd newItem)
        {
            // Attention 28 - To add a customer, we MUST have an Employee identifier
            // We must validate that employee first by attempting to fetch it
            // If successful, then we can continue

            // Attempt to find the associated object
            var a = ds.Employees.Find(newItem.SupportRepId);

            if (a == null)
            {
                return null;
            }
            else
            {
                // Attempt to add the object
                var addedItem = ds.Customers.Add(Mapper.Map<Customer>(newItem));
                // Set the associated item property
                addedItem.Employee = a;
                ds.SaveChanges();

                // Return the result, or null if there was an error
                return (addedItem == null) ? null : Mapper.Map<CustomerBase>(addedItem);
            }
        }

        public void CustomerDelete(int id)
        {
            // Attempt to fetch the existing item
            var storedItem = ds.Customers.Find(id);

            // Interim coding strategy...

            if (storedItem == null)
            {
                // Throw an exception, and you will learn how soon
            }
            else
            {
                try
                {
                    // If this fails, throw an exception (as above)
                    // This implementation just prevents an error from bubbling up
                    ds.Customers.Remove(storedItem);
                    ds.SaveChanges();
                }
                catch (Exception) { }
            }
        }
    }
}