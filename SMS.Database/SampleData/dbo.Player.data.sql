SET IDENTITY_INSERT [dbo].[Player] ON
INSERT INTO [dbo].[Player] ([Id], [Firstname], [Lastname], [Email], [PhoneNumber], [IsActivePlayer]) VALUES (1, N'John', N'Smith', N'J@smith.com', N'123-456-789', 1)
INSERT INTO [dbo].[Player] ([Id], [Firstname], [Lastname], [Email], [PhoneNumber], [IsActivePlayer]) VALUES (2, N'Peter', N'Parker', N'P@parker.com', N'987-654-321', 0)
INSERT INTO [dbo].[Player] ([Id], [Firstname], [Lastname], [Email], [PhoneNumber], [IsActivePlayer]) VALUES (3, N'Ben', N'Stokes', N'Stokey@lbw.com', N'222-222-222', 1)
INSERT INTO [dbo].[Player] ([Id], [Firstname], [Lastname], [Email], [PhoneNumber], [IsActivePlayer]) VALUES (4, N'Harry', N'Kane', N'Harry@spurs.com', N'333-333-333', 1)
SET IDENTITY_INSERT [dbo].[Player] OFF
