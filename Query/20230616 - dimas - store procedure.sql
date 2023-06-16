
create procedure [dbo].[USP_GETMEMBER]                          
	@EMAIL VARCHAR(50)
as
begin
	select TOP 1 * from MEMBER where email=@EMAIL
	select h.* from MEMBER_HOBBY mb join RFHOBBY h on mb.ID_HOBBY=h.ID where mb.ID_MEMBER=(select TOP 1 ID from MEMBER where email=@EMAIL)
end
GO

create procedure [dbo].[USP_MEMBER]                          
	@ID VARCHAR(5), @NAMA VARCHAR(100), @EMAIL VARCHAR(50), @PHONE VARCHAR(15)
as
begin
	if EXISTS(SELECT * FROM MEMBER WHERE email=@EMAIL)
	begin
		RAISERROR('email yang sudah diinputkan sudah ada !', 16, 1)                         
		RETURN -1     
	end

	if EXISTS (SELECT * FROM MEMBER WHERE email=@EMAIL or ID=@ID)
	begin 
		update MEMBER set NAMA=@NAMA, EMAIL=@EMAIL, PHONE=@PHONE
		where ID=@ID
	end
	else
	begin
		INSERT INTO MEMBER VALUES (@ID, @NAMA, @EMAIL, @PHONE)
	end
end
go

create procedure [dbo].[USP_MEMBER_UPADTE]                          
	@ID VARCHAR(5), @NAMA VARCHAR(100), @EMAIL VARCHAR(50), @PHONE VARCHAR(15)
as
begin
	if EXISTS (SELECT * FROM MEMBER WHERE email=@EMAIL or ID=@ID)
	begin 
		update MEMBER set NAMA=@NAMA, EMAIL=@EMAIL, PHONE=@PHONE
		where ID=@ID
	end
	else
	begin
		INSERT INTO MEMBER VALUES (@ID, @NAMA, @EMAIL, @PHONE)
	end
end
go

create procedure [dbo].[USP_MEMBER_HOBBY]                          
	@ID_MEMBER VARCHAR(5), @ID_HOBBY VARCHAR(5)
as
begin
	if NOT EXISTS(SELECT * FROM RFHOBBY WHERE Id=@ID_HOBBY)
	begin
		RAISERROR('daftar hobby tidak ada !', 16, 1)                         
		RETURN -1     
	end

	INSERT INTO MEMBER_HOBBY VALUES (@ID_MEMBER, @ID_HOBBY)
end
GO


--exec USP_GETMEMBER 'dribrahim004@gmail.com'
--EXEC USP_MEMBER '00001', 'aa', 'aa@gmail.com', '022'
--EXEC USP_MEMBER_UPADTE '00001', 'aa', 'aa@gmail.com', '022'
--EXEC USP_MEMBER_HOBBY '00001', '00001'
