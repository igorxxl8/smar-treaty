CREATE TABLE [dbo].[RoleUser] (
    [Role_Id] INT              NOT NULL,
    [User_Id] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_dbo.RoleUser] PRIMARY KEY CLUSTERED ([Role_Id] ASC, [User_Id] ASC),
    CONSTRAINT [FK_dbo.RoleUser_dbo.Role_Role_Id] FOREIGN KEY ([Role_Id]) REFERENCES [dbo].[Role] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.RoleUser_dbo.User_User_Id] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_User_Id]
    ON [dbo].[RoleUser]([User_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Role_Id]
    ON [dbo].[RoleUser]([Role_Id] ASC);

