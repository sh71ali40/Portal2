using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
using Microsoft.AspNetCore.Mvc;
using Portal.DataLayer.Model;
using Portal.DataLayer.Model.Entities;

namespace Portal.Infrustructure.Filter
{
    public class HasDefinedPermission : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly int _permissionId;
        
        public HasDefinedPermission(int permissionId)
        {
            _permissionId = permissionId;
            
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userClaim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
            if (userClaim==null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var userId =   int.Parse(userClaim)  ;
           var unitOfWork =
               context.HttpContext.RequestServices.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
           var moduleRoles = unitOfWork.Set<ModuleRole>();
            var moduleId = int.Parse((string)context.HttpContext.Items["moduleId"]);
            var hasPermission = moduleRoles
                .Any(mr => mr.ModuleId == moduleId &&
                           mr.PermissionId == _permissionId &&
                           mr.Role.UserRoles.Any(ur => ur.UserId == userId));

            if (!hasPermission)
            {
                context.Result = new UnauthorizedResult();
            }
            
        }
    }
}
