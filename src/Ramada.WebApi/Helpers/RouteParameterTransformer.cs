using System.Text.RegularExpressions;

namespace Ramada.WebApi.Helpers;

public class RouteParameterTransformer : IOutboundParameterTransformer
{
    public string TransformOutbound(object value)
    {
        if (value is string val && !string.IsNullOrWhiteSpace(val))
            return Regex.Replace(val, "([a-z])([A-Z])", "$1-$2").ToLower();

        return null;
    }
}
