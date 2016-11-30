using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;

namespace Assignment2.Controllers
{
    // Attention 10 - Album resource models, Add, Base, and WithArtist
    public class AlbumAdd
    {
        [Required, StringLength(160)]
        public string Title { get; set; }

        public int ArtistId { get; set; }
    }

    public class AlbumBase : AlbumAdd
    {
        [Key]
        public int AlbumId { get; set; }
    }

    public class AlbumWithArtist : AlbumBase
    {
        public ArtistBase Artist { get; set; }
    }

    public class AlbumEditTitle
    {
        // Allow only specific properties to be edited

        [Key]
        public int AlbumId { get; set; }

        [Required, StringLength(160)]
        public string Title { get; set; }
    }
}
