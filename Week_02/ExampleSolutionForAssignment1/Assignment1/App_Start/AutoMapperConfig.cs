using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// added...
using AutoMapper;

namespace Assignment1
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Add map creation statements here
            // Mapper.CreateMap< FROM , TO >();

            // Disable AutoMapper v4.2.x warnings
#pragma warning disable CS0618

            // Attention 01 - AutoMapper added, here are the create map statements

            Mapper.CreateMap<Models.Smartphone, Controllers.SmartphoneBase>();
            Mapper.CreateMap<Controllers.SmartphoneAdd, Models.Smartphone>();

#pragma warning restore CS0618
        }
    }
}
