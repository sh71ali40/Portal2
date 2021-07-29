if  NOT  EXISTS ( SELECT * FROM ModuleDef WHERE ModuleDefId = 5003)
insert into ModuleDef(ModuleDefId,Name,HomeController,DllName,Enabled) values(5003,N'مدیریت صفحات','PageManagement','Portal.Core',1)
GO
IF  NOT  EXISTS ( SELECT * FROM PermissionRoleBase WHERE PermissionId = 500300)
insert into PermissionRoleBase (PermissionId,PermissionName,IsManager) values(500300,N'مشاهده',1)
GO

IF  NOT  EXISTS ( SELECT * FROM PermissionRoleBase WHERE PermissionId = 500301)
insert into PermissionRoleBase (PermissionId,PermissionName,IsManager) values(500301,N'مدیریت',1)
GO

if  NOT  EXISTS ( SELECT * FROM PermissionRoleModuleDef WHERE PermissionId = 500300 and ModuleDefId=5003)
 insert into PermissionRoleModuleDef(ModuleDefId,PermissionId) values(5003,500300)
GO

if  NOT  EXISTS ( SELECT * FROM PermissionRoleModuleDef WHERE PermissionId = 500301 and ModuleDefId=5003)
 insert into PermissionRoleModuleDef(ModuleDefId,PermissionId) values(5003,500301)
GO
