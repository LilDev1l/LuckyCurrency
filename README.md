!!! Для начала работы в базу данных необходимо добавить функцию, которая требуется при авторизации !!!

Скрипт создания необходимой функции:

CREATE FUNCTION [dbo].[GetAPI_Key]
(
	@login NVARCHAR (MAX),
	@password NVARCHAR (MAX)
)
RETURNS @returntable TABLE
(
	Id        INT,
	publicKey NVARCHAR (MAX),
	secretKey NVARCHAR (MAX)
)
AS
BEGIN
	INSERT @returntable
	SELECT *
		FROM API_Key api 
		WHERE api.Id = (SELECT top(1) u.Id
						FROM Users u
						WHERE	u.Login = @login AND
								u.Password = @password)
	RETURN
END
