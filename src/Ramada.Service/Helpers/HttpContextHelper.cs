using Microsoft.AspNetCore.Http;

namespace Ramada.Service.Helpers;

public static class HttpContextHelper
{
    public static IHttpContextAccessor HttpContextAccessor { get; set; }
    public static HttpContext HttpContext => HttpContextAccessor?.HttpContext;
    public static IHeaderDictionary ResponseHeaders => HttpContext?.Response?.Headers;
    public static long UserId => Convert.ToInt64(HttpContext?.User?.FindFirst("Id")?.Value);
}
