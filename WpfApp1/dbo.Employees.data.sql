SET IDENTITY_INSERT [dbo].[Employees] ON
INSERT INTO [dbo].[Employees] ([Id], [FIO], [id_department], [Salary]) VALUES (1328, N'Первый сотрудник ', 2, CAST(313512 AS Decimal(18, 0)))
INSERT INTO [dbo].[Employees] ([Id], [FIO], [id_department], [Salary]) VALUES (1329, N'Второй сотрудник', 1, CAST(3525 AS Decimal(18, 0)))
INSERT INTO [dbo].[Employees] ([Id], [FIO], [id_department], [Salary]) VALUES (1330, N'Третий сотрудник', 1, CAST(4322345 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[Employees] OFF
SET IDENTITY_INSERT [dbo].[Department] ON
INSERT INTO [dbo].[Department] ([Id], [Department]) VALUES (1, N'Первый департамент')
INSERT INTO [dbo].[Department] ([Id], [Department]) VALUES (1005, N'Второй департамент')
SET IDENTITY_INSERT [dbo].[Department] OFF