using Custra.Application.Common.Exceptions;
using FluentValidation;

namespace Custra.Web.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ForbiddenAccessException)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
        }
        catch (UnauthorizedAccessException)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response.WriteAsJsonAsync(new
            {
                errors = ex.Errors.Select(x => x.ErrorMessage)
            });
        }
        catch (KeyNotFoundException)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception.");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}