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
	DateCheckIn DATE DEFAULT GETDATE(),
	DataCheckOut DATE ,
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

GO

CREATE PROC USP_GetAccountByUserName
@userName nvarchar(100)
AS
BEGIN
	SELECT*FROM Account WHERE UserName=@userName;
END
EXEC USP_GetAccountByUserName @userName=N'K9';

select * from dbo.Account where UserName = N'K9' and Password= N'1';

CREATE PROCEDURE USP_Login
@username nvarchar(100), @password nvarchar(100)
AS
BEGIN
	SELECT * FROM dbo.account WHERE UserName=@username AND Password=@password
END
GO

exec USP_Login @username=N'K9', @password=N'1';


DECLARE @i INT=1
WHILE @i<=10
BEGIN
	INSERT INTO dbo.tablefood	
	        ( name,status)VALUES  (  N'Table' + CAST(@i AS NVARCHAR(100)), N'Trống')
			SET @i= @i+1
END
GO
-----------
DECLARE @i INT=11
WHILE @i<=12
BEGIN
	INSERT INTO dbo.tablefood	
	        ( name,status)VALUES  (  N'Table' + CAST(@i AS NVARCHAR(100)), N'Trống')
			SET @i= @i+1
END
GO
--------------
SELECT * FROM dbo.tablefood
GO


CREATE PROCEDURE USP_Tablefood
AS SELECT * FROM dbo.tablefood
GO
----------
EXEC dbo.USP_Tablefood

select * from Bill
select * from BillInfo
select * from Food
select * from FoodCategory

------------------------------------------
INSERT INTO dbo.FoodCategory
        ( name )
VALUES  ( N'Món biển'  -- nametable - nvarchar(100)
          )

INSERT INTO dbo.FoodCategory
        ( name )
VALUES  ( N'Món Rừng'  -- nametable - nvarchar(100)
          )

INSERT INTO dbo.FoodCategory
        ( name )
VALUES  ( N'Nước uống'  -- nametable - nvarchar(100)
          )
INSERT INTO dbo.FoodCategory
        ( name )
VALUES  ( N'Nông sản')
--
-------thêm món ăn menu id=1
INSERT INTO dbo.food
        ( name, idCategory , price )
VALUES  ( N'Bắp cải xào', -- name - nvarchar(100)
          4, -- idCategory  - int
          50000  -- price - float
          )
GO
INSERT INTO dbo.food
        ( name, idCategory , price )
VALUES  ( N'Mực một nắng', -- name - nvarchar(100)
          1, -- idCategory  - int
          250000  -- price - float
          )   
GO
----------------menu id=2------------------
INSERT INTO dbo.food
        ( name, idCategory , price )
VALUES  ( N'Heo rừng nướng', -- name - nvarchar(100)
          2, -- idCategory  - int
          500000  -- price - float
          )
GO
INSERT INTO dbo.food
        ( name, idCategory , price )
VALUES  ( N'Thịt nai gu ti', -- name - nvarchar(100)
          2, -- idCategory  - int
          40000  -- price - float
          )
GO
INSERT INTO dbo.food
        ( name, idCategory , price )
VALUES  ( N'Nhím ca ry', -- name - nvarchar(100)
          2, -- idCategory  - int
          200000  -- price - float
          )
GO
INSERT INTO dbo.food
        ( name, idCategory , price )
VALUES  ( N'Bò kho', -- name - nvarchar(100)
          2, -- idCategory  - int
          100000  -- price - float
          )
GO
INSERT INTO dbo.food
        ( name, idCategory , price )
VALUES  ( N'Trâu gác bếp', -- name - nvarchar(100)
          2, -- idCategory  - int
          150000  -- price - float
          )
GO
-----------menu id=3
INSERT INTO dbo.food
        ( name, idCategory , price )
VALUES  ( N'Bia 333', -- name - nvarchar(100)
          3, -- idCategory  - int
          15000  -- price - float
          )
GO
INSERT INTO dbo.food
        ( name, idCategory , price )
VALUES  ( N'Bia Tiger', -- name - nvarchar(100)
          3, -- idCategory  - int
          15000  -- price - float
          )
GO
INSERT INTO dbo.food
        ( name, idCategory , price )
