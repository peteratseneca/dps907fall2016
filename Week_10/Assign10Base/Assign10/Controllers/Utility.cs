using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.Net.Http.Headers;
using System.Web.Http;
using Assign10.ServiceLayer;

namespace Assign10.Controllers
{
    public static class Utility
    {
        // Attention - Class to determine media handling capability
        // Check the request headers, and determine whether the 
        // ByteFormatter class will be handling the request

        public static bool IsRequestForMediaItem(HttpRequestHeaders headers)
        {
            // Will look for one only accept header
            if (headers.Accept.Count != 1) { return false; }

            var acceptHeader = headers.Accept.First().MediaType.ToString();

            // Can do a wildcard match, or an exact match

            if (acceptHeader.EndsWith("/*"))
            {
                // Wild card match

                // Get the beginning of the header, up to the wildcard characters
                acceptHeader = acceptHeader.Substring(0, acceptHeader.IndexOf("/*"));

                var formatter = new ByteFormatter();
                var mthv = formatter.SupportedMediaTypes
                    .FirstOrDefault(smt => smt.MediaType.StartsWith(acceptHeader));
                return (mthv == null) ? false : true;
            }
            else
            {
                // Exact match
                var formatter = new ByteFormatter();
                var mthv = formatter.SupportedMediaTypes
                    .FirstOrDefault(smt => smt.MediaType == acceptHeader);
                return (mthv == null) ? false : true;
            }
        }

    }

}
