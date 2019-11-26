CREATE TABLE [dbo].[TrainerGroup] (
    [Id]   UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Name] NVARCHAR (32)    NOT NULL,
    CONSTRAINT [PK_dbo.TrainerGroup] PRIMARY KEY CLUSTERED ([Id] ASC)
);












GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Name]
    ON [dbo].[TrainerGroup]([Name] ASC);



