using CarCheckup.Domain.Core.Entities.Configs;


namespace CarCheckup.EndPoints.Api.Middleware;
public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseApiKeyValidation(this IApplicationBuilder builder) => builder.UseMiddleware<ApiKeyMiddleware>();
}
public class ApiKeyMiddleware(RequestDelegate next, SiteSettings settings)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.ToString();
        //if (path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase) ||
        // path.StartsWith("/scalar", StringComparison.OrdinalIgnoreCase))
        //{
        //    await next(context);
        //    return;
        //}

       
        if (path.Contains("CarModel", StringComparison.CurrentCultureIgnoreCase))
        {
          
            if (path.StartsWith("/api", StringComparison.OrdinalIgnoreCase))
            {
              
                if (context.Request.Headers.TryGetValue("ApiKey", out var apiKey) && !string.IsNullOrEmpty(apiKey))
                {
               
                    if (apiKey == settings.ApiKey)
                    {
                        await next(context);
                        return;
                    }

              
                    await context.Response.WriteAsync("Access Denied: Invalid ApiKey");
                    return;
                }

             
                await context.Response.WriteAsync("Access Denied: ApiKey is required");
                return;
            }
        }
        await next(context);
    

    }


}
