-- Create the player table
CREATE TABLE [dbo].[Player] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Firstname]      NVARCHAR (MAX) NOT NULL,
    [Lastname]       NVARCHAR (MAX) NOT NULL,
    [Email]          NVARCHAR (MAX) NOT NULL,
    [PhoneNumber]    NVARCHAR (50)  NULL,
    [IsActivePlayer] BIT            DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO

-- Create the Fixture table
CREATE TABLE [dbo].[Fixture] (
    [Id]                      INT            IDENTITY (1, 1) NOT NULL,
    [Opponent]                NVARCHAR (MAX) NOT NULL,
    [DateOfFixture]           DATETIME       NOT NULL,
    [Venue]                   NVARCHAR (10)  NOT NULL,
    [StartTime]               NVARCHAR (5)   NOT NULL,
    [Postcode]                NVARCHAR (10)  NULL,
    [NumberOfPlayersRequired] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO

-- Create the availability table
CREATE TABLE [dbo].[Availability] (
    [Id]        INT IDENTITY (1, 1) NOT NULL,
    [FixtureId] INT NOT NULL,
    [PlayerId]  INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Availability_Fixture] FOREIGN KEY ([FixtureId]) REFERENCES [dbo].[Fixture] ([Id]),
    CONSTRAINT [FK_Availability_Player] FOREIGN KEY ([PlayerId]) REFERENCES [dbo].[Player] ([Id])
);

GO

-------------------------------------------------------------------------------

-- Insert dummy data
SET IDENTITY_INSERT [dbo].[Player] ON
INSERT INTO [dbo].[Player] ([Id], [Firstname], [Lastname], [Email], [PhoneNumber], [IsActivePlayer]) VALUES (1, N'John', N'Smith', N'J@smith.com', N'123-456-789', 1)
INSERT INTO [dbo].[Player] ([Id], [Firstname], [Lastname], [Email], [PhoneNumber], [IsActivePlayer]) VALUES (2, N'Peter', N'Parker', N'P@parker.com', N'987-654-321', 0)
INSERT INTO [dbo].[Player] ([Id], [Firstname], [Lastname], [Email], [PhoneNumber], [IsActivePlayer]) VALUES (3, N'Ben', N'Stokes', N'Stokey@lbw.com', N'222-222-222', 1)
INSERT INTO [dbo].[Player] ([Id], [Firstname], [Lastname], [Email], [PhoneNumber], [IsActivePlayer]) VALUES (4, N'Harry', N'Kane', N'Harry@spurs.com', N'333-333-333', 1)
SET IDENTITY_INSERT [dbo].[Player] OFF

GO

SET IDENTITY_INSERT [dbo].[Fixture] ON
INSERT INTO [dbo].[Fixture] ([Id], [Opponent], [DateOfFixture], [Venue], [StartTime], [Postcode], [NumberOfPlayersRequired]) VALUES (1, N'Southampton FC', N'2023-01-23 00:00:00', N'Away', N'15:00', N'postcode 1', 11)
INSERT INTO [dbo].[Fixture] ([Id], [Opponent], [DateOfFixture], [Venue], [StartTime], [Postcode], [NumberOfPlayersRequired]) VALUES (2, N'Liverpool', N'2023-01-24 00:00:00', N'Home', N'15:00', N'n/a', 11)
INSERT INTO [dbo].[Fixture] ([Id], [Opponent], [DateOfFixture], [Venue], [StartTime], [Postcode], [NumberOfPlayersRequired]) VALUES (3, N'Manchester City', N'2023-01-25 00:00:00', N'Away', N'15:00', N'postcode 2', 11)
INSERT INTO [dbo].[Fixture] ([Id], [Opponent], [DateOfFixture], [Venue], [StartTime], [Postcode], [NumberOfPlayersRequired]) VALUES (4, N'West Ham', N'2023-01-26 00:00:00', N'Home', N'17:00', N'n/a', 11)
SET IDENTITY_INSERT [dbo].[Fixture] OFF

GO

SET IDENTITY_INSERT [dbo].[Availability] ON
INSERT INTO [dbo].[Availability] ([Id], [FixtureId], [PlayerId]) VALUES (1, 1, 1)
INSERT INTO [dbo].[Availability] ([Id], [FixtureId], [PlayerId]) VALUES (2, 1, 2)
INSERT INTO [dbo].[Availability] ([Id], [FixtureId], [PlayerId]) VALUES (3, 1, 3)
INSERT INTO [dbo].[Availability] ([Id], [FixtureId], [PlayerId]) VALUES (4, 1, 4)
INSERT INTO [dbo].[Availability] ([Id], [FixtureId], [PlayerId]) VALUES (5, 2, 2)
INSERT INTO [dbo].[Availability] ([Id], [FixtureId], [PlayerId]) VALUES (6, 2, 4)
INSERT INTO [dbo].[Availability] ([Id], [FixtureId], [PlayerId]) VALUES (7, 3, 1)
INSERT INTO [dbo].[Availability] ([Id], [FixtureId], [PlayerId]) VALUES (8, 3, 2)
INSERT INTO [dbo].[Availability] ([Id], [FixtureId], [PlayerId]) VALUES (9, 3, 3)
SET IDENTITY_INSERT [dbo].[Availability] OFF

