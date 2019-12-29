/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Tables\app\app.TAPPSTATUS.sql
** Name:		app.TAPP
** Desc:		Table to store details of the application status
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
CREATE TABLE [app].[TAPPSTATUS]
(
	[id] TINYINT NOT NULL,
	[name] NVARCHAR(32),
	[description] NVARCHAR(2048) NULL,
	CONSTRAINT PK_APP_TAPPSTATUS PRIMARY KEY ([id] ASC)
)
