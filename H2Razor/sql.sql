
DROP TABLE IF EXISTS dbo.ToDo
GO
DROP TABLE IF EXISTS dbo.Prioritys
GO
DROP TABLE IF EXISTS dbo.StatusCompleted
GO

CREATE TABLE [ToDo] (
  [ToDoID] int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
  [GUID] nvarchar(50),
  [PriorityID] int,
  [DescText] varchar(25),--Description
  [CreatedTime] datetime,
  [StatusID] int ,
  [IsDeleted] bit 
)
GO

CREATE TABLE [Prioritys] (
  [PriorityID] int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
  [PriorityName] int --a int for the priority
)
GO

CREATE TABLE [StatusCompleted] (
  [StatusID] int PRIMARY KEY IDENTITY(1,1) NOT NULL,
  [Tilstand] int -- int to have as many status stages as wanted
)
GO

ALTER TABLE [ToDo] ADD FOREIGN KEY ([StatusID]) REFERENCES [StatusCompleted] ([StatusID])
GO

ALTER TABLE [ToDo] ADD FOREIGN KEY ([PriorityID]) REFERENCES [Prioritys] ([PriorityID])
GO

-- preset the prioritys
insert into Prioritys values(1)
insert into Prioritys values(0)
insert into Prioritys values(2)
GO
--preset status
insert into StatusCompleted values(0) -- active ToDo
insert into StatusCompleted values(1) -- Disabled ToDo
-- can add more later
GO

insert into todo(PriorityID, [GUID], DescText,CreatedTime,StatusID,IsDeleted) values(1,NEWID(), 'aa',2020-01-02,(SELECT StatusID From StatusCompleted WHERE Tilstand = 0),0)
GO

--CRUD

--Create
DROP PROCEDURE IF EXISTS dbo.spCreateToDo
GO
CREATE PROCEDURE spCreateToDo
@Description varchar(25),
@GUID nvarchar(50),
@CreatedTime datetime,
@prio int,
@Status int
AS
insert into ToDo([GUID], PriorityID, DescText, CreatedTime, StatusID, IsDeleted)
OUTPUT INSERTED.ToDoID
values(@GUID ,(SELECT PriorityID FROM Prioritys WHERE PriorityName = @prio),@Description,@CreatedTime,(SELECT StatusID FROM StatusCompleted WHERE Tilstand = @Status),0)
GO


--Update ToDo
DROP PROCEDURE IF EXISTS dbo.spUpdateToDo
GO
CREATE PROCEDURE spUpdateToDo
@ID int,
@Prio int,
@Desc varchar,
@Status int
AS
UPDATE ToDo
SET PriorityID = (SELECT PriorityID FROM Prioritys WHERE PriorityName = @Prio), DescText = @Desc, StatusID = (SELECT StatusID FROM StatusCompleted WHERE Tilstand = @Status)
WHERE ToDoID = @ID
GO


--delete
DROP PROCEDURE iF EXiSTS dbo.spDeleteToDo
GO
CREATE PROCEDURE spDeleteToDo
@ID int
AS
UPDATE ToDo
SET IsDeleted = 1
WHERE ToDoID = @ID
GO


--Complate todo
DROP PROCEDURE IF EXISTS dbo.spComplateToDo
GO
CREATE PROCEDURE spComplateToDo
@ToDoId int,
@Status int
AS
UPDATE ToDo
SET StatusID = @Status
WHERE ToDoID = @ToDoId
GO

-- Get all ToDo's

DROP PROCEDURE IF EXISTS dbo.spReadAllToDo
GO
CREATE PROCEDURE spReadAllToDo
AS
SELECT ToDoId, [GUID], DescText,CreatedTime,PriorityName,Tilstand
FROM ToDo
INNER JOIN Prioritys ON ToDo.PriorityID = Prioritys.PriorityID
INNER JOIN StatusCompleted ON ToDo.StatusID = StatusCompleted.StatusID
WHERE IsDeleted = 0
GO

--DROP VIEW IF EXISTS dbo.vwReadAllToDo
--GO
--CREATE VIEW vwReadAllToDo
--AS
--SELECT ToDoId, [GUID], DescText,CreatedTime,PriorityName,Tilstand
--FROM ToDo
--INNER JOIN Prioritys ON ToDo.PriorityID = Prioritys.PriorityID
--INNER JOIN StatusCompleted ON ToDo.StatusID = StatusCompleted.StatusID
--GO