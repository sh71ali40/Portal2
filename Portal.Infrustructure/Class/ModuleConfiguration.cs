using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Portal.DataLayer.Model;
using Portal.DataLayer.Model.Entities;


namespace Portal.Infrustructure.Class
{
    public class ModuleConfiguration
    {
        private readonly PageModule _pageModule;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IQueryable<PageModule> _currentPageModuleQuery;

        private int? _currentUserId
        {
            get
            {
                var userClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
                var userId = userClaim != null ? int.Parse(userClaim) : (int?)null;
                return userId;
            }
        }
        public int? ModuleId => _pageModule?.ModuleId;
        public int? PageId => _pageModule?.PageId;
        public int? ModuleDefId => _pageModule?.ModuleDefId;
        public string HomeController => _pageModule?.ModuleDef.HomeController;
        public string Name => _pageModule?.ModuleDef.Name;

        public ModuleConfiguration(PageModule pageModule, IHttpContextAccessor httpContextAccessor)
        {
            _pageModule = pageModule;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork =
                httpContextAccessor.HttpContext.RequestServices.GetService(typeof(IUnitOfWork)) as IUnitOfWork;

            //_currentPageModuleQuery = _unitOfWork.Set<PageModule>().Where(p => p.ModuleId == ModuleId);
        }

        public string GetSettingValue(int settingId)
        {
            if (ModuleId != null) // for modular module
            {
                var moduleSetting = _pageModule.ModuleSettings
                    .FirstOrDefault(s => s.ModuleId == ModuleId && s.SettingId == settingId);
                return moduleSetting?.SettingValue;

            }
            // for non modular module
            var moduleDefSetting = _unitOfWork.Set<ModuleDefSetting>().FirstOrDefault(s => s.SettingId == settingId);
            return moduleDefSetting?.NonModularValue;
        }

        public bool HasDefinedPermission(int permissionId)
        {
            if (ModuleId != null) // for modular module
            {
                var hasPermission = _pageModule.ModuleRoles
                    .Any(mr => mr.ModuleId == ModuleId &&
                               mr.PermissionId == permissionId &&
                               mr.Role.UserRoles.Any(ur => ur.UserId == _currentUserId));
                return hasPermission;
            }
            // for non modular module
            var moduleDefPermission = _unitOfWork.Set<ModuleDefRole>().Any(mr => mr.PermissionId == permissionId &&
                                                                                 mr.Role.UserRoles.Any(ur =>
                                                                                     ur.UserId == _currentUserId));
            return moduleDefPermission;
        }
    }
}
