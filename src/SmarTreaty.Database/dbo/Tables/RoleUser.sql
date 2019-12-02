CREATE TABLE [dbo].[RoleUser] (
    [RoleId] INT              NOT NULL,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_dbo.RoleUser] PRIMARY KEY CLUSTERED ([RoleId] ASC, [UserId] ASC),
    CONSTRAINT [FK_dbo.RoleUser_dbo.Role_Role_Id] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.RoleUser_dbo.User_User_Id] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_User_Id]
    ON [dbo].[RoleUser]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Role_Id]
    ON [dbo].[RoleUser]([RoleId] ASC);

