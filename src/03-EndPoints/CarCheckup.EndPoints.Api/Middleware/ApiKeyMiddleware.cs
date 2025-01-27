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
        if (path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase) ||
         path.StartsWith("/scalar", StringComparison.OrdinalIgnoreCase))
        {
            await next(context);
            return;
        }

        // فقط مسیرهایی که شامل CarModel هستند بررسی می‌شوند
        if (path.Contains("CarModel", StringComparison.CurrentCultureIgnoreCase))
        {
            // فقط مسیرهای داخل "/api" باید چک شوند
            if (path.StartsWith("/api", StringComparison.OrdinalIgnoreCase))
            {
                // بررسی وجود Header با کلید ApiKey
                if (context.Request.Headers.TryGetValue("ApiKey", out var apiKey) && !string.IsNullOrEmpty(apiKey))
                {
                    // اعتبارسنجی ApiKey
                    if (apiKey == settings.ApiKey)
                    {
                        await next(context); // ادامه پردازش
                        return;
                    }

                    context.Response.StatusCode = 403; // Unauthorized
                    await context.Response.WriteAsync("Access Denied: Invalid ApiKey");
                    return;
                }

                context.Response.StatusCode = 400; // Bad Request
                await context.Response.WriteAsync("Access Denied: ApiKey is required");
                return;
            }
        }

        // اگر مسیر ربطی به CarModel نداشت، درخواست به میدل‌ویر بعدی هدایت می‌شود
        await next(context);
    }


}
