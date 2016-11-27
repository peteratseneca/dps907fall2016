using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace AssociationsOther
{
    // Attention 11 - AutoMapper configuration class, called from the Application_Start method (in the WebApiApplication class in Global.asax.cs)

    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Add map creation statements here
            // Mapper.CreateMap< FROM , TO >();

            // Disable AutoMapper v4.2.x warnings
#pragma warning disable CS0618

            Mapper.CreateMap<Models.Employee, Controllers.EmployeeBase>();
            Mapper.CreateMap<Models.Employee, Controllers.EmployeeBase2>();
            Mapper.CreateMap<Models.Employee, Controllers.EmployeeBase3>();
            Mapper.CreateMap<Controllers.EmployeeAdd, Models.Employee>();

            Mapper.CreateMap<Models.Address, Controllers.AddressBase>();
            Mapper.CreateMap<Models.Address, Controllers.AddressFull>();
            Mapper.CreateMap<Controllers.AddressAdd, Models.Address>();

            Mapper.CreateMap<Models.JobDuty, Controllers.JobDutyBase>();
            Mapper.CreateMap<Models.JobDuty, Controllers.JobDutyFull>();
            Mapper.CreateMap<Controllers.JobDutyAdd, Models.JobDuty>();

#pragma warning restore CS0618
        }
    }
}