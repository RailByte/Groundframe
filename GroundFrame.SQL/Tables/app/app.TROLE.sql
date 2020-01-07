/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Tables\app\app.TROLE.sql
** Name:		app.TROLE
** Desc:		Table to store details of each user roles
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
CREATE TABLE [app].[TROLE]
(
	[id] SMALLINT NOT NULL,
	[name] NVARCHAR(128) NOT NULL,
	[description] NVARCHAR(2048) NOT NULL,
	CONSTRAINT PK_APP_TROLE PRIMARY KEY ([id] ASC),
	CONSTRAINT UQ_APP_TROLE_NAME UNIQUE ([name])
)
