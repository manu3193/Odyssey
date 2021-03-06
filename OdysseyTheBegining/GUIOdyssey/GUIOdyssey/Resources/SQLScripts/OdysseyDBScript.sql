

--******************************************************************************************************************************************
--**
--**                                     Configuracion azure y creacion de cuenta usuario
--**
--******************************************************************************************************************************************


--Creates login user in sql
CREATE LOGIN OdysseyDBUser 
    WITH PASSWORD = 'PerroViejo1141';
GO

--Creates user in database for windows and azure conections
CREATE USER OdysseyDBUser FOR LOGIN OdysseyDBUser;
GO

--Grant permissions
EXEC sp_addrolemember 'db_owner', 'OdysseyDBUser';
GO

--Creating OdysseyDB schema
create database OdysseyDB;
GO

--Grant user operations into azure SQL database
Grant ALTER to OdysseyDBUser;
GO



--******************************************************************************************************************************************
--**
--**                                      Definici'on de tablas principales
--**
--******************************************************************************************************************************************



--Creates Genres table
GO
CREATE TABLE  Genres
	(
	GenreID uniqueidentifier NOT NULL,
	Genre nvarchar(50) NOT NULL
	) 
GO

--Creating PK in table Genres 
ALTER TABLE  Genres ADD CONSTRAINT
	PK_Genres PRIMARY KEY  
	(
	GenreID
	) 
GO

--Creating table Moods,  one to one realiationship with Genres table
CREATE TABLE  Moods
	(
	MoodID uniqueidentifier NOT NULL,
	Mood nvarchar(50) NULL,
	FK_GenreID uniqueidentifier NULL
	) 
GO

--Adding PK constraint
ALTER TABLE  Moods ADD CONSTRAINT
	PK_Moods PRIMARY KEY 
	(
	MoodID
	)
GO

--Creating FK constraint 
ALTER TABLE  Moods ADD CONSTRAINT
	FK_Moods_Genres FOREIGN KEY
	(
	FK_GenreID
	) REFERENCES  Genres
	(
	GenreID
	)
GO

--Creating Users table 
CREATE TABLE  Users
	(
	UserID uniqueidentifier NOT NULL,
	Nickname nvarchar(50) NOT NULL,
	Name nvarchar(50) NOT NULL,
	Password nvarchar(25) NOT NULL,
	FK_MoodID uniqueidentifier NULL
	)
GO
ALTER TABLE  Users ADD CONSTRAINT
	PK_Users PRIMARY KEY  
	(
	UserID
	)
GO

--Adding FK constraint
ALTER TABLE  Users ADD CONSTRAINT
	FK_Users_Moods FOREIGN KEY
	(
	FK_MoodID
	) REFERENCES  Moods
	(
	MoodID
	) 

GO

--Creating Tracks table, represents metadata atributes 
CREATE TABLE  Tracks
	(
	TrackID uniqueidentifier NOT NULL,
	Title nvarchar(50) NOT NULL,
	Lyrics nvarchar(max) NOT NULL,
	TrackURI nvarchar(200) NOT NULL,
	Album nvarchar(50) NOT NULL,
	Artist nvarchar(50) NOT NULL,
	Year int NOT NULL,
	FK_GenreID uniqueidentifier NOT NULL
	) 
GO

--Creating PK constraint
ALTER TABLE  Tracks ADD CONSTRAINT
	PK_Tracks PRIMARY KEY  
	(
	TrackID
	) 
GO

--Creating FK constraint
ALTER TABLE  Tracks ADD CONSTRAINT
	FK_Tracks_Genres FOREIGN KEY
	(
	FK_GenreID
	) REFERENCES  Genres
	(
	GenreID
	)
GO

--Creating UserTracks table, representing one to one relationship between Users and Tracks entities 
CREATE TABLE  UserTracks
	(
	FK_UserID uniqueidentifier NOT NULL,
	FK_TrackID uniqueidentifier NOT NULL
	) 
GO

--Creating PK constraint
ALTER TABLE  UserTracks ADD CONSTRAINT
	PK_UsersLibrary PRIMARY KEY  
	(
	FK_UserID,
	FK_TrackID
	) 
