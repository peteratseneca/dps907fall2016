using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HypermediaObjectGraph.Controllers
{
    // Attention 01 - Artist resource models, with link relations classes

    public class ArtistAdd
    {
        [Required, StringLength(120)]
        public string Name { get; set; }
    }

    public class ArtistBase : ArtistAdd
    {
        [Key]
        public int ArtistId { get; set; }
    }

    // Attention 02 - Artist with albums, the collection is made of AlbumBase or AlbumWithLink objects
    // When AlbumWithLink is used, links can be added for each album in the collection
    public class ArtistWithAlbums : ArtistBase
    {
        public ArtistWithAlbums()
        {
            //Albums = new List<AlbumBase>();
            Albums = new List<AlbumWithLink>();
        }

        //public IEnumerable<AlbumBase> Albums { get; set; }
        public IEnumerable<AlbumWithLink> Albums { get; set; }
    }

    // ############################################################
    // Linked classes

    public class ArtistWithLink : ArtistWithAlbums
    {
        public Link Link { get; set; }

        // Duplicated identity field
        [JsonIgnore]
        public int Id { get; set; }
    }

    public class ArtistLinked : LinkedItem<ArtistWithLink>
    {
        public ArtistLinked(ArtistWithLink item) : base(item) { }
    }

    public class ArtistsLinked : LinkedCollection<ArtistWithLink>
    {
        public ArtistsLinked(IEnumerable<ArtistWithLink> collection) : base(collection) { }
    }
}