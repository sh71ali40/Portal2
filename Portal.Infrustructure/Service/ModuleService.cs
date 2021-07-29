using Microsoft.AspNetCore.Http;
using Portal.Infrustructure.Class;
using Portal.Service.Portal;

namespace Portal.Infrustructure.Service
{
    public interface IModuleService
    {
        ModuleConfiguration GetModuleById(int moduleId);
    }
    public class ModuleService : IModuleService
    {
        private readonly IPageModuleService _pageModuleService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ModuleService(IPageModuleService pageModuleService, IHttpContextAccessor httpContextAccessor)
        {
            _pageModuleService = pageModuleService;
            _httpContextAccessor = httpContextAccessor;
        }
        public ModuleConfiguration GetModuleById(int moduleId)
        {

            var pageModule = _pageModuleService.GetById(moduleId);
            return new ModuleConfiguration(pageModule, _httpContextAccessor);
        }
    }
}
