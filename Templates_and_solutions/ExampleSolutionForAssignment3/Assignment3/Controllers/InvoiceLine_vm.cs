using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;

namespace Assignment3.Controllers
{
    // Attention 05 - InvoiceLine resource models

    public class InvoiceLineBase
    {
        [Key]
        public int InvoiceLineId { get; set; }
        public int InvoiceId { get; set; }
        public int TrackId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }

    public class InvoiceLineWithDetail : InvoiceLineBase
    {
        // Attention 06 - InvoiceLine with composite properties from Track, Album, Artist, and MediaType objects

        public string TrackName { get; set; }
        public string TrackComposer { get; set; }

        // Album and artist
        public string TrackAlbumTitle { get; set; }
        public string TrackAlbumArtistName { get; set; }

        // Media type
        public string TrackMediaTypeName { get; set; }
    }

}
