using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace SecuredCustomer
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
            Mapper.CreateMap<Models.Employee, Controllers.EmployeeWithCustomers>();
            Mapper.CreateMap<Controllers.EmployeeAdd, Models.Employee>();

            Mapper.CreateMap<Models.Customer, Controllers.CustomerBase>();
            Mapper.CreateMap<Models.Customer, Controllers.CustomerWithEmployee>();

            Mapper.CreateMap<Models.Customer, Controllers.CustomerEditContactInfo>();

            Mapper.CreateMap<Controllers.CustomerAdd, Models.Customer>();

#pragma warning restore CS0618
        }
    }
}