using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.ComponentModel.DataAnnotations;

namespace Assign10.Controllers
{
    // Attention - Media entity resource models
    // Add, Base, WithMediaItem

    public class MediaAdd
    {
        [Required, StringLength(200)]
        public string Name { get; set; }

        [Required, StringLength(2000)]
        public string Description { get; set; }

        [Range(1,UInt32.MaxValue)]
        public int ProjectId { get; set; }
    }

    // Delivery only, no data annotations needed
    public class MediaAddTemplate
    {
        public string Name { get { return "Name of the media item, required, text, 200 character limit"; } }
        public string Description { get { return "Description of the media item, required, text, 2000 character limit"; } }
        public string ProjectId { get { return "Associated project identifier, required"; } }
    }

    // Delivery only, no data annotations needed
    public class MediaBase : MediaAdd
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Owner { get; set; }
        public string ItemContentType { get; set; }
    }

    // Delivery only, no data annotations needed
    public class MediaWithMediaItem : MediaBase
    {
        public byte[] ItemMedia { get; set; }
        public string ProjectName { get; set; }
    }

    // ############################################################

    // Classes that have link relations

    public class MediaWithLink : MediaBase
    {
        public Link Link { get; set; }
    }

    public class MediaLinked : LinkedItem<MediaWithLink>
    {
        // Constructor - call the base class constructor

        // All use cases except "add new"
        public MediaLinked(MediaWithLink item) : base(item) { }

        // "Add new" use case
        public MediaLinked(MediaWithLink item, int id) : base(item, id) { }
    }

    public class MediasLinked : LinkedCollection<MediaWithLink>
    {
        // Constructor - call the base class constructor
        public MediasLinked(IEnumerable<MediaWithLink> collection) : base(collection)
        {
            Template = new MediaAddTemplate();
        }

        public MediaAddTemplate Template { get; set; }
    }

}
