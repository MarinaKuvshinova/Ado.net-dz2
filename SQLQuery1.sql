create database Sales
go
use Sales

create table Shoppers(
	IDShopper int not null primary key identity,
	FirstName nvarchar(20) not null,
	LastName nvarchar(20) not null
)
insert into Shoppers
values
	('Иванов','Иван'),
	('Сидоров','Семен')


create table Sellers(
	IDSeller int not null primary key identity,
	FirstName nvarchar(20) not null,
	LastName nvarchar(20) not null
)
insert into Sellers
values
	('Петров','Петр')

create table Sales(
	IDSales int not null primary key identity,
	IDShopper int not null foreign key references Shoppers(IDShopper),
	IDSeller int not null foreign key references Sellers(IDSeller),
	Price money not null,
	[Date] date not null default getdate()
)
insert into Sales
values
	(1,1,2500,'2018-10-10'),
	(2,1,1500,'2018-10-11')