VALUES  ( N'CoCa Cola', -- name - nvarchar(100)
          3, -- idCategory  - int
          10000  -- price - float
          )
GO
INSERT INTO dbo.food
        ( name, idCategory , price )
VALUES  ( N'Nước suối', -- name - nvarchar(100)
          3, -- idCategory  - int
          5000  -- price - float
          )
INSERT INTO dbo.food
        ( name, idCategory , price )
VALUES  ( N'Dừa tươi', -- name - nvarchar(100)
          3, -- idCategory  - int
          20000  -- price - float
          )
GO
----------menu id-4
INSERT INTO dbo.food
        ( name, idCategory , price )
VALUES  ( N'Mì Xào rau muốn', -- name - nvarchar(100)
          4, -- idCategory  - int
          50000  -- price - float
          )
GO
INSERT INTO dbo.food
        ( name, idCategory , price )
VALUES  ( N'Rau củ xào thập cẩm', -- name - nvarchar(100)
          4, -- idCategory  - int
          50000  -- price - float
          )
GO
-----------------thêm bill
INSERT INTO dbo.Bill
        ( datecheckIn ,
          datecheckout ,
          idtable ,
          status
        )
VALUES  ( GETDATE() , -- datecheckIn - date
          NULL , -- datecheckout - date
          13 , -- idtable - int
          1  -- status - int
        )
go
INSERT INTO dbo.Bill
        ( datecheckIn ,
          datecheckout,
          idtable ,
          status
        )
VALUES  ( GETDATE() , -- datecheckIn - date
          null , -- datecheckout - date
          16 , -- idtable - int
          0  -- status - int
        )
GO
		INSERT INTO dbo.Bill
        ( datecheckIn ,
          datecheckout ,
          idtable ,
          status
        )
VALUES  ( GETDATE() , -- datecheckIn - date
          GETDATE() , -- datecheckout - date
          19 , -- idtable - int
          1  -- status - int
        )
GO 
----------inser billinfo
INSERT dbo.billInfo
        ( idbill, idfood, count )
VALUES  ( 1, -- idbill - int
          3, -- idfood - int
          2  -- count - int
          )
GO 
INSERT dbo.billInfo
        ( idbill, idfood, count )
VALUES  ( 1, -- idbill - int
          7, -- idfood - int
          1  -- count - int
          )
GO 
INSERT dbo.billInfo
        ( idbill, idfood, count )
VALUES  ( 1, -- idbill - int
         12, -- idfood - int
          1  -- count - int
          )
GO 
--------insert bill 2
INSERT dbo.billInfo
        ( idbill, idfood, count )
VALUES  ( 2, -- idbill - int
         10, -- idfood - int
          1  -- count - int
          )
GO 
INSERT dbo.billInfo
        ( idbill, idfood, count )
VALUES  ( 2, -- idbill - int
         3, -- idfood - int
          1  -- count - int
          )
GO 
INSERT dbo.billInfo
        ( idbill, idfood, count )
VALUES  ( 1, -- idbill - int
         8, -- idfood - int
          3  -- count - int
          )
GO 

----------insert billinfo 3
INSERT dbo.billInfo
        ( idbill, idfood, count )
VALUES  ( 3, -- idbill - int
         9, -- idfood - int
          1  -- count - int
          )
GO 

INSERT dbo.billInfo
        ( idbill, idfood, count )
VALUES  ( 3, -- idbill - int
         14, -- idfood - int
          3  -- count - int
          )
GO 


--
select * from Bill
select * from TableFood
select * from Bill where idTable = 13 and status = 1;
select * from BillInfo where idBill = 1

SELECT f.name, bi.count, f.price, f.price*bi.count AS totalPrice FROM dbo.BillInfo AS bi, dbo.Bill AS b, dbo.Food AS f WHERE bi.idBill = b.id AND bi.idFood = f.id AND b.status = 0 AND b.idTable = 16
go



create proc USP_InsertBill
@idTable INT
as
begin
	Insert dbo.Bill
		(	DateCheckIn,
			DateCheckOut,
			idTable,
			status)
	values( GETDATE(),
			NULL,
			@idTable, --idTable -int
			0)
end
go

alter proc USP_InsertTable
@name varchar(100)
as
begin
	Insert dbo.TableFood
		(name,
			status)
	values(
			@name, --idTable -int
			 N'Trống')
