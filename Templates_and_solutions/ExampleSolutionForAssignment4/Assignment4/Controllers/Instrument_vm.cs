using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;

namespace Assignment4.Controllers
{
    // Attention 05 - Resource models, can have different designs

    public class InstrumentAdd
    {
        [Required, StringLength(100)]
        public string Category { get; set; }

        [Required, StringLength(100)]
        public string ManufacturerName { get; set; }

        [Required, StringLength(1000)]
        public string InstrumentName { get; set; }

        [Required, StringLength(100)]
        public string ModelCode { get; set; }

        [Range(0, UInt32.MaxValue)]
        public int MSRP { get; set; }
    }

    // Display only, no data annotations are needed
    public class InstrumentBase : InstrumentAdd
    {
        public int Id { get; set; }

        // Attention 06 - Alternative design, add these properties to this "base" class
        //public int PhotoMediaLength { get; set; }
        //public string PhotoContentType { get; set; }
        //public int SoundClipMediaLength { get; set; }
        //public string SoundClipContentType { get; set; }
    }

    public class InstrumentWithMediaInfo : InstrumentBase
    {
        public int PhotoMediaLength { get; set; }
        public string PhotoContentType { get; set; }
        public int SoundClipMediaLength { get; set; }
        public string SoundClipContentType { get; set; }
    }

    public class InstrumentWithMedia : InstrumentWithMediaInfo
    {
        public byte[] PhotoMedia { get; set; }
        public byte[] SoundClipMedia { get; set; }
    }

}
