dotnet ef dbcontext scaffold "Data Source=(local);Initial Catalog=PortalCore3;Integrated Security = true" Microsoft.EntityFrameworkCore.SqlServer -d -c PortalContext -t ModuleDef -t Page -t PageModule -t ModuleDefSetting -t ModuleSetting -t ModuleRole -t PermissionRoleBase -t PermissionRoleModuleDef -t Role -t PageRole -t ModuleDefRole -t User -t UserRole -t ErrorLog -t ObjLog -o Model\Entities --force --startup-project ../Portal.Core
pause;