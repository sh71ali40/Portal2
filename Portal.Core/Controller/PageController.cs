using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Portal.DataLayer.Model.Entities;
using Portal.Infrustructure.Service;
using Portal.Service.Portal;
using Portal.Statics;

namespace Portal.Controller
{
    public class PageController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IPageService _pageService;
        private readonly IPageModuleService _pageModuleService;
        private readonly IErrorLogService _errorLogService;

        public PageController(IPageService pageService, IPageModuleService pageModuleService, IErrorLogService errorLogService)
        {
            _pageService = pageService;
            _pageModuleService = pageModuleService;
            _errorLogService = errorLogService;
        }

        public async Task<IActionResult> Index(int? pageId)
        {
            try
            {

                var currentPageId = pageId ?? Variable.MainPageId;
                var page = await _pageService.GetPageById(currentPageId);
                // check if Current User Has Page Permission
                ViewBag.Title = page.Name;
                var templateName = page.TemplateName;
                return View(templateName, page.PageModules.Where(p => p.ModuleRoles.Any()).ToList());
            }
            catch (Exception e)
            {
                _errorLogService.Add(e);
                return Redirect("/error");
            }

        }
    }
}
