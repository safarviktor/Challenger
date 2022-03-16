
drop table if exists clg.[Skip]
drop table if exists clg.[SkipType]
go


CREATE TABLE clg.[SkipType]
(
	Id int not null
		constraint PK_SkipType PRIMARY KEY CLUSTERED,
	[Name] varchar(100) not null
)
go

insert into clg.SkipType
(Id, [name])
select 1, 'Sick'
go

CREATE TABLE clg.[Skip]
(
	Id int Identity(1,1) not null
		constraint PK_Skip PRIMARY KEY CLUSTERED,
	ChallengeId int not null
		constraint FK_Skip_Challenge FOREIGN KEY REFERENCES clg.Challenge(Id),
	[Date] Date not null,
	SkipType int not null
		constraint FK_Skip_SkipType FOREIGN KEY REFERENCES clg.SkipType (Id),
	Comment nvarchar(255) null,
	DateTimeCreated DATETIME NOT NULL
		CONSTRAINT DF_Skip_DateTimeCreated DEFAULT GETDATE()
)