if  NOT  EXISTS ( SELECT * FROM ModuleDef WHERE ModuleDefId = 5002)
insert into ModuleDef(ModuleDefId,Name,HomeController,DllName,Enabled) values(5002,N'مدیریت ماژول ها','ModuleDefManagement/Home','Portal.Core',1)
GO
IF  NOT  EXISTS ( SELECT * FROM PermissionRoleBase WHERE PermissionId = 500200)
insert into PermissionRoleBase (PermissionId,PermissionName,IsManager) values(500200,N'مشاهده',1)
GO

IF  NOT  EXISTS ( SELECT * FROM PermissionRoleBase WHERE PermissionId = 500201)
insert into PermissionRoleBase (PermissionId,PermissionName,IsManager) values(500201,N'مدیریت',1)
GO

if  NOT  EXISTS ( SELECT * FROM PermissionRoleModuleDef WHERE PermissionId = 500200 and ModuleDefId=5002)
 insert into PermissionRoleModuleDef(ModuleDefId,PermissionId) values(5002,500200)
GO

if  NOT  EXISTS ( SELECT * FROM PermissionRoleModuleDef WHERE PermissionId = 500201 and ModuleDefId=5002)
 insert into PermissionRoleModuleDef(ModuleDefId,PermissionId) values(5002,500201)
GO
