CREATE TABLE [dbo].[SmartContract]
(
	[SmartContractID] UNIQUEIDENTIFIER NOT NULL, 
    [Abi] NVARCHAR(MAX) NOT NULL, 
    [Bytecode] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [PK_SmartContract] PRIMARY KEY CLUSTERED ([SmartContractID] ASC)
)
