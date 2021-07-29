using Microsoft.AspNetCore.Http;
using Portal.Infrustructure.Class;
using Portal.Service.Portal;


namespace Portal.Infrustructure
{
    public interface ICoreController
    {
        ModuleConfiguration ModuleConfiguration { get; set; }
    }
    public class CoreController : ICoreController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ModuleConfiguration ModuleConfiguration { get; set; }
        

        public CoreController(IHttpContextAccessor httpContextAccessor, IPageModuleService moduleService)
        {
            _httpContextAccessor = httpContextAccessor;
            var contextModuleId = _httpContextAccessor.HttpContext.Items["moduleId"];
            if (contextModuleId == null)
            {
                ModuleConfiguration = new ModuleConfiguration(null,httpContextAccessor);
                return;
            }
            var moduleId = int.Parse(contextModuleId.ToString());
            
            var module = moduleService.GetById(moduleId);
            
            ModuleConfiguration = new ModuleConfiguration(module, httpContextAccessor);
            
        }
    }
}
