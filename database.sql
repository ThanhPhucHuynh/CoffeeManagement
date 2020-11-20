create database QuanLyQuanCafe
go 

use QuanLyQuanCafe
go

-- Food
-- Table
-- FoodCategory
-- Account
-- Bill
-- BillInfo

CREATE TABLE TableFood
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
	status NVARCHAR(100) NOT NULL DEFAULT N'Trống'-- Trống || Có người
)

go
CREATE TABLE Account
(
	
	UserName NVARCHAR(100) PRIMARY KEY DEFAULT N'Kter',
	DisplayName NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
	Password NVARCHAR(100) NOT NULL DEFAULT N'0',
	Type INT NOT NULL DEFAULT 0 -- 1. Admim || 0. Staff
)
go
CREATE TABLE FoodCategory
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên'
)
go

CREATE TABLE Food
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
	idCategory INT NOT NULL,
	price FLOAT NOT NULL  DEFAULT 0,

	FOREIGN KEY (idCategory) REFERENCES FoodCategory(id)
)
CREATE TABLE Bill
(
	id INT IDENTITY PRIMARY KEY,
	idTable INT NOT NULL,
	DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
	DataCheckOut DATE NOT NULL,
	status INT NOT NULL, -- 1. Đã thanh toán || 0: Chưa thanh toán
	
	FOREIGN KEY(idTable) REFERENCES TableFood(id)
)
go
CREATE TABLE BillInfo
(
	id INT IDENTITY PRIMARY KEY,
	idBill INT NOT NULL,
	idFood INT NOT NULL,
	count INT NOT NULL DEFAULT 0,

	FOREIGN KEY(idBill) REFERENCES Bill(id),
	FOREIGN KEY(idFood) REFERENCES Food(id)
)
go 

INSERT INTO Account (UserName,DisplayName,Password,Type) VALUES (N'K9',N'RongK9',N'1',1)
INSERT INTO Account (UserName,DisplayName,Password,Type) VALUES (N'Staff',N'Staff',N'1',0)

SELECT*FROM Account
