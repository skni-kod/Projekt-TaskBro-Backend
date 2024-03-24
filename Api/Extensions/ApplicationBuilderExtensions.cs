using Api.Middleware;

namespace Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
}