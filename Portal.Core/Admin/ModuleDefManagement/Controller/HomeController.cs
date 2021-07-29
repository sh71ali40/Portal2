using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Portal.DataLayer.Model.Entities;
using Portal.Infrustructure;
using Portal.Infrustructure.Class;
using Portal.Infrustructure.Service;
using Portal.Service.Portal;

namespace Portal.Admin.ModuleDefManagement.Controller
{
    [Area("ModuleDefManagement")]
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ICoreController _coreController;
        private readonly IModuleDefService _moduleDefService;
        private readonly IErrorLogService _errorlogService;

        public HomeController(ICoreController coreController, IModuleDefService moduleDefService,IErrorLogService errorLogService)
        {
            _moduleDefService = moduleDefService;
            _coreController = coreController;
            _errorlogService = errorLogService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int? moduleDefId,int? pageIndex,string name,string controllerName)
        {
            try
            {
                ViewData["ModuleDefId"] = moduleDefId;
                ViewData["Name"] = name;
                ViewData["ControllerName"] = controllerName;
                var moduleDefs = _moduleDefService.GetAll()
                    .Where(m => m.Name.Contains(name??""))
                    .Where(m => m.HomeController.Contains(controllerName??""));

                if (moduleDefId != null)
                {
                    moduleDefs = moduleDefs.Where(m => m.ModuleDefId == moduleDefId.Value);
                }


                var pagedModuleDefs = await GridViewList<ModuleDef>.CreateAsync(moduleDefs.AsNoTracking(), pageIndex??1,
                    Statics.Variable.PageSize);
                return View("/Admin/ModuleDefManagement/Views/Home/Index.cshtml", pagedModuleDefs);
            }
            catch (Exception e)
            {
                _errorlogService.Add(e);
                throw;

            }
            
        }


    }
}
