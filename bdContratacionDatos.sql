USE [bdContratacion]
GO
SET IDENTITY_INSERT [dbo].[Menus] ON 

INSERT [dbo].[Menus] ([MenuID], [DescripcionMenu], [Controlador], [Accion], [Ordenamiento]) VALUES (1, N'Inicio', N'Home', N'Index', 1)
INSERT [dbo].[Menus] ([MenuID], [DescripcionMenu], [Controlador], [Accion], [Ordenamiento]) VALUES (2, N'Acerca de', N'Home', N'About', 2)
INSERT [dbo].[Menus] ([MenuID], [DescripcionMenu], [Controlador], [Accion], [Ordenamiento]) VALUES (3, N'Contacto', N'Home', N'Contact', 3)
INSERT [dbo].[Menus] ([MenuID], [DescripcionMenu], [Controlador], [Accion], [Ordenamiento]) VALUES (4, N'Maestros', N'Maestros', N'Maestros', 4)
INSERT [dbo].[Menus] ([MenuID], [DescripcionMenu], [Controlador], [Accion], [Ordenamiento]) VALUES (5, N'Administración', N'Administracion', N'Administracion', 5)
SET IDENTITY_INSERT [dbo].[Menus] OFF
SET IDENTITY_INSERT [dbo].[SubMenuOperaciones] ON 

INSERT [dbo].[SubMenuOperaciones] ([SubMenuOperacionID], [MenuID], [EsSubMenu], [DescripcionOperacion], [Controlador], [Accion], [OrdenamientoSubMenu]) VALUES (1, 4, 1, N'Cargos', N'Cargos', N'Index', 1)
INSERT [dbo].[SubMenuOperaciones] ([SubMenuOperacionID], [MenuID], [EsSubMenu], [DescripcionOperacion], [Controlador], [Accion], [OrdenamientoSubMenu]) VALUES (2, 4, 1, N'Tipo Documentos', N'TipoDocumentos', N'Index', 2)
INSERT [dbo].[SubMenuOperaciones] ([SubMenuOperacionID], [MenuID], [EsSubMenu], [DescripcionOperacion], [Controlador], [Accion], [OrdenamientoSubMenu]) VALUES (3, 5, 1, N'Usuarios', N'Usuarios', N'Index', 1)
INSERT [dbo].[SubMenuOperaciones] ([SubMenuOperacionID], [MenuID], [EsSubMenu], [DescripcionOperacion], [Controlador], [Accion], [OrdenamientoSubMenu]) VALUES (4, 5, 1, N'Barra de Menú', N'Menus', N'Index', 2)
INSERT [dbo].[SubMenuOperaciones] ([SubMenuOperacionID], [MenuID], [EsSubMenu], [DescripcionOperacion], [Controlador], [Accion], [OrdenamientoSubMenu]) VALUES (5, 5, 1, N'Funciones o Sub Menú', N'SubMenuOperaciones', N'Index', 3)
SET IDENTITY_INSERT [dbo].[SubMenuOperaciones] OFF
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1', N'Administrador')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2', N'Empleados')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3', N'Invitados')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'a627b1a1-37ee-4cb0-b7e4-f954e57aee14', N'Dar123@hotmail.com', 0, N'ABrDRiM/dS1ThgBWbK3LSEHXYrRrZTGXMBO5UmdHNg9uEjL8TJRNRH0+PdnzayL6kg==', N'b2465ca3-6bb9-4020-892b-02c60a15328f', NULL, 0, 0, NULL, 1, 0, N'Dar123@hotmail.com')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a627b1a1-37ee-4cb0-b7e4-f954e57aee14', N'3')
