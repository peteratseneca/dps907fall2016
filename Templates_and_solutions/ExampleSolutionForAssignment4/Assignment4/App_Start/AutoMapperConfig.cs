using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace Assignment4
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Add map creation statements here
            // Mapper.CreateMap< FROM , TO >();

            // Disable AutoMapper v4.2.x warnings
#pragma warning disable CS0618

            Mapper.CreateMap<Models.Instrument, Controllers.InstrumentBase>();
            Mapper.CreateMap<Models.Instrument, Controllers.InstrumentWithMediaInfo>();
            Mapper.CreateMap<Models.Instrument, Controllers.InstrumentWithMedia>();
            Mapper.CreateMap<Controllers.InstrumentWithMedia, Controllers.InstrumentWithMediaInfo>();

            Mapper.CreateMap<Controllers.InstrumentAdd, Models.Instrument>();

#pragma warning restore CS0618
        }
    }
}