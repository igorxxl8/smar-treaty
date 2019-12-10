CREATE TABLE [dbo].[Contract]
(
	[Id] UNIQUEIDENTIFIER NOT NULL, 
    [Address] NVARCHAR(50) NOT NULL, 
    [User_Id] UNIQUEIDENTIFIER NOT NULL, 
    [CreationDate] DATETIME NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_Contract] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Contract_ToTable_User] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[User]([Id])
)