end
go
exec USP_InsertTable @name = 'hehe'
go
alter proc USP_InsertBillInfo
@idBill INT, @idFood int, @count int
as
begin
	Declare @isExitsBill INT;
	Declare @foodCount INT = 1;
	Select @isExitsBill = id, @foodCount = b.count from dbo.BillInfo as b where idBill = @idBill and idFood = @idFood
	if(@isExitsBill > 0)
		begin 
			Declare @newCount INT = @foodCount + @count;
			if (@newCount > 0)
				update dbo.BillInfo set count = @foodCount + @count where idFood = @idFood
			else
				delete dbo.BillInfo where idBill = @idBill and idFood = @idFood
		end
	else
		begin
			Insert dbo.BillInfo
				(	idBill,
					idFood,
					count)
			values( @idBill,
					@idFood,
					@count)
		end
end
go
SELECT MAX(id) FROM dbo.Bill
select * from BillInfo where idBill = 2
Select  id, count = b.count from dbo.BillInfo as b where idBill = 2 and idFood = 10
exec USP_InsertBillInfo @idBill=2 , @idFood=3 , @count=1
go

delete dbo.BillInfo
delete dbo.Bill


--
create trigger UTG_UpdateBillInfo
on dbo.BillInfo for Insert, Update
as 
begin
	Declare @idBill int
	select @idBill = idBill from inserted
	Declare @idtable int
	
	select @idtable = idTable from dbo.Bill where id = @idBill and status = 0
	
	Declare @count int
	select @count = COUNT(*) from dbo.BillInfo where idBill = @idBill

	if(@count >0)
		update dbo.TableFood set status = N'Có Người' where id = @idtable
	else
		update dbo.TableFood set status = N'Trống' where id = @idtable
end
go

create trigger UTG_UpdateTable
on dbo.TableFood for Update
as
begin
	Declare @idtable int
	Declare @status nvarchar(100)

	select @idtable = id, @status = inserted.status from inserted
	
	Declare @idBill int
	select @idBill = id from dbo.Bill where idTable = @idtable and status = 0

	declare @countBillinfo int
	select @countBillinfo = count(*) from dbo.BillInfo where idBill = @idBill

	if (@countBillinfo > 0 and @status <> N'Có Người')
		update dbo.TableFood set status = N'Có Người' where id = @idtable
	else if (@countBillinfo <= 0 and @status <> N'Trống')
		update dbo.TableFood set status = N'Trống' where id = @idtable
		

end
go


alter trigger UTG_UpdateBill
on dbo.Bill for Update
as 
begin 
	Declare @idBill int
	select @idBill = id from inserted

	Declare @idtable int
	select @idtable = idTable from dbo.Bill where id = @idBill

	Declare @count int = 0
	select COUNT(*) from dbo.Bill where idTable = @idtable and status = 0 
	if (@count = 0)
		update dbo.TableFood set status = N'Trống' where id = @idtable
end
go

---
alter table dbo.Bill 
add discount int
alter table dbo.Bill 
add totalprice int
update dbo.Bill set discount = 0
select * from Bill
go
---
alter proc USP_SwitchTable
@idTable1 int , @idTable2 int
as 
begin
	Declare @idFristBill int
	Declare @idSecondBill int 

	Declare @idFristTableEmpy int = 1
	Declare @idSecondTableEmpy int = 1


	select @idFristBill = id from dbo.Bill where idTable = @idTable1 and status = 0
	select @idSecondBill = id from dbo.Bill where idTable = @idTable2 and status = 0

	if(@idFristBill is NULL)
		begin 
			insert dbo.Bill
			(	DateCheckIn,
				DateCheckOut,
				idTable,
				status)
			values (GETDATE(),
					NULL,
					@idTable1,
					0)
			select @idFristBill = Max(id) from dbo.Bill where idTable = @idTable1 and status = 0
			--set @idFristTableEmpy = 1
		end		
		select @idFristTableEmpy = COUNT(*) from dbo.BillInfo where idBill = @idFristBill
	if(@idSecondBill is NULL)
		begin 
			insert dbo.Bill
			(	DateCheckIn,
				DateCheckOut,
				idTable,
				status)
			values (GETDATE(),
					NULL,
					@idTable2,
					0)
			select @idSecondBill = Max(id) from dbo.Bill where idTable = @idTable2 and status = 0
			--set @idSecondTableEmpy = 1
		end	
		select @idSecondTableEmpy = COUNT(*) from dbo.BillInfo where idBill = @idSecondBill

	select id into IDBillInfoTable from dbo.BillInfo where idBill = @idSecondBill
	update dbo.BillInfo set idBill = @idSecondBill where idBill = @idFristBill
	update dbo.BillInfo set idBill = @idFristBill where id In (select * from IDBillInfoTable)
	drop Table IDBillInfoTable

	if (@idFristTableEmpy = 0)
		update dbo.TableFood set
				status = N'Trống' where id = @idTable2
	if (@idSecondTableEmpy = 0)
		update dbo.TableFood set
				status = N'Trống' where id = @idTable1
