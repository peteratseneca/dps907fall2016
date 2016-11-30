using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;

namespace Assignment2.Controllers
{
    // Attention 02 - Artist resource models, Add, Base, and WithAlbums

    public class ArtistAdd
    {
        [Required, StringLength(120)]
        public string Name { get; set; }
    }

    // Attention 04 - Inheritance works in this simple situation
    public class ArtistBase : ArtistAdd
    {
        // Attention 03 - For most Chinook model classes, must use the [Key] attribute in the resource model classes for the identifier property

        [Key]
        public int ArtistId { get; set; }
    }

    // Attention 05 - Inheritance works in this simple situation
    public class ArtistWithAlbums : ArtistBase
    {
        public ArtistWithAlbums()
        {
            // Attention 06 - When there is a collection, initialize it in the default constructor
            Albums = new List<AlbumBase>();
        }

        public IEnumerable<AlbumBase> Albums { get; set; }
    }
}
