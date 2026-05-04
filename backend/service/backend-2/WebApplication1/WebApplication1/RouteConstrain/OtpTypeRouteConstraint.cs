using WebApplication1.Shared.Enum;

namespace WebApplication1.RouteConstrain;

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