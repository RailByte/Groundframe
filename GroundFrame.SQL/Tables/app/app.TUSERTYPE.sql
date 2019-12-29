/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Tables\app\app.TUSERTYPE.sql
** Name:		app.TUSERTYPE
** Desc:		Table to store details of the application type
** Unit Test:	
** Auth:		Tim Caceres
** Date:		2019-12-12
**************************
** Change History
**************************
** Ver	Date		Author		Description 
** ---  --------	-------		------------------------------------
** 1    2019-12-12	TC			Initial Script creation
**
*******************************/
CREATE TABLE [app].[TUSERTYPE]
(
	[id] TINYINT NOT NULL,
	[name] NVARCHAR(32),
	[description] NVARCHAR(2048) NULL,
	CONSTRAINT PK_APP_TUSERTYPE PRIMARY KEY ([id] ASC)
)
