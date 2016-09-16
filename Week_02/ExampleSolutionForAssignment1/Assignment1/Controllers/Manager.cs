using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using Assignment1.Models;
using AutoMapper;

namespace Assignment1.Controllers
{
    // Attention 11 - Data service operations for the app

    public class Manager
    {
        // Attention 12 - Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // Attention 13 - Get all - it's a good/safe idea to fetch the collection before attempting to return it
        public IEnumerable<SmartphoneBase> SmartphoneGetAll()
        {
            // Fetch
            var c = ds.Smartphones.OrderBy(s => s.Manufacturer).ThenBy(s => s.Model);

            // Transform and return
            return Mapper.Map<IEnumerable<SmartphoneBase>>(c);
        }

        // Attention 14 - Get one - it's a good/save idea to attempt to fetch the object before returning it
        public SmartphoneBase SmartphoneGetById(int id)
        {
            // Fetch
            var o = ds.Smartphones.Find(id);

            // Transform and return
            return (o == null) ? null : Mapper.Map<SmartphoneBase>(o);
        }

        // Attention 15 - Add new
        public SmartphoneBase SmartphoneAdd(SmartphoneAdd newItem)
        {
            // Ensure that we can continue
            if (newItem == null)
            {
                return null;
            }
            else
            {
                // Add the new object
                var addedItem = Mapper.Map<Smartphone>(newItem);

                ds.Smartphones.Add(addedItem);
                ds.SaveChanges();

                // Transform and return
                return Mapper.Map<SmartphoneBase>(addedItem);
            }

            // Alternate code - either style is OK...
            /*
            var addedItem = ds.Smartphones.Add(Mapper.Map<Smartphone>(newItem));
            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<SmartphoneBase>(addedItem);
            */
        }
    }
}
