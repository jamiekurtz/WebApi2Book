CREATE TABLE [dbo].[TaskUser]
(
[TaskId] bigint NOT NULL,
[UserId] bigint not null,
[ts] rowversion not null,
primary key (TaskId, UserId),
foreign key (UserId) references dbo.[User] (UserId),
foreign key (TaskId) references dbo.Task (TaskId)
)
go
create index ix_TaskUser_UserId on TaskUser(UserId)
go