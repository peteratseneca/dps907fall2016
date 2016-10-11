using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace LinkRelationsIntro
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

            Mapper.CreateMap<Controllers.EmployeeAdd, Models.Employee>();

            // Attention 09 - Special CreateMap code for the Chinook database
            // Had to add an "Id" property, with an initial value of "EmployeeId"
            // Otherwise the link relations factory won't work correctly
            Mapper.CreateMap<Controllers.EmployeeBase, Controllers.EmployeeWithLink>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EmployeeId));

            Mapper.CreateMap<Models.Employee, Controllers.EmployeeBase>();

#pragma warning restore CS0618
        }
    }
}