CREATE FUNCTION [dbo].[userLoginAuth] (@username varchar(45), @pass varchar(45))
RETURNS bit
AS 
BEGIN
    DECLARE @result bit
    IF EXISTS (SELECT * FROM [dbo].[Users] WHERE Id=@username AND Password=@pass)
		SET @result=1
	ELSE
		SET @result=0
RETURN @result
END;
GO

