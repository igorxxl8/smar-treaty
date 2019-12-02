CREATE TABLE [dbo].[User] (
    [Id]                  UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Login]               NVARCHAR (128)   NOT NULL,
    [PasswordHash]        NVARCHAR (128)   NOT NULL,
    [PasswordSalt]        NVARCHAR (128)   NOT NULL,
    [FirstName]			  NVARCHAR (128)   NOT NULL,
    [LastName]			  NVARCHAR (128)   NOT NULL,
	[MiddleName]		  NVARCHAR (128)   NOT NULL,
    [RegistrationDate]    DATE             NOT NULL,
	[Wallet]              NVARCHAR (256)   NULL,
    CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED ([Id] ASC)
);














GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Login]
    ON [dbo].[User]([Login] ASC);

