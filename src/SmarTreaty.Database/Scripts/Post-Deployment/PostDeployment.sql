/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
:r .\dbo.Role.Table.sql
go

:r .\dbo.User.Table.sql
go

:r .\dbo.RoleUser.Table.sql
go

:r .\dbo.TrainerGroup.Table.sql
go

:r .\dbo.Trainer.Table.sql
go

:r .\dbo.CourseGroup.Table.sql
go

:r .\dbo.Course.Table.sql
go

:r .\dbo.TrainerCourse.Table.sql
go
