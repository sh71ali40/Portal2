using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Portal.Class
{
    public class FindModuleId
    {
        private readonly RequestDelegate _next;
        

        public FindModuleId(RequestDelegate next, 
            IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _next = next;
            
        }

        public async Task InvokeAsync(HttpContext context)
        {
            


            var path = context.Request.Path.ToString();
            if(!path.StartsWith("/module"))
            {
                await _next.Invoke(context);
                return;
            }

            if (context.Request.Headers["X-Requested-With"]=="XMLHttpRequest")
            {
                context.Items["PortalLayout"] = null;
            }
            else
            {
                context.Items["PortalLayout"] = "_Layout";
            }

            var splitedPath= path.Split('-');
            var moduleId = "";

            if (splitedPath.Length>1)
            {
                moduleId = splitedPath[1].Split('/')[0];
            }
            
            if (!string.IsNullOrWhiteSpace(moduleId))
            {
                context.Items["moduleId"] = moduleId.ToString();
            }
           
            await _next.Invoke(context);
        }
    }
}
