using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.ComponentModel.DataAnnotations;

namespace Assignment4.Models
{
    // Attention 01 - Instrument design model class
    public class Instrument
    {
        public Instrument()
        {
            MSRP = 0;
        }

        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Category { get; set; }

        [Required, StringLength(100)]
        public string ManufacturerName { get; set; }

        [Required, StringLength(1000)]
        public string InstrumentName { get; set; }

        [Required, StringLength(100)]
        public string ModelCode { get; set; }

        public int MSRP { get; set; }

        public byte[] PhotoMedia { get; set; }
        public string PhotoContentType { get; set; }

        // Attention 02 - BSD, audio proprerties
        public byte[] SoundClipMedia { get; set; }
        public string SoundClipContentType { get; set; }
    }
}
