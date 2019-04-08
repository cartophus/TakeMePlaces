using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace ProiectIP.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetAge(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Age");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetSex(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Sex");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetOccupation(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Occupation");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetZipcode(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Zipcode");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}