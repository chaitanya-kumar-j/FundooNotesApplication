create table UsersNotesInfo
(
UserId int foreign key references UserInfo(UserId),
NoteId int primary key not null,
NoteTitle Nvarchar(50),
NoteBody ntext,
NoteColour Nvarchar(50)  default 'White',
Label Nvarchar(50) default null,
Collaborations Nvarchar(50) default null,
Reminder datetime default null,
IsArchive bit default 0,
IsPin bit default 0,
IsTrash bit default 0,
CreatedDateTime datetime,
LastModifiedDateTime datetime
)

select * from UsersNotesInfo

--**************************************************************************
--*                          STORED PROCEDURES                             *
--**************************************************************************
-- procedure to view all Notes details
create procedure spViewAllNotes
(@UserId int)
as 
begin
select * from UsersNotesInfo where UserId = @UserId
end

--**************************************************************************
-- procedure to create note
alter procedure spCreateNote
(
@UserId int,
@NoteTitle Nvarchar(50),
@NoteBody ntext,
@NoteColour Nvarchar(50),
@Label Nvarchar(50),
@Collaborations Nvarchar(50),
@IsArchive bit,
@IsPin bit,
@Reminder datetime
)
as
begin
declare @noteCount int, @NoteTitleCount int
select @noteCount = max(NoteId) from UsersNotesInfo where USERID = @UserId
if (@noteCount = null)
	begin
	set @noteCount = 0
	end
select @NoteTitleCount = count(NoteId) from UsersNotesInfo where (USERID = @UserId and NoteTitle = @NoteTitle)

if (@NoteTitleCount <> 0)
	begin
	Raiserror('Note with same name already exists.',16,1)
	end
else
	begin
	insert into UsersNotesInfo values(@UserId, @noteCount+1, @NoteTitle, @NoteBody, @NoteColour, @Label, @Collaborations, @Reminder, @IsArchive, @IsPin, 0, SYSDATETIME() , SYSDATETIME() )
	select * from UsersNotesInfo where UserId = @UserId and NoteId = @noteCount+1
	end
end


exec spCreateNote 4,'A','asdfg',null,null,null,null,null,null
--**************************************************************************
-- procedure to View note
alter procedure spViewNote
(@UserId int, @NoteId int)
as
begin
	declare @count int 
	select @count = count(NoteId) from UsersNotesInfo where UserId = @UserId and NoteId = @NoteId and IsTrash = 0
	if (@count <> 1)
		begin
		Raiserror('No Note with given NoteId.',16,1)
		end
	else
		begin
		select * from UsersNotesInfo where UserId = @UserId and NoteId = @NoteId
		end
end

--**************************************************************************
-- procedure to Edit note
alter procedure spEditNote
(@UserId int, @NoteId int, @NoteBody ntext)
as
begin
	declare @count int 
	select @count = count(NoteId) from UsersNotesInfo where UserId = @UserId and NoteId = @NoteId
	if (@count <> 1)
		begin
		Raiserror('No Note with given NoteId.',16,1)
		end
	else
		begin
		update UsersNotesInfo set NoteBody = @NoteBody where UserId = @UserId and NoteId = @NoteId
		declare @datetime datetime
		set @datetime = SYSDATETIME()
		select @datetime
		update UsersNotesInfo set LastModifiedDateTime =  @datetime  where UserId = 4 and NoteId = 2
		select * from UsersNotesInfo where UserId = @UserId and NoteId = @NoteId
		end
end

--**************************************************************************
-- procedure to Move Into Or Move From Trash note
create procedure spDeleteOrRestoreNote
(@UserId int, @NoteId int)
as
begin
	declare @count int 
	select @count = count(NoteId) from UsersNotesInfo where UserId = @UserId and NoteId = @NoteId
	if (@count <> 1)
		begin
		Raiserror('No Note with given NoteId.',16,1)
		end
	else
		begin
		update UsersNotesInfo set IsTrash = IsTrash^1 where UserId = @UserId and NoteId = @NoteId
		update UsersNotesInfo set LastModifiedDateTime = GETDATE() where UserId = @UserId and NoteId = @NoteId
		select * from UsersNotesInfo where UserId = @UserId and NoteId = @NoteId
		end
