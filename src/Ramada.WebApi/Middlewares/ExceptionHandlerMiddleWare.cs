using Microsoft.AspNetCore.Diagnostics;
using Ramada.Service.Exceptions;
using Ramada.WebApi.Models;

namespace Ramada.WebApi.Middlewares;

public class ExceptionHandlerMiddleWare(RequestDelegate next, RequestDelegate request, ILogger<ExceptionHandlerMiddleware> logger)
{
    public readonly RequestDelegate request;
    public readonly ILogger<ExceptionHandlerMiddleware> logger;

    public async Task Invoke(HttpContext context)
    {
		try
		{
			await next.Invoke(context);
		}
		catch (AlreadyExistException ex)
		{
            context.Response.StatusCode = ex.StatusCode;
            await context.Response.WriteAsJsonAsync(new Response()
            {
                Message = ex.Message,
                StatusCode = ex.StatusCode
            });

        }
        catch (ArgumentIsNotValidException ex)
        {
            context.Response.StatusCode = ex.StatusCode;
            await context.Response.WriteAsJsonAsync(new Response()
            {
                Message = ex.Message,
                StatusCode = ex.StatusCode
            });
        }
        catch (CustomException ex)
        {
            context.Response.StatusCode = ex.StatusCode;
            await context.Response.WriteAsJsonAsync(new Response()
            {
                Message = ex.Message,
                StatusCode = ex.StatusCode
            });
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode = ex.StatusCode;
            await context.Response.WriteAsJsonAsync(new Response()
            {
                Message = ex.Message,
                StatusCode = ex.StatusCode
            });
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            this.logger.LogError(ex.ToString());
            await context.Response.WriteAsJsonAsync(new Response()
            {
                Message = ex.Message,
                StatusCode = 500
            });
        }
    }
}
