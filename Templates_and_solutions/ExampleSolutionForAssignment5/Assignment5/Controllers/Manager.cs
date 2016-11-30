using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment5.Models;

namespace Assignment5.Controllers
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




        // Attention 17 - Customer get all, returns extra data
        public IEnumerable<CustomerWithData> CustomerGetAll()
        {
            var c = ds.Customers
                .Include("Employee")
                .Take(25)
                .OrderBy(cu => cu.LastName)
                .ThenBy(cu => cu.FirstName);

            return Mapper.Map<IEnumerable<CustomerWithData>>(c);
        }

        // Attention 21 - Customer get one, returns extra data
        public CustomerWithData CustomerGetById(int id)
        {
            // Attempt to fetch the object
            var o = ds.Customers.Include("Employee")
                .SingleOrDefault(c => c.CustomerId == id);

            // Return the result, or null if not found
            return (o == null) ? null : Mapper.Map<CustomerWithData>(o);
        }

        // Attention 26 - Customer add new, returns extra data
        // Must do a bit more than a normal "add new", because of the association
        // First, check whether an identifier (for Employee) was provided 
        // It may or may not be there - it's optional
        // If present, validate, and if valid, must do one extra fetch before return
        // so that the associated data comes back; if invalid, set it to null
        // Please be aware that there could be alternative coding plans - 
        // the following plan is not authoritative, and others could work
        public CustomerWithData CustomerAdd(CustomerAdd newItem)
        {
            // Employee object (support rep)
            Employee associatedItem = null;

            // Is there a SupportRepId value?
            if (newItem.SupportRepId != null)
            {
                // Attempt to get the associated item
                associatedItem = ds.Employees.Find(newItem.SupportRepId.GetValueOrDefault());

                // If the attempt fails, must remove the invalid support rep identifier
                if (associatedItem == null)
                {
                    newItem.SupportRepId = null;
                }
            }

            // Attempt to add the object
            var addedItem = ds.Customers.Add(Mapper.Map<Customer>(newItem));
            ds.SaveChanges();

            if (addedItem == null)
            {
                return null;
            }
            else
            {
                // If associated item is null, just return the added item
                // Else, do the extra fetch

                if (associatedItem == null)
                {
                    return Mapper.Map<CustomerWithData>(addedItem);
                }
                else
                {
                    // Set the association
                    addedItem.Employee = associatedItem;
                    ds.SaveChanges();

                    // Fetch the just-added object, with extra data
                    var o = ds.Customers.Include("Employee")
                        .SingleOrDefault(c => c.CustomerId == addedItem.CustomerId);

                    // Return the result
                    return Mapper.Map<CustomerWithData>(o);
                }
            }
        }

        // Attention 31 - Customer edit address, returns extra data
        // Must do a bit more than a normal "edit existing", because of the association
        // One extra fetch before return so that the associated data comes back
        // Please be aware that there could be alternative coding plans - 
        // the following plan is not authoritative, and others could work
        public CustomerWithData CustomerEditAddress(CustomerEditAddress editedItem)
        {
            // Ensure that we can continue
            if (editedItem == null) { return null; }

            // Attempt to fetch the object
            var storedItem = ds.Customers.Find(editedItem.CustomerId);

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

                // Extra fetch
                var o = ds.Customers.Include("Employee")
                    .SingleOrDefault(c => c.CustomerId == storedItem.CustomerId);

                return Mapper.Map<CustomerWithData>(o);
            }
        }

        // Attention 37 - Set support rep command
        public void CustomerSetSupportRep(CustomerSupportRep item)
        {
            // Get a reference to the customer
            var customer = ds.Customers.Find(item.CustomerId);
            if (customer == null) { return; }

            // Get a reference to the employee
            var employee = ds.Employees.Find(item.SupportRepId);
            if (employee == null) { return; }

            // Make the changes, save, and exit
            customer.Employee = employee;
            customer.SupportRepId = employee.EmployeeId;
            ds.SaveChanges();
        }

        // ############################################################
        // Invoice

        public IEnumerable<InvoiceWithData> InvoiceGetAll()
        {
            // Attention 44 - Invoice get all, with extra data
            var c = ds.Invoices
                .Include("Customer")
                .Take(25)
                .OrderByDescending(i => i.InvoiceDate)
                .ThenBy(i => i.Customer.Company);

            return Mapper.Map<IEnumerable<InvoiceWithData>>(c);
        }

    }
}