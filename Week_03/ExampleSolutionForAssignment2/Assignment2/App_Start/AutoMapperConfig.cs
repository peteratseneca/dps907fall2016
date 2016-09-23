using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace Assignment2
{
    // Attention 01 - AutoMapper config, also added a statement in the WebApiApplication class that calls RegisterMappings

    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Add map creation statements here
            // Mapper.CreateMap< FROM , TO >();

            // Disable AutoMapper v4.2.x warnings
#pragma warning disable CS0618

            Mapper.CreateMap<Models.Artist, Controllers.ArtistBase>();
            Mapper.CreateMap<Models.Artist, Controllers.ArtistWithAlbums>();
            Mapper.CreateMap<Controllers.ArtistAdd, Models.Artist>();

            Mapper.CreateMap<Models.Album, Controllers.AlbumBase>();
            Mapper.CreateMap<Models.Album, Controllers.AlbumWithArtist>();
            Mapper.CreateMap<Controllers.AlbumAdd, Models.Album>();

#pragma warning restore CS0618
        }
    }
}