GO

--Creatinf FK constraint
ALTER TABLE  UserTracks ADD CONSTRAINT
	FK_UsersLibrary_Users FOREIGN KEY
	(
	FK_UserID
	) REFERENCES  Users
	(
	UserID
	)
	
GO

--Creating FK constraint
ALTER TABLE  UserTracks ADD CONSTRAINT
	FK_UsersLibrary_Tracks FOREIGN KEY
	(
	FK_TrackID
	) REFERENCES  Tracks
	(
	TrackID
	) 
	
GO

--Creating admin personal info entity
CREATE TABLE  Admins
	(
	AdminID uniqueidentifier NOT NULL,
	Nickname nvarchar(50) NOT NULL,
	Password nvarchar(50) NOT NULL
	) 
GO

--Creating FK constraint
ALTER TABLE  Admins ADD CONSTRAINT
	PK_Admins PRIMARY KEY  
	(
	AdminID
	) 
GO

--Creating Tracks history entity
CREATE TABLE Tracks_History
	(
	TrackID uniqueidentifier NOT NULL,
	Version int NOT NULL,
	Title nvarchar(50) NOT NULL,
	Lyrics nvarchar(max) NOT NULL,
	Album nvarchar(50) NOT NULL,
	Artist nvarchar(50) NOT NULL,
	Year int NOT NULL,
	FK_GenreID uniqueidentifier NOT NULL
	) 
GO

--Creating PK constraint
ALTER TABLE  Tracks_History ADD CONSTRAINT
	PK_Tracks_History PRIMARY KEY  
	(
	TrackID,
	Version
	) 
GO

--Creating FK constraint
ALTER TABLE  Tracks_History ADD CONSTRAINT
	FK_Tracks_History_Genres FOREIGN KEY
	(
	FK_GenreID
	) REFERENCES  Genres
	(
	GenreID
	)




--******************************************************************************************************************************************
--**
--**                                                      Funciones 
--**
--******************************************************************************************************************************************

--OBtiene el siguiente numero de version que se debe insertar


GO
CREATE FUNCTION dbo.GetLastVersionNumber(@TrackID uniqueidentifier)
RETURNS int
AS
BEGIN
	DECLARE @lastVersion int;
	SELECT @lastVersion = MAX(Version) FROM
	dbo.Tracks_History history
	WHERE history.TrackID = @TrackID;
	RETURN @lastVersion+1;
END
GO




/*Funcion escalar encargada de validar si ya existe un usuario, retorna el id de usuario en caso de encontrarlo, retorna null si no lo encuentra*/
GO
CREATE FUNCTION dbo.IfUserExist(@Nickname nvarchar(50))
RETURNS BIT
AS
BEGIN
	DECLARE @Exists bit
	IF EXISTS(SELECT 1 FROM dbo.Users users WHERE @Nickname =users.Nickname)
		SET @Exists = 1
	ELSE 
		SET @Exists = 0
	RETURN @Exists
END
GO


/*Funcion escalar encargada de validar si ya existe un administrador, retorna el id de administrador en caso de encontrarlo, retorna null si no lo encuentra*/
GO
CREATE FUNCTION dbo.IfAdminExist(@Nickname nvarchar(50))
RETURNS BIT
AS
BEGIN
	DECLARE @Exists bit
	IF EXISTS(SELECT 1 FROM dbo.Admins admins WHERE @Nickname =admins.Nickname)
		SET @Exists = 1
	ELSE 
		SET @Exists = 0
	RETURN @Exists
END
GO

/*Funcion que revisa si existe una cancion en la base de datos*/
GO
CREATE FUNCTION dbo.IfTrackExist(@TrackId uniqueidentifier)
RETURNS BIT 
AS
BEGIN
	DECLARE @Exist bit
	IF EXISTS(SELECT 1 FROM dbo.Tracks tracks WHERE @TrackId= tracks.TrackID)
		SET @Exist =1
	ELSE
		SET @Exist=0
	RETURN @Exist
END
GO

