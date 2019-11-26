CREATE TABLE [dbo].[User] (
    [Id]           UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Email]        NVARCHAR (128)   NOT NULL,
    [PasswordHash] NVARCHAR (128)   NOT NULL,
    [PasswordSalt] NVARCHAR (128)   NOT NULL,
    [FirstName]    NVARCHAR (128)   NOT NULL,
    [LastName]     NVARCHAR (128)   NOT NULL,
    [StartDate]    DATE             NOT NULL,
    [EndDate]      DATE             NULL,
    [Department]   NVARCHAR (128)   NULL,
    [Location]     NVARCHAR (128)   NULL,
    [Position]     NVARCHAR (128)   NULL,
    [Photo]        VARBINARY (MAX)  NULL,
    CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED ([Id] ASC)
);














GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Email]
    ON [dbo].[User]([Email] ASC);

