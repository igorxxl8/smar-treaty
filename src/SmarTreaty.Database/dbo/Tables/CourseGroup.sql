CREATE TABLE [dbo].[CourseGroup] (
    [Id]   UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Name] NVARCHAR (32)    NOT NULL,
    CONSTRAINT [PK_dbo.CourseGroup] PRIMARY KEY CLUSTERED ([Id] ASC)
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Name]
    ON [dbo].[CourseGroup]([Name] ASC);