end

--**************************************************************************
-- procedure to Change colour of note
create procedure spChangeColourOfNote
(@UserId int, @NoteId int, @NoteColour Nvarchar(50))
as
begin
	declare @count int 
	select @count = count(NoteId) from UsersNotesInfo where UserId = @UserId and NoteId = @NoteId
	if (@count <> 1)
		begin
		Raiserror('No Note with given NoteId.',16,1)
		end
	else
		begin
		update UsersNotesInfo set NoteColour = @NoteColour where UserId = @UserId and NoteId = @NoteId
		update UsersNotesInfo set LastModifiedDateTime = GETDATE() where UserId = @UserId and NoteId = @NoteId
		select * from UsersNotesInfo where UserId = @UserId and NoteId = @NoteId
		end
end


--**************************************************************************
-- procedure to set reminder to note
create procedure spSetReminderToNote
(@UserId int, @NoteId int, @Reminder datetime)
as
begin
	declare @count int 
	select @count = count(NoteId) from UsersNotesInfo where UserId = @UserId and NoteId = @NoteId
	if (@count <> 1)
		begin
		Raiserror('No Note with given NoteId.',16,1)
		end
	else
		begin
		update UsersNotesInfo set Reminder = @Reminder where UserId = @UserId and NoteId = @NoteId
		update UsersNotesInfo set LastModifiedDateTime = GETDATE() where UserId = @UserId and NoteId = @NoteId
		select * from UsersNotesInfo where UserId = @UserId and NoteId = @NoteId
		end
end


--**************************************************************************
-- procedure to Pin/Unpin Note
alter procedure spPinOrUnPin
(@UserId int, @NoteId int)
as
begin
	declare @count int 
	select @count = count(NoteId) from UsersNotesInfo where UserId = @UserId and NoteId = @NoteId
	if (@count <> 1)
		begin
		Raiserror('No Note with given NoteId.',16,1)
		end
	else
		begin
		update UsersNotesInfo set IsPin = IsPin^1 where UserId = @UserId and NoteId = @NoteId
		update UsersNotesInfo set LastModifiedDateTime = GETDATE() where UserId = @UserId and NoteId = @NoteId
		select * from UsersNotesInfo where UserId = @UserId and NoteId = @NoteId
		end
end

--**************************************************************************
-- procedure to Archive/UnArchive Note
create procedure spArchiveOrUnArchive
(@UserId int, @NoteId int)
as
begin
	declare @count int
	select @count = count(NoteId) from UsersNotesInfo where UserId = @UserId and NoteId = @NoteId
	if (@count <> 1)
		begin
		Raiserror('No Note with given NoteId.',16,1)
		end
	else
		begin
		update UsersNotesInfo set IsArchive = IsArchive^1 where UserId = @UserId and NoteId = @NoteId
		update UsersNotesInfo set LastModifiedDateTime = GETDATE() where UserId = @UserId and NoteId = @NoteId
		select * from UsersNotesInfo where UserId = @UserId and NoteId = @NoteId
		end
end

--**************************************************************************

-- procedure to Move Into Or Move From Trash note
create procedure spAddOrRemoveLabel
(@UserId int, @NoteId int, @Label Nvarchar(50))
as
begin
	declare @count int
	select @count = count(NoteId) from UsersNotesInfo where UserId = @UserId and NoteId = @NoteId
	if (@count <> 1)
		begin
		Raiserror('No Note with given NoteId.',16,1)
		end
	else
		begin
		update UsersNotesInfo set IsTrash = IsTrash^1 where UserId = @UserId and NoteId = @NoteId
		update UsersNotesInfo set LastModifiedDateTime = GETDATE() where UserId = @UserId and NoteId = @NoteId
		select * from UsersNotesInfo where UserId = @UserId and NoteId = @NoteId
		end
end

--**************************************************************************
