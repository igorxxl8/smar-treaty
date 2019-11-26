CREATE TABLE [dbo].[Trainer] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [Info]           NVARCHAR (4000)  NULL,
    [TrainerGroupId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_dbo.Trainer] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Trainer_dbo.TrainerGroup_TrainerGroupId] FOREIGN KEY ([TrainerGroupId]) REFERENCES [dbo].[TrainerGroup] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Trainer_dbo.User_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[User] ([Id])
);






GO
CREATE NONCLUSTERED INDEX [IX_TrainerGroupId]
    ON [dbo].[Trainer]([TrainerGroupId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[Trainer]([Id] ASC);

