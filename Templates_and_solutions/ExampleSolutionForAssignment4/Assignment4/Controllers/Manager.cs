using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment4.Models;

namespace Assignment4.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

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
        // Instrument

        // Attention 15 - Get all instruments, no media, but WITH media INFO
        public IEnumerable<InstrumentWithMediaInfo> InstrumentGetAll()
        {
            var c = ds.Instruments.OrderBy(i => i.InstrumentName);

            return Mapper.Map<IEnumerable<InstrumentWithMediaInfo>>(c);
        }

        // Attention 16 - Get one instrument, WITH media
        public InstrumentWithMedia InstrumentGetById(int id)
        {
            var o = ds.Instruments.Find(id);

            return (o == null) ? null : Mapper.Map<InstrumentWithMedia>(o);
        }

        // Attention 17 - Add new instrument, standard logic
        public InstrumentBase InstrumentAdd(InstrumentAdd newItem)
        {
            // Attempt to add the object
            var addedItem = ds.Instruments.Add(Mapper.Map<Instrument>(newItem));
            ds.SaveChanges();

            // Return the result, or null if there was an error
            return (addedItem == null) ? null : Mapper.Map<InstrumentBase>(addedItem);
        }

        // Attention 18 - Set/configure a photo for an existing instrument
        public bool InstrumentSetPhoto(int id, string contentType, byte[] media)
        {
            // Ensure that we can continue
            if (string.IsNullOrEmpty(contentType) | media == null) { return false; }

            // Attempt to find the matching object
            var storedItem = ds.Instruments.Find(id);

            // Ensure that we can continue
            if (storedItem == null) { return false; }

            // Save the photo
            storedItem.PhotoContentType = contentType;
            storedItem.PhotoMedia = media;

            // Attempt to save changes
            return (ds.SaveChanges() > 0) ? true : false;
        }

        // Attention 19 - (BSD) Set/configure a sound clip for an existing instrument
        public bool InstrumentSetSoundClip(int id, string contentType, byte[] media)
        {
            // Ensure that we can continue
            if (string.IsNullOrEmpty(contentType) | media == null) { return false; }

            // Attempt to find the matching object
            var storedItem = ds.Instruments.Find(id);

            // Ensure that we can continue
            if (storedItem == null) { return false; }

            // Save the photo
            storedItem.SoundClipContentType = contentType;
            storedItem.SoundClipMedia = media;

            // Attempt to save changes
            return (ds.SaveChanges() > 0) ? true : false;
        }

        // Attention 20 - Delete instrument, standard logic
        public void InstrumentDelete(int id)
        {
            // Attempt to fetch the existing item
            var storedItem = ds.Instruments.Find(id);

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
                    ds.Instruments.Remove(storedItem);
                    ds.SaveChanges();
                }
                catch (Exception) { }
            }
        }
    }
}
