CREATE TABLE [dbo].[SmartContract]
(
	[Id] UNIQUEIDENTIFIER NOT NULL, 
    [Abi] NVARCHAR(MAX) NOT NULL, 
    [Bytecode] NVARCHAR(MAX) NOT NULL, 
    [Name] NCHAR(50) NOT NULL, 
    [User_Id] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [PK_SmartContract] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_SmartContract_ToTable_User] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[User]([Id])
)
