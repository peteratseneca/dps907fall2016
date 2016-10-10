using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using HypermediaObjectGraph.Models;

namespace HypermediaObjectGraph.Controllers
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

        public IEnumerable<ArtistBase> ArtistGetAll()
        {
            var c = ds.Artists.OrderBy(a => a.Name).Take(25);

            return Mapper.Map<IEnumerable<ArtistBase>>(c);
        }

        // Method that does NOT fetch the associated object(s)
        /*
        public ArtistBase ArtistGetById(int id)
        {
            var o = ds.Artists.SingleOrDefault(a => a.ArtistId == id);

            return (o == null) ? null : Mapper.Map<ArtistBase>(o);
        }
        */

        // Attention 10 - Fetch and artist, with its albums collection
        public ArtistWithAlbums ArtistGetById(int id)
        {
            var o = ds.Artists
                .Include("Albums")
                .SingleOrDefault(a => a.ArtistId == id);

            return (o == null) ? null : Mapper.Map<ArtistWithAlbums>(o);
        }
    }
}