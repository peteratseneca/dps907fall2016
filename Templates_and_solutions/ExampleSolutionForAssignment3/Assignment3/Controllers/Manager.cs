using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment3.Models;

namespace Assignment3.Controllers
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
        // Invoice

        public IEnumerable<InvoiceWithCustomerInfo> InvoiceGetAll()
        {
            // Attention 20 - Manager - Invoice get all - include customer info

            // For this sample solution, we take only the top 50, just to reduce the amount of data
            // This was not in the spec, so students aren't expected to do this

            // Any order/sequence is OK
            var c = ds.Invoices
                .Include("Customer.Employee")
                .OrderByDescending(i => i.InvoiceDate);

            return Mapper.Map<IEnumerable<InvoiceWithCustomerInfo>>(c.Take(50));
        }

        public InvoiceBase InvoiceGetById(int id)
        {
            // Not used in this app

            // Attempt to fetch the object
            var o = ds.Invoices.Find(id);

            // Return the result, or null if not found
            return (o == null) ? null : Mapper.Map<InvoiceBase>(o);
        }

        public InvoiceWithLinesInfo InvoiceGetByIdWithDetail(int id)
        {
            // Attention 21 - Manager - Invoice get one - include all associated info
            // Notice the property path - this will include both Customer and Employee
            // And notice the others - everything is fetched
            var o = ds.Invoices
                .Include("Customer.Employee")
                .Include("InvoiceLines.Track.Album.Artist")
                .Include("InvoiceLines.Track.MediaType")
                .SingleOrDefault(i => i.InvoiceId == id);

            // Return the result, or null if not found
            return (o == null) ? null : Mapper.Map<InvoiceWithLinesInfo>(o);
        }

        // ############################################################
        // Employee

        public IEnumerable<EmployeeBase> EmployeeGetAll()
        {
            return Mapper.Map<IEnumerable<EmployeeBase>>
                (ds.Employees.OrderBy(e => e.LastName).ThenBy(e => e.FirstName));
        }

        public IEnumerable<EmployeeWithDetails> EmployeeGetAllWithOrgInfo()
        {
            // Attention 25 - Manager - Employee - get all, with self-referencing info

            // Ugh, I hate these property names
            var c = ds.Employees.Include("Employee1").Include("Employee2");

            // Return the result
            return Mapper.Map<IEnumerable<EmployeeWithDetails>>
                (c.OrderBy(e => e.LastName).ThenBy(e => e.FirstName));
        }

        public EmployeeWithDetails EmployeeGetByIdWithDetails(int id)
        {
            // Attention 26 - Manager - Employee - get all, with self-referencing info

            // Attempt to get the matching object
            var o = ds.Employees.Include("Employee1").Include("Employee2")
                .SingleOrDefault(e => e.EmployeeId == id);

            // Return the result, or null if not found
            return (o == null) ? null : Mapper.Map<EmployeeWithDetails>(o);
        }

        public EmployeeBase EmployeeAdd(EmployeeAdd newItem)
        {
            // Attention 27 - Manager - Employee - add, we are going to allow a "reports to" (manager/supervisor) identifier to be provided
            // Therefore, we must attempt to fetch it
            // We will tolerate success or failure

            // Attempt to find the reports to (manager/supervisor) object
            var a = ds.Employees.Find(newItem.ReportsTo.GetValueOrDefault());

            // Attempt to add the object
            var addedItem = ds.Employees.Add(Mapper.Map<Employee>(newItem));
            if (a != null)
            {
                // Ugh, I hate this property name
                addedItem.Employee2 = a;
            }
            ds.SaveChanges();

            // Return the result, or null if there was an error
            return (addedItem == null) ? null : Mapper.Map<EmployeeBase>(addedItem);
        }

        public EmployeeWithDetails EmployeeSetSupervisor(EmployeeSupervisor newItem)
        {
            // Attention 30 - Manager - Employee - command to update the self-referencing to-one association

            // Attempt to fetch the object
            // Include associated data (look at the return type from this method)
            var o = ds.Employees.Include("Employee1").Include("Employee2")
                .SingleOrDefault(e => e.EmployeeId == newItem.EmployeeId);

            // Attempt to fetch the associated object
            // Include associated data (look at the return type from this method)
            Employee a = null;
            if (newItem.ReportsToId > 0)
            {
                a = ds.Employees.Include("Employee1").Include("Employee2")
                    .SingleOrDefault(e => e.EmployeeId == newItem.ReportsToId);
            }

            // Must do two tests here before continuing
            if (o == null | a == null)
            {
                // Problem - one of the items was not found, so return
                return null;
            }
            else
            {
                // Configure the new supervisor
                // MUST set both properties - the int and the Employee
                o.Employee2 = a;
                o.ReportsTo = a.EmployeeId;

                ds.SaveChanges();

                // Prepare and return the object
                return Mapper.Map<EmployeeWithDetails>(o);
            }
        }

        public EmployeeWithDetails EmployeeSetDirectReports(EmployeeDirectReports newItem)
        {
            // Attention 31 - Manager - Employee - command to update the self-referencing to-many association

            // Attempt to fetch the object

            // When editing an object with a to-many collection,
            // and you wish to edit the collection,
            // MUST fetch its associated collection
            var o = ds.Employees.Include("Employee1").Include("Employee2")
                .SingleOrDefault(e => e.EmployeeId == newItem.EmployeeId);

            if (o == null)
            {
                // Problem - object was not found, so return
                return null;
            }
            else
            {
                // Update the object with the incoming values

                // First, clear out the existing collection
                // "Employee1" is the badly-named to-many collection property
                o.Employee1.Clear();

                // Then, go through the incoming items
                // For each one, add to the fetched object's collection
                foreach (var item in newItem.EmployeeIds)
                {
                    var a = ds.Employees.Find(item);
                    if (a != null)
                    {
                        o.Employee1.Add(a);
                    }
                }
                // Save changes
                ds.SaveChanges();

                return Mapper.Map<EmployeeWithDetails>(o);
            }
        }

    }
}