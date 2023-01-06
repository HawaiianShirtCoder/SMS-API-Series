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

