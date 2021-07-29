using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Portal.Infrustructure.Service;

namespace Portal.Controller
{
    public class ErrorController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IErrorLogService _errorLogService;

        public ErrorController(IErrorLogService errorLogService)
        {
            _errorLogService = errorLogService;
        }
        [Route("error")]
        public async Task<IActionResult> Error(int? statusCode)
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var feature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            

            ViewData["ErrorUrl"] = feature?.OriginalPath;

            var exception = context?.Error; // thrown exception
            if (exception != null)
            {
                var originalPath = HttpContext.Features.Get<IExceptionHandlerPathFeature>()?.Path;
                var errorLogId = _errorLogService.Add(exception);


                Response.StatusCode = 500;
                ViewBag.ErrorId = errorLogId;
                return PartialView("/Views/Error/InternalError.cshtml");
            }

            if (statusCode != null)
            {
                Response.StatusCode = statusCode.Value;
            }
            return PartialView("/Views/Error/NotFound.cshtml");

        }
    }
}
