CREATE TABLE [dbo].[TrainerCourse] (
    [Course_Id]  UNIQUEIDENTIFIER NOT NULL,
    [Trainer_Id] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_dbo.TrainerCourse] PRIMARY KEY CLUSTERED ([Course_Id] ASC, [Trainer_Id] ASC),
    CONSTRAINT [FK_dbo.TrainerCourse_dbo.Course_Course_Id] FOREIGN KEY ([Course_Id]) REFERENCES [dbo].[Course] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.TrainerCourse_dbo.Trainer_Trainer_Id] FOREIGN KEY ([Trainer_Id]) REFERENCES [dbo].[Trainer] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_Course_Id]
    ON [dbo].[TrainerCourse]([Course_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Trainer_Id]
    ON [dbo].[TrainerCourse]([Trainer_Id] ASC);

