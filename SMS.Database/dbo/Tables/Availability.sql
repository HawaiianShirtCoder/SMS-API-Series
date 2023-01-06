CREATE TABLE [dbo].[Availability] (
    [Id]        INT IDENTITY (1, 1) NOT NULL,
    [FixtureId] INT NOT NULL,
    [PlayerId]  INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Availability_Fixture] FOREIGN KEY ([FixtureId]) REFERENCES [dbo].[Fixture] ([Id]),
    CONSTRAINT [FK_Availability_Player] FOREIGN KEY ([PlayerId]) REFERENCES [dbo].[Player] ([Id])
);

