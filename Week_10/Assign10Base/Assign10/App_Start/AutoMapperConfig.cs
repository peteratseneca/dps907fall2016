using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace Assign10
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Add map creation statements here
            // Mapper.CreateMap< FROM , TO >();

            // Disable AutoMapper v4.2.x warnings
#pragma warning disable CS0618

            // AutoMapper create map statements

            Mapper.CreateMap<Models.Project, Controllers.ProjectBase>();
            Mapper.CreateMap<Models.Project, Controllers.ProjectWithMediaInfo>();
            Mapper.CreateMap<Controllers.ProjectAdd, Models.Project>();
            Mapper.CreateMap<Controllers.ProjectBase, Controllers.ProjectWithLink>();
            Mapper.CreateMap<Controllers.ProjectWithMediaInfo, Controllers.ProjectWithLink>();

            Mapper.CreateMap<Models.Media, Controllers.MediaBase>();
            Mapper.CreateMap<Models.Media, Controllers.ProjectWithMediaInfo>();
            Mapper.CreateMap<Models.Media, Controllers.MediaWithMediaItem>();
            Mapper.CreateMap<Controllers.MediaAdd, Models.Media>();
            Mapper.CreateMap<Controllers.MediaBase, Controllers.MediaWithLink>();


#pragma warning restore CS0618
        }
    }
}