end
go


select * from dbo.Bill where status = 1 and DateCheckIn >= '20201122' and DateCheckOut<='20201122'
select t.name, b.totalprice, DateCheckIn, DateCheckOut, discount
	from dbo.Bill as b, dbo.TableFood as t
	where DateCheckIn >= '20201122' and DateCheckOut<='20201122' and b.status =1
	and t.id = b.id
go

create proc USP_GetListBillByDate
@checkIn date, @checkOut date
as begin

	select t.name, b.totalprice, DateCheckIn, DateCheckOut, discount
	from dbo.Bill as b, dbo.TableFood as t
	where DateCheckIn >= @checkIn and DateCheckOut<= @checkOut and b.status =1
	and t.id = b.id
end
go

drop proc USP_GetListBillByDate
go 
select * from account
go

create proc  USP_UpdateAccount
@userName nvarchar(100), @displayName nvarchar(100), @password nvarchar(100), @newPassword nvarchar(100)
as 
begin
	declare @isRightPass int = 0

	select @isRightPass = count(*) from dbo.Account where UserName = @userName and Password= @password

	if (@isRightPass=1)
	begin
		if(@newPassword is NULL or @newPassword = '')
		begin
			Update dbo.Account set DisplayName = @displayName where UserName = @userName
		end
		else
			Update dbo.A	ccount set DisplayName = @displayName, Password = @newPassword where UserName = @userName
	end
end
go
select * from Food
go 

--
create trigger UTG_DeleteBillInfo
on dbo.BillInfo for delete
as 
begin
	Declare @idBillInfo int
	Declare @idBill int
	select @idBillInfo = id, @idBill = deleted.idBill from deleted

	Declare @idTable int
	select @idTable = idTable from dbo.Bill where id= @idBill

	Declare @count  int = 0
	select @count = count(*) from dbo.BillInfo as bi, dbo.Bill as b where b.id = bi.idBill and b.id = @idBill and b.status =0

	if(@count = 0)
		update dbo.TableFood set status = N'Trống' where id = @idTable
end
go

CREATE FUNCTION [dbo].fuConvertToUnsign1(@strInput NVARCHAR(4000)) 
RETURNS NVARCHAR(4000)
AS
BEGIN     
    IF @strInput IS NULL RETURN @strInput
    IF @strInput = '' RETURN @strInput
    DECLARE @RT NVARCHAR(4000)
    DECLARE @SIGN_CHARS NCHAR(136)
    DECLARE @UNSIGN_CHARS NCHAR (136)

    SET @SIGN_CHARS       = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệếìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵýĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ'+NCHAR(272)+ NCHAR(208)
    SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeeeiiiiiooooooooooooooouuuuuuuuuuyyyyyAADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD'

    DECLARE @COUNTER int
    DECLARE @COUNTER1 int
    SET @COUNTER = 1
 
    WHILE (@COUNTER <=LEN(@strInput))
    BEGIN   
      SET @COUNTER1 = 1
      --Tim trong chuoi mau
       WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1)
       BEGIN
     IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) )
     BEGIN           
          IF @COUNTER=1
              SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1)                   
          ELSE
              SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER)    
              BREAK         
               END	
             SET @COUNTER1 = @COUNTER1 +1
       END
      --Tim tiep
       SET @COUNTER = @COUNTER +1
    END
    RETURN @strInput
END
go

SELECT * FROM dbo.Food WHERE dbo.fuConvertToUnsign1(name) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'
select * from Food
