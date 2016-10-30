using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace ManageClaims
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

            // ############################################################
            // AppClaim

            Mapper.CreateMap<Models.AppClaim, Controllers.AppClaimBase>();
            Mapper.CreateMap<Controllers.AppClaimAdd, Models.AppClaim>();


#pragma warning restore CS0618
        }
    }
}