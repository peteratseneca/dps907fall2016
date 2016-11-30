using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace Assignment5
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Add map creation statements here
            // Mapper.CreateMap< FROM , TO >();

            // Disable AutoMapper v4.2.x warnings
#pragma warning disable CS0618

            // Attention 16 - Customer create map statements

            Mapper.CreateMap<Models.Customer, Controllers.CustomerBase>();
            Mapper.CreateMap<Models.Customer, Controllers.CustomerWithData>();

            Mapper.CreateMap<Controllers.CustomerBase, Controllers.CustomerWithLink>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CustomerId));
            Mapper.CreateMap<Controllers.CustomerWithData, Controllers.CustomerWithLink>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CustomerId));

            Mapper.CreateMap<Controllers.CustomerAdd, Models.Customer>();

            // Attention 43 - Invoice create map statements

            Mapper.CreateMap<Models.Invoice, Controllers.InvoiceWithData>();

            Mapper.CreateMap<Controllers.InvoiceWithData, Controllers.InvoiceWithLink>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InvoiceId));

#pragma warning restore CS0618
        }
    }
}