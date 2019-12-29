/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Tables\app\app.TUSERSTATUS.sql
** Name:		app.TUSERSTATUS
** Desc:		Table to store details of the user status
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
CREATE TABLE [app].[TUSERSTATUS]
(
	[id] TINYINT NOT NULL,
	[name] NVARCHAR(32),
	[description] NVARCHAR(2048) NULL,
	CONSTRAINT PK_APP_TUSERSTATUS PRIMARY KEY ([id] ASC)
)
