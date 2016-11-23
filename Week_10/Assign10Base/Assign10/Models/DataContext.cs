using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Assign10.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=DataContext") {  }

        // DbSet<TEntity> properties
        public DbSet<Project> Projects { get; set; }
        public DbSet<Media> Medias { get; set; }
    }
}
