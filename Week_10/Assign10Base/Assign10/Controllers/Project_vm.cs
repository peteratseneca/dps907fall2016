using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.ComponentModel.DataAnnotations;

namespace Assign10.Controllers
{
    // Attention - Project entity resource models
    // Add, Base, WithMediaInfo

    public class ProjectAdd
    {
        [Required, StringLength(200)]
        public string Name { get; set; }

        [Required, StringLength(2000)]
        public string Description { get; set; }

        [Required, StringLength(100)]
        public string Visibility { get; set; } // Private, Public, Shared

        // Optional
        [StringLength(200)]
        public string Tags { get; set; }
    }

    // Delivery only, no data annotations needed
    public class ProjectAddTemplate
    {
        public string Name { get { return "Project name, required, text, 200 character limit"; } }
        public string Description { get { return "Description of the project, required, text, 2000 character limit"; } }
        public string Visibility { get { return "Visibility (Private, Public, Shared), required, text, 100 character limit"; } }
        public string Tags { get { return "Descriptive tags, optional, text, 200 character limit"; } }
    }

    // Delivery only, no data annotations needed
    public class ProjectBase : ProjectAdd
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Owner { get; set; }

    }

    // Delivery only, no data annotations needed
    public class ProjectWithMediaInfo : ProjectBase
    {
        public ICollection<MediaBase> Medias { get; set; }
    }

    // ############################################################

    // Classes that have link relations

    public class ProjectWithLink : ProjectWithMediaInfo
    {
        public Link Link { get; set; }
    }

    public class ProjectLinked : LinkedItem<ProjectWithLink>
    {
        // Constructor - call the base class constructor

        // All use cases except "add new"
        public ProjectLinked(ProjectWithLink item) : base(item) { }

        // "Add new" use case
        public ProjectLinked(ProjectWithLink item, int id) : base(item, id) { }
    }

    public class ProjectsLinked : LinkedCollection<ProjectWithLink>
    {
        // Constructor - call the base class constructor
        public ProjectsLinked(IEnumerable<ProjectWithLink> collection) : base(collection)
        {
            Template = new ProjectAddTemplate();
        }

        public ProjectAddTemplate Template { get; set; }
    }

}
