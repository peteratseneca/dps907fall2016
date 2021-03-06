﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace HypermediaObjectGraph
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

            Mapper.CreateMap<Models.Artist, Controllers.ArtistBase>();
            Mapper.CreateMap<Models.Artist, Controllers.ArtistWithAlbums>();

            Mapper.CreateMap<Controllers.ArtistBase, Controllers.ArtistWithLink>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ArtistId));
            Mapper.CreateMap<Controllers.ArtistWithAlbums, Controllers.ArtistWithLink>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ArtistId));

            Mapper.CreateMap<Controllers.ArtistAdd, Models.Artist>();

            Mapper.CreateMap<Models.Album, Controllers.AlbumBase>();

            Mapper.CreateMap<Models.Album, Controllers.AlbumWithLink>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AlbumId));

            Mapper.CreateMap<Controllers.AlbumAdd, Models.Album>();
            
#pragma warning restore CS0618
        }
    }
}