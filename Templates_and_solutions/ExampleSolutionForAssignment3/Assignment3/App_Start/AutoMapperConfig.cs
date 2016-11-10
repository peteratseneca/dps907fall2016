using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace Assignment3
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Add map creation statements here
            // Mapper.CreateMap< FROM , TO >();

            // Disable AutoMapper v4.2.x warnings
#pragma warning disable CS0618

            // Attention 01 - Infrastructure - AutoMapper create-maps

            Mapper.CreateMap<Models.Invoice, Controllers.InvoiceBase>();
            Mapper.CreateMap<Models.Invoice, Controllers.InvoiceWithCustomerInfo>();

            Mapper.CreateMap<Models.InvoiceLine, Controllers.InvoiceLineBase>();
            Mapper.CreateMap<Models.InvoiceLine, Controllers.InvoiceLineWithDetail>();

            Mapper.CreateMap<Models.Employee, Controllers.EmployeeBase>();
            Mapper.CreateMap<Models.Employee, Controllers.EmployeeWithDetails>();
            Mapper.CreateMap<Controllers.EmployeeAdd, Models.Employee>();

#pragma warning restore CS0618
        }
    }
}