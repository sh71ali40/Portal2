
GO
/****** Object:  Table [dbo].[ModuleDef]    Script Date: 12/16/2020 11:38:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModuleDef](
	[ModuleDefId] [int] NOT NULL,
	[Name] [varchar](250) NOT NULL,
	[ViewComponentName] [varchar](250) NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_Modules] PRIMARY KEY CLUSTERED 
(
	[ModuleDefId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ModuleDefSetting]    Script Date: 12/16/2020 11:38:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModuleDefSetting](
	[SettingId] [int] NOT NULL,
	[ModuleDefId] [int] NOT NULL,
	[SettingName] [nvarchar](250) NOT NULL,
	[SettingValues] [nvarchar](max) NULL,
	[DefaultValue] [nvarchar](max) NULL,
	[SettingHelp] [nvarchar](max) NULL,
 CONSTRAINT [PK_ModuleDefSetting] PRIMARY KEY CLUSTERED 
(
	[SettingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ModuleRole]    Script Date: 12/16/2020 11:38:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModuleRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ModuleId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL,
 CONSTRAINT [PK_ModuleRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ModuleSetting]    Script Date: 12/16/2020 11:38:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModuleSetting](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ModuleId] [int] NOT NULL,
	[SettingId] [int] NOT NULL,
	[SettingValue] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ModuleSetting] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Page]    Script Date: 12/16/2020 11:38:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Page](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[ParentId] [int] NULL,
	[TemplateName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PageModule]    Script Date: 12/16/2020 11:38:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageModule](
	[ModuleId] [int] IDENTITY(1,1) NOT NULL,
	[PageId] [int] NOT NULL,
	[ModuleDefId] [int] NOT NULL,
	[PaneName] [varchar](50) NULL,
	[IsVisible] [bit] NOT NULL,
 CONSTRAINT [PK_PageModule] PRIMARY KEY CLUSTERED 
(
	[ModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PageRole]    Script Date: 12/16/2020 11:38:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PageId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_PageRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionRoleBase]    Script Date: 12/16/2020 11:38:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionRoleBase](
	[PermissionId] [int] NOT NULL,
	[PermissionName] [nvarchar](250) NOT NULL,
	[IsManager] [bit] NOT NULL,
 CONSTRAINT [PK_PermissionRoleBase] PRIMARY KEY CLUSTERED 
(
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionRoleModuleDef]    Script Date: 12/16/2020 11:38:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionRoleModuleDef](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ModuleDefId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL,
 CONSTRAINT [PK_PermissionRoleModuleDef] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 12/16/2020 11:38:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 12/16/2020 11:38:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[UserPass] [nvarchar](100) NOT NULL,
	[Email] [varchar](50) NULL,
	[UserName] [varchar](50) NOT NULL,
	[IsSuperAdmin] [bit] NOT NULL,
	[IsLocked] [bit] NOT NULL,
	[LastLoginDate] [datetime] NULL,
	[LastPasswordChangeDate] [datetime] NULL,
	[LastModifierUserId] [bigint] NULL,
	[LastModificationDateTime] [datetime] NULL,
	[FailedPasswordAttemptCount] [int] NULL,
	[Mobile] [varchar](12) NULL,
	[HasChangedPassword] [bit] NOT NULL,
	[JoinDate] [datetime] NOT NULL,
	[JoinIP] [varchar](15) NOT NULL,
	[LastIP] [varchar](15) NULL,
	[EmailConfirmed] [bit] NULL,
	[MobileConfirmed] [bit] NULL,
	[Picture] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedDateTime] [datetime] NULL,
	[DeleterUserId] [bigint] NULL,
	[SentCode] [int] NULL,
	[SentCodeExpirationDateTime] [datetime] NULL,
	[CreditCardNumber] [varchar](20) NULL,
 CONSTRAINT [PK_t_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 12/16/2020 11:38:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ModuleDef] ([ModuleDefId], [Name], [ViewComponentName], [Enabled]) VALUES (5001, N'اعتبارسنجي', N'Authentication', 1)
GO
INSERT [dbo].[ModuleDefSetting] ([SettingId], [ModuleDefId], [SettingName], [SettingValues], [DefaultValue], [SettingHelp]) VALUES (500100, 5001, N'شماره صفحه', NULL, NULL, N'پس از لاگين، کاربر به اين صفحه ارسال مي شود')
GO
SET IDENTITY_INSERT [dbo].[ModuleSetting] ON 
GO
INSERT [dbo].[ModuleSetting] ([Id], [ModuleId], [SettingId], [SettingValue]) VALUES (2, 1002, 500100, N'2')
GO
SET IDENTITY_INSERT [dbo].[ModuleSetting] OFF
GO
SET IDENTITY_INSERT [dbo].[Page] ON 
GO
INSERT [dbo].[Page] ([Id], [Name], [ParentId], [TemplateName]) VALUES (1, N'صفحه اصلی', NULL, N'OneColumn')
GO
SET IDENTITY_INSERT [dbo].[Page] OFF
GO
SET IDENTITY_INSERT [dbo].[PageModule] ON 
GO
INSERT [dbo].[PageModule] ([ModuleId], [PageId], [ModuleDefId], [PaneName], [IsVisible]) VALUES (1002, 1, 5001, N'FirstRow', 1)
GO
SET IDENTITY_INSERT [dbo].[PageModule] OFF
GO
INSERT [dbo].[PermissionRoleBase] ([PermissionId], [PermissionName], [IsManager]) VALUES (500100, N'مشاهده', 1)
GO
SET IDENTITY_INSERT [dbo].[PermissionRoleModuleDef] ON 
GO
INSERT [dbo].[PermissionRoleModuleDef] ([Id], [ModuleDefId], [PermissionId]) VALUES (1, 5001, 500100)
GO
SET IDENTITY_INSERT [dbo].[PermissionRoleModuleDef] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 
GO
INSERT [dbo].[Role] ([Id], [Name]) VALUES (1, N'مدیر سایت')
GO
INSERT [dbo].[Role] ([Id], [Name]) VALUES (2, N'کاربران شناخته شده')
GO
INSERT [dbo].[Role] ([Id], [Name]) VALUES (3, N'تمام کاربران')
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [UserPass], [Email], [UserName], [IsSuperAdmin], [IsLocked], [LastLoginDate], [LastPasswordChangeDate], [LastModifierUserId], [LastModificationDateTime], [FailedPasswordAttemptCount], [Mobile], [HasChangedPassword], [JoinDate], [JoinIP], [LastIP], [EmailConfirmed], [MobileConfirmed], [Picture], [IsDeleted], [DeletedDateTime], [DeleterUserId], [SentCode], [SentCodeExpirationDateTime], [CreditCardNumber]) VALUES (2, N'Ali', N'Azimi', N'10000.yzQqH/wWGVXc07FTg42/8w==.30mcxLT2dKcuw01tv6+Zh1xbEQI6yAkaPdb7DRXeurQ=', N'Mirazimi@palizct.com', N'Admin', 1, 0, NULL, NULL, NULL, NULL, 0, N'09371806342', 0, CAST(N'2020-09-12T00:00:00.000' AS DateTime), N'127.0.0.1', NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRole] ON 
GO
INSERT [dbo].[UserRole] ([Id], [RoleId], [UserId]) VALUES (1, 1, 2)
GO
SET IDENTITY_INSERT [dbo].[UserRole] OFF
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_t_User_IsSuperAdmin]  DEFAULT ((0)) FOR [IsSuperAdmin]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_t_User_IsLocked]  DEFAULT ((0)) FOR [IsLocked]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_HasChangedPassword]  DEFAULT ((0)) FOR [HasChangedPassword]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_t_User_JoinDate]  DEFAULT (getdate()) FOR [JoinDate]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_t_User_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[ModuleDefSetting]  WITH CHECK ADD  CONSTRAINT [FK_ModuleDefSetting_ModuleDef1] FOREIGN KEY([ModuleDefId])
REFERENCES [dbo].[ModuleDef] ([ModuleDefId])
GO
ALTER TABLE [dbo].[ModuleDefSetting] CHECK CONSTRAINT [FK_ModuleDefSetting_ModuleDef1]
GO
ALTER TABLE [dbo].[ModuleRole]  WITH CHECK ADD  CONSTRAINT [FK_ModuleRole_PageModule] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[PageModule] ([ModuleId])
GO
ALTER TABLE [dbo].[ModuleRole] CHECK CONSTRAINT [FK_ModuleRole_PageModule]
GO
ALTER TABLE [dbo].[ModuleRole]  WITH CHECK ADD  CONSTRAINT [FK_ModuleRole_PermissionRoleBase] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[PermissionRoleBase] ([PermissionId])
GO
ALTER TABLE [dbo].[ModuleRole] CHECK CONSTRAINT [FK_ModuleRole_PermissionRoleBase]
GO
ALTER TABLE [dbo].[ModuleRole]  WITH CHECK ADD  CONSTRAINT [FK_ModuleRole_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[ModuleRole] CHECK CONSTRAINT [FK_ModuleRole_Role]
GO
ALTER TABLE [dbo].[ModuleSetting]  WITH CHECK ADD  CONSTRAINT [FK_ModuleSetting_ModuleDefSetting] FOREIGN KEY([SettingId])
REFERENCES [dbo].[ModuleDefSetting] ([SettingId])
GO
ALTER TABLE [dbo].[ModuleSetting] CHECK CONSTRAINT [FK_ModuleSetting_ModuleDefSetting]
GO
ALTER TABLE [dbo].[ModuleSetting]  WITH CHECK ADD  CONSTRAINT [FK_ModuleSetting_PageModule] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[PageModule] ([ModuleId])
GO
ALTER TABLE [dbo].[ModuleSetting] CHECK CONSTRAINT [FK_ModuleSetting_PageModule]
GO
ALTER TABLE [dbo].[Page]  WITH CHECK ADD  CONSTRAINT [FK_Pages_Pages] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Page] ([Id])
GO
ALTER TABLE [dbo].[Page] CHECK CONSTRAINT [FK_Pages_Pages]
GO
ALTER TABLE [dbo].[PageModule]  WITH CHECK ADD  CONSTRAINT [FK_PageModule_ModuleDef] FOREIGN KEY([ModuleDefId])
REFERENCES [dbo].[ModuleDef] ([ModuleDefId])
GO
ALTER TABLE [dbo].[PageModule] CHECK CONSTRAINT [FK_PageModule_ModuleDef]
GO
ALTER TABLE [dbo].[PageModule]  WITH CHECK ADD  CONSTRAINT [FK_PageModule_Pages] FOREIGN KEY([PageId])
REFERENCES [dbo].[Page] ([Id])
GO
ALTER TABLE [dbo].[PageModule] CHECK CONSTRAINT [FK_PageModule_Pages]
GO
ALTER TABLE [dbo].[PageRole]  WITH CHECK ADD  CONSTRAINT [FK_PageRole_Page] FOREIGN KEY([PageId])
REFERENCES [dbo].[Page] ([Id])
GO
ALTER TABLE [dbo].[PageRole] CHECK CONSTRAINT [FK_PageRole_Page]
GO
ALTER TABLE [dbo].[PageRole]  WITH CHECK ADD  CONSTRAINT [FK_PageRole_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[PageRole] CHECK CONSTRAINT [FK_PageRole_Role]
GO
ALTER TABLE [dbo].[PermissionRoleModuleDef]  WITH CHECK ADD  CONSTRAINT [FK_PermissionRoleModuleDef_ModuleDef] FOREIGN KEY([ModuleDefId])
REFERENCES [dbo].[ModuleDef] ([ModuleDefId])
GO
ALTER TABLE [dbo].[PermissionRoleModuleDef] CHECK CONSTRAINT [FK_PermissionRoleModuleDef_ModuleDef]
GO
ALTER TABLE [dbo].[PermissionRoleModuleDef]  WITH CHECK ADD  CONSTRAINT [FK_PermissionRoleModuleDef_PermissionRoleBase] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[PermissionRoleBase] ([PermissionId])
GO
ALTER TABLE [dbo].[PermissionRoleModuleDef] CHECK CONSTRAINT [FK_PermissionRoleModuleDef_PermissionRoleBase]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Role]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_User]
GO
