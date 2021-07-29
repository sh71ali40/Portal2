using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portal.Infrustructure.Class;

namespace Portal.Admin.RoleManagement
{
    public class RoleManagement : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ModuleConfiguration moduleConfiguration)
        {
            return View();
        }
    }
}
