create database FundooNotesAppDB

use FundooNotesAppDB

--*************************************************************************

-- Table to store users personal data
create table UserInfo
(
UserId int primary key not null identity(1,1),  
FirstName Nvarchar(50),  
LastName Nvarchar(50),  
City Nvarchar(50), 
Mobilenumber varchar(10) unique, 
Email Nvarchar(50) unique,  
Password Nvarchar(50),
RegistrationDateTime datetime
)

select * from UserInfo
--*************************************************************************

-- Table to store types of transactions user can do
create table TransactionTypesInfo
(
TransactionTypeId int primary key not null identity(1,1),
TransactionType Nvarchar(50) not null unique
)

insert into TransactionTypesInfo values ('Login')
insert into TransactionTypesInfo values ('ResetPassword')
insert into TransactionTypesInfo values ('CreateNotes')
insert into TransactionTypesInfo values ('EditNotes')
insert into TransactionTypesInfo values ('DeleteNotes')


select * from TransactionTypesInfo
--**************************************************************************

-- Table to store transaction details of each user
create table TransactionsInfo
(
TransactionId int primary key not null identity(1,1),
UserId int foreign key references UserInfo(UserId),
TransactionTypeId int foreign key references TransactionTypesInfo(TransactionTypeId),
TransactionTime datetime
)

select * from TransactionsInfo

--**************************************************************************
--*                          STORED PROCEDURES                             *
--**************************************************************************
-- procedure to view all users in usersinfo table
create procedure spViewAllUsers
as 
begin
select * from UserInfo
end

--**************************************************************************
-- procedure to register a user
create procedure spRegistration
(@FirstName Nvarchar(50),  
@LastName Nvarchar(50),  
@City Nvarchar(50), 
@Mobilenumber varchar(10), 
@Email Nvarchar(50),  
@Password Nvarchar(50))
as
begin
declare @emailCount int, @mobileCount int
select @emailCount = count(UserId) from UserInfo where Email = @Email
select @mobileCount = count(UserId) from UserInfo where Mobilenumber = @Mobilenumber
if (@emailCount<>0)
   begin
   Raiserror('Email Id already registered with another user id.',16,1)
   end
else
	if (@mobileCount<>0)
		begin
		Raiserror('Mobile number already registered with another user id.',16,1)
		end
	else
		begin
		Insert into UserInfo values (@FirstName, @LastName, @City, @Mobilenumber, @Email, @Password, GETDATE())
		select * from UserInfo where Email = @Email
		end
end

exec spRegistration 'ChaitanyaKumar','Jinka','Proddatur','9398459597','chaitanyakumar.jinka@gmail.com','Chaitanya@123'
--**************************************************************************
-- procedure to login
create procedure spUserLogin
(@Email Nvarchar(50), @Password Nvarchar(50))
as
begin
declare @userCount int
select @userCount = count(UserId) from UserInfo where Email = @Email

if (@userCount <>1)
	begin
	Raiserror('No User registered with given Email id.',16,1)
	end
else
	begin
	select * from UserInfo where Email = @Email and Password = @Password
	declare @userId int
	select @userId = UserId from UserInfo where Email = @Email
	insert into TransactionsInfo values (@userId,1,GETDATE())
	end
end

--**************************************************************************
-- procedure to reset password
alter procedure spResetPassword
(@UserId Nvarchar(50), @CurrentPassword Nvarchar(50), @NewPassword Nvarchar(50))
as
begin
	declare @userCount int
	select @userCount = count(UserId) from UserInfo where UserId = @UserId and Password = @CurrentPassword

	if (@userCount <>1)
		begin
		Raiserror('Entered wrong password in current password.',16,1)
		end
	else
		begin
		update UserInfo set Password = @NewPassword where UserId = @UserId and Password =@CurrentPassword
		select * from UserInfo where UserId = @UserId and Password = @NewPassword
		insert into TransactionsInfo values (@userId,2,GETDATE())
		end
end

