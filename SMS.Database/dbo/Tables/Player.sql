CREATE TABLE [dbo].[Player] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Firstname]      NVARCHAR (MAX) NOT NULL,
    [Lastname]       NVARCHAR (MAX) NOT NULL,
    [Email]          NVARCHAR (MAX) NOT NULL,
    [PhoneNumber]    NVARCHAR (50)  NULL,
    [IsActivePlayer] BIT            DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

