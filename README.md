![image](https://github.com/user-attachments/assets/f315a34f-3cab-45f0-8f2c-7e2ae157ba90)
![image](https://github.com/user-attachments/assets/1e783d9b-1878-4685-bb1b-8e70bbbac43b)





SQL commands :-


______________________________________________________________________________________________


create table [User]
(mailId varchar(60) primary key,
password varchar(512) not null)

______________________________________________________________________________________________

CREATE PROCEDURE InsertProc
    @mailId VARCHAR(60),
    @password VARCHAR(512)
AS
BEGIN
    INSERT INTO [User] (mailId, password)
    VALUES (@mailId, @password);
END


______________________________________________________________________________________________


CREATE PROCEDURE FetchPassword
	@mailId VARCHAR(60)
AS
BEGIN
	SELECT password FROM [User] WHERE mailId = @mailId;
END

______________________________________________________________________________________________

create table Keys
( [key] varchar(512) primary key) 

______________________________________________________________________________________________

INSERT INTO Keys
VALUES('F94970E44D251B14CEB2D744D032B594E7F0E063EA708B6991B54DCED81C3EC7')

______________________________________________________________________________________________

create procedure GetKey
as
begin
	select * from Keys
end
