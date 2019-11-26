CREATE TABLE [dbo].[Course] (
    [Id]                 UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Name]               NVARCHAR (64)    NOT NULL,
    [Description]        NVARCHAR (4000)  NULL,
    [IsNew]              BIT              NOT NULL,
    [TypeCode]           INT              NOT NULL,
    [PlanningMethodCode] INT              NOT NULL,
    [CourseGroupId]      UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_dbo.Course] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Course_dbo.CourseGroup_CourseGroupId] FOREIGN KEY ([CourseGroupId]) REFERENCES [dbo].[CourseGroup] ([Id]) ON DELETE CASCADE
);








GO
CREATE NONCLUSTERED INDEX [IX_CourseGroupId]
    ON [dbo].[Course]([CourseGroupId] ASC);

