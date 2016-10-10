using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HypermediaObjectGraph.Controllers
{
    // Attention 05 - Album resource models, with link relations classes

    public class AlbumAdd
    {
        [Required, StringLength(160)]
        public string Title { get; set; }

        [Range(1,UInt32.MaxValue)]
        public int ArtistId { get; set; }
    }

    public class AlbumBase : AlbumAdd
    {
        [Key]
        public int AlbumId { get; set; }
    }

    // ############################################################
    // Linked classes

    public class AlbumWithLink : AlbumBase
    {
        public Link Link { get; set; }

        // Duplicated identity field
        [JsonIgnore]
        public int Id { get; set; }
    }

    public class AlbumLinked : LinkedItem<AlbumWithLink>
    {
        public AlbumLinked(AlbumWithLink item) : base(item) { }
    }

    public class AlbumsLinked : LinkedCollection<AlbumWithLink>
    {
        public AlbumsLinked(IEnumerable<AlbumWithLink> collection) : base(collection) { }
    }
}