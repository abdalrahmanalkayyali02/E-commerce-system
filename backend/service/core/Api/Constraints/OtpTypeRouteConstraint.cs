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
            if (values.TryGetValue(routeKey, out var value) && value != null)
            {
                var stringValue = value.ToString();

                return Enum.TryParse<OtpType>(stringValue, true, out _);
            }

            return false;
        }
    }
}