/*Funcion que retorna el id del genero segun su nombre*/
GO
CREATE FUNCTION dbo.GetGenreId(@Genre varchar(50))
RETURNS UNIQUEIDENTIFIER 
AS
BEGIN
	DECLARE @GenreID UNIQUEIDENTIFIER
	SELECT @GenreID = GenreId 
	FROM [dbo].[Genres] 
	WHERE Genre = @Genre
	RETURN @GenreID
END
GO


--******************************************************************************************************************************************
--**
--**                                                      Triggers 
--**
--******************************************************************************************************************************************

/* Trigger que ingresa los datos modificados de la metadata en la tabla de historial con su respectiva version,      *******FALTA PROBAR
 */
CREATE TRIGGER dbo.TrackUpdate 
	ON dbo.Tracks
AFTER UPDATE
AS
BEGIN
	DECLARE @TrackId uniqueidentifier
	DECLARE @Version int
	DECLARE @Title nvarchar(50)
	DECLARE @Lyrics nvarchar(max)
	DECLARE @Album nvarchar(50)
	DECLARE @Artist nvarchar(50)
	DECLARE @Year int
	DECLARE @Genre uniqueidentifier

	SELECT @TrackId = inserted.TrackID,
		   @Version = dbo.GetLastVersionNumber(@TrackId),
		   @Title = inserted.Title,
		   @Lyrics = inserted.Lyrics,
		   @Album = inserted.Album,
		   @Artist = inserted.Artist,
		   @Year = inserted.Year,
		   @Genre = inserted.FK_GenreID
	FROM inserted
	INSERT INTO dbo.Tracks_History values(@TrackId,@Version,@Title,@Lyrics,@Album,@Artist,@Year,@Genre)
END
GO




--******************************************************************************************************************************************
--**
--**                                       Procedimientos almacenados
--**
--******************************************************************************************************************************************


GO
/*Procedimiento almacenado utilizado para la autentificaci'on de usuario, recibe el nickname y el
  password como par'ametros. Retorna un escalar con el guid de usuario en caso de autentificarse 
  y null en caso de fallar*/
CREATE PROCEDURE [dbo].[usp_AuthUserLogin]
	@Nickname nvarchar(50),
	@Password nvarchar(50),
	@Name nvarchar(50) OUTPUT,
	@UserID uniqueidentifier OUTPUT
	
AS
BEGIN
	SET NOCOUNT ON;
	SELECT @UserID= UserID,@Name=Name
	FROM [dbo].[Users] AS users 
	WHERE [users].[Nickname] = @Nickname AND [users].[Password] = @Password
END

GO

--Elimina el procedimiento almacenado de autentificaci'on
--drop procedure dbo.usp_insertTrack
--GO

GO
/*Procedimiento almacenado utilizado para la autentificaci'on de usuario. Recibe como par'ametros 
  el nickname y password, se retorna un escalar con */
CREATE PROCEDURE [dbo].[usp_AuthAdminLogin]
	@Nickname nvarchar(50),
	@Password nvarchar(50),
	@result uniqueidentifier OUTPUT, 
	
AS
BEGIN
	SET NOCOUNT ON;
	SELECT @result= AdminID
	FROM [dbo].[Admins] AS admins 
	WHERE [admins].[Nickname] = @Nickname AND [admins].[Password] = @Password
END

GO
GO
/*Procedimiento almacenado que se encarga de ingresar nuevos usuarios*/
CREATE PROCEDURE dbo.usp_newUser
	@Nickname nvarchar(50),
	@Password nvarchar(50),
	@Name nvarchar(50),
	@UserId uniqueidentifier OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
    DECLARE @userCheck BIT
	SET @userCheck = [dbo].[IfUserExist](@Nickname)
	IF @userCheck = 1
		SET @UserId = NULL
	ELSE BEGIN
		SET @UserId = NEWID()
		INSERT INTO [dbo].[Users] VALUES (@UserId,@Nickname,@Name,@Password,NULL)
	END
END

GO
/*Procedimiento almacenado que se encarga de ingresar nuevos administradores de cloud*/
CREATE PROCEDURE dbo.usp_newAdmin
	@Nickname nvarchar(50),
	@Password nvarchar(50),
	@Name nvarchar(50),
	@AdminId uniqueidentifier OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
    DECLARE @userCheck BIT
	SET @userCheck = [dbo].[IfAdminExist](@Nickname)
	IF @userCheck = 1
		SET @AdminId = NULL
	ELSE BEGIN
		SET @AdminId = NEWID()
		INSERT INTO [dbo].[Admins] VALUES (@AdminId,@Nickname,@Name,@Password,NULL)
	END
END		
GO

/*Procedimiento almacenado que ingresa tracks nuevas a la base de datos*/
CREATE PROCEDURE dbo.usp_insertTrack
	@TrackId uniqueidentifier,
	@Title  nvarchar(50),
	@Lyrics nvarchar(max),
	@TrackURI nvarchar(200),
	@Album nvarchar(50),
	@Artist nvarchar(50),
	@Year int,
	@Genre varchar(50),
	@Result int OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @TrackCheck BIT
	DECLARE @GenreID uniqueidentifier
	SET @TrackCheck= [dbo].[IfTrackExist](@TrackId)
	SET @GenreID = [dbo].[getGenreId](@Genre)
	IF @TrackCheck = 0
		BEGIN
			INSERT INTO [dbo].[Tracks] VALUES (@TrackID ,@Title ,@Lyrics, @TrackURI, @Album, @Artist,@Year, @GenreID) 
			SET @Result = 1
		END
	ELSE
		SET @Result=0
END

/*Procedimiento almacenado que retorna las canciones de un usuario*/
CREATE PROCEDURE dbo.usp_GetAllUserTracks
	@TrackId uniqueidentifier,
	@Title  nvarchar(50),
	@Lyrics nvarchar(max),
	@TrackURI nvarchar(200),
	@Album nvarchar(50),
	@Artist nvarchar(50),
	@Year int,
	@Genre varchar(50),
	@Result int OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @TrackCheck BIT
	DECLARE @GenreID uniqueidentifier
	SET @TrackCheck= [dbo].[IfTrackExist](@TrackId)
	SET @GenreID = [dbo].[getGenreId](@Genre)
	IF @TrackCheck = 0
		BEGIN
			INSERT INTO [dbo].[Tracks] VALUES (@TrackID ,@Title ,@Lyrics, @TrackURI, @Album, @Artist,@Year, @GenreID) 
			SET @Result = 1
		END
	ELSE
		SET @Result=0
END



--******************************************************************************************************************************************
--**P
--**                                                       Pruebas
--**
--******************************************************************************************************************************************

--Inserta elementos a la tabla de usuarios
INSERT into dbo.Users values (NEWID(), 'manzumbado', 'MAnuel Zumbado', 'cacabubu', null),
							 (NEWID(), 'kevuo', 'Kevin Viquez', 'elcharlatan',null),
							 (NEWID(), 'majesco', 'Martin De JEsus', 'HELOMIDIOS',null);

GO

--Ejemplo de uso al llamar el procedimiento almacenado de autentificacion de , devuelve null si no existe o bien el id en caso de existir
DECLARE @result1 uniqueidentifier 
DECLARE @result2 nvarchar(50)
EXEC [dbo].[usp_AuthUserLogin] @Nickname='majesco',
																 @Password = 'HELOMIDIOS',
																 @Name =@result2 OUTPUT,
																 @UserId= @result1 OUTPUT
Select @result1 as UserID, @result2 as Name
go

--Ejemplo de uso al llamar el procedimiento almacenado de autentificacion de administrador, retorna null si no existe o bien el id en caso de existir
DECLARE @AdminIdd uniqueidentifier EXEC [dbo].[usp_AuthAdminLogin] @Nickname='someadmin',
																		@Password = 'somepassword',
																		@result=@adminIdd OUTPUT
SELECT @adminIdd as AdminID
GO


--Ejemplo ejecutar funcion, solo para testing ya que esta funcion esta destinada a llamarse solo en procedimientos almacenados
DECLARE @return bit EXEC @return = dbo.IfUserExist @Nickname='manzumado'
SELECT 'return' = @return
GO


