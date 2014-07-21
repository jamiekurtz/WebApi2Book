if not exists(select * from dbo.Status where Name = 'Not Started')
insert into dbo.Status(Name, Ordinal) values('Not Started', 0);
if not exists(select * from dbo.Status where Name = 'In Progress')
insert into dbo.Status(Name, Ordinal) values('In Progress', 1);
if not exists(select * from dbo.Status where Name = 'Completed')
insert into dbo.Status(Name, Ordinal) values('Completed', 2);