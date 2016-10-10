using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace WebAndApiControllers
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

            Mapper.CreateMap<Models.Employee, Controllers.EmployeeBase>();

            Mapper.CreateMap<Controllers.EmployeeBase, Controllers.EmployeeWithLink>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EmployeeId));

            Mapper.CreateMap<Controllers.EmployeeAdd, Models.Employee>();

#pragma warning restore CS0618
        }
    }
}