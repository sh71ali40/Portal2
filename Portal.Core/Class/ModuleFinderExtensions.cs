using Microsoft.AspNetCore.Builder;

namespace Portal.Class
{
    public static class ModuleFinderExtensions
    {
        public static IApplicationBuilder UseModuleFinder(
            this IApplicationBuilder app
        )
        {
            return app.UseMiddleware<FindModuleId>();

        }
    }
}
