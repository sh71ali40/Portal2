using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;
using Portal.Service.Portal;


namespace Portal.Core.Admin.PageManagement.Controller.Home
{
    [Area("PageManagement")]
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IPageService _pageService;

        public HomeController(IPageService pageService)
        {
            _pageService = pageService;
        }
        public IActionResult Index()
        {
            var pages = _pageService.GetAll().ToList();
            ViewBag.PageJson = JsonSerializer.Serialize(pages);
            return View("/Admin/PageManagement/Views/Home/Index.cshtml");
        }
    }
}
