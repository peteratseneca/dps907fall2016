using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment2.Models;

namespace Assignment2.Controllers
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
        // Artist

        // Attention 15 - Artist get all and get one methods - very familiar

        public IEnumerable<ArtistBase> ArtistGetAll()
        {
            var c = ds.Artists.OrderBy(a => a.Name);

            return Mapper.Map<IEnumerable<ArtistBase>>(c);
        }

        public ArtistBase ArtistGetById(int id)
        {
            // Attempt to fetch the object
            var o = ds.Artists.Find(id);

            return (o == null) ? null : Mapper.Map<ArtistBase>(o);
        }

        // Attention 16 - Artist get one with associated objects, new

        public ArtistWithAlbums ArtistGetByIdWithAlbums(int id)
        {
            // Attempt to fetch the object
            var o = ds.Artists.Include("Albums")
                .SingleOrDefault(a => a.ArtistId == id);

            return (o == null) ? null : Mapper.Map<ArtistWithAlbums>(o);
        }

        // Attention 17 - Artist add one

        public ArtistBase EmployeeAdd(ArtistAdd newItem)
        {
            // Attempt to add the object
            var addedItem = ds.Artists.Add(Mapper.Map<Artist>(newItem));
            ds.SaveChanges();

            // Return the result, or null if there was an error
            return (addedItem == null) ? null : Mapper.Map<ArtistBase>(addedItem);
        }
        
        // ############################################################
        // Album

        // Attention 20 - Album get all and get one methods - very familiar

        public IEnumerable<AlbumBase> AlbumGetAll()
        {
            var c = ds.Albums.OrderBy(a => a.Title);

            return Mapper.Map<IEnumerable<AlbumBase>>(c);
        }

        public AlbumBase AlbumGetById(int id)
        {
            // Attempt to fetch the object
            var o = ds.Albums.Find(id);

            return (o == null) ? null : Mapper.Map<AlbumBase>(o);
        }

        // Attention 21 - Album get one with associated object, new

        public AlbumWithArtist AlbumGetByIdWithArtist(int id)
        {
            // Attempt to fetch the object
            var o = ds.Albums.Include("Artist")
                .SingleOrDefault(a => a.AlbumId == id);

            return (o == null) ? null : Mapper.Map<AlbumWithArtist>(o);
        }

        // Attention 22 - Album add one

        public AlbumBase AlbumAdd(AlbumAdd newItem)
        {
            // Attention 23 - To add an album, we MUST have an Artist identifier
            // We must validate that artist first by attempting to fetch it
            // If successful, then we can continue

            // Attempt to find the associated object
            var a = ds.Artists.Find(newItem.ArtistId);

            if (a == null)
            {
                return null;
            }
            else
            {
                // Attempt to add the object
                var addedItem = ds.Albums.Add(Mapper.Map<Album>(newItem));
                // Set the associated item property
                addedItem.Artist = a;
                ds.SaveChanges();

                // Return the result, or null if there was an error
                return (addedItem == null) ? null : Mapper.Map<AlbumBase>(addedItem);
            }
        }

        // Attention 24 - Album edit existing

        public AlbumBase AlbumEditTitle(AlbumEditTitle editedItem)
        {
            // Ensure that we can continue
            if (editedItem == null) { return null; }

            // Attempt to fetch the object
            var storedItem = ds.Albums.Find(editedItem.AlbumId);

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

                return Mapper.Map<AlbumBase>(storedItem);
            }
        }

        public void AlbumDelete(int id)
        {
            // Attempt to fetch the existing item
            var storedItem = ds.Albums.Find(id);

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
                    ds.Albums.Remove(storedItem);
                    ds.SaveChanges();
                }
                catch (Exception) { }
            }
        }
    }
}