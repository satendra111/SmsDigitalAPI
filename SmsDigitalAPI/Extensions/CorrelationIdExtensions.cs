using API.Middleware;

namespace API.Extensions
{
    public static class CorrelationIdExtensions
    {
        public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            return app.UseMiddleware<CorrelationIdMiddleware>();
        }


    }
}
