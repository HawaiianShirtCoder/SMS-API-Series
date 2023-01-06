SET IDENTITY_INSERT [dbo].[Fixture] ON
INSERT INTO [dbo].[Fixture] ([Id], [Opponent], [DateOfFixture], [Venue], [StartTime], [Postcode], [NumberOfPlayersRequired]) VALUES (1, N'Southampton FC', N'2023-01-23 00:00:00', N'Away', N'15:00', N'postcode 1', 11)
INSERT INTO [dbo].[Fixture] ([Id], [Opponent], [DateOfFixture], [Venue], [StartTime], [Postcode], [NumberOfPlayersRequired]) VALUES (2, N'Liverpool', N'2023-01-24 00:00:00', N'Home', N'15:00', N'n/a', 11)
INSERT INTO [dbo].[Fixture] ([Id], [Opponent], [DateOfFixture], [Venue], [StartTime], [Postcode], [NumberOfPlayersRequired]) VALUES (3, N'Manchester City', N'2023-01-25 00:00:00', N'Away', N'15:00', N'postcode 2', 11)
INSERT INTO [dbo].[Fixture] ([Id], [Opponent], [DateOfFixture], [Venue], [StartTime], [Postcode], [NumberOfPlayersRequired]) VALUES (4, N'West Ham', N'2023-01-26 00:00:00', N'Home', N'17:00', N'n/a', 11)
SET IDENTITY_INSERT [dbo].[Fixture] OFF
