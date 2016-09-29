using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace MediaUpload
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Add map creation statements here
            // Mapper.CreateMap< FROM , TO >();

            // Disable AutoMapper v4.2.x warnings
#pragma warning disable CS0618

            Mapper.CreateMap<Models.Book, Controllers.BookBase>();
            Mapper.CreateMap<Models.Book, Controllers.BookWithMediaInfo>();
            Mapper.CreateMap<Models.Book, Controllers.BookWithMedia>();

            Mapper.CreateMap<Controllers.BookWithMedia, Controllers.BookWithMediaInfo>();
            
            Mapper.CreateMap<Controllers.BookAdd, Models.Book>();

#pragma warning restore CS0618
        }
    }
}
