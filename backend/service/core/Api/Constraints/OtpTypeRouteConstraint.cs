using Common.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Api.Constraints
{
    public class OtpTypeRouteConstraint : IRouteConstraint
    {
        public bool Match(
            HttpContext? httpContext,
            IRouter? route,
            string routeKey,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            // 1. Get the value from the URL (e.g., "registration")
            if (values.TryGetValue(routeKey, out var value) && value != null)
            {
                var stringValue = value.ToString();

                // 2. Check if it matches your new Enum (0, 1, 2, etc.)
                // This handles the string-to-enum conversion for the route
                return Enum.TryParse<OtpType>(stringValue, true, out _);
            }

            return false;
        }
    }
}