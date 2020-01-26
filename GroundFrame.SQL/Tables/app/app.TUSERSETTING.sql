/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Tables\app\app.TUSERSETTING.sql
** Name:		app.TUSERSETTING
** Desc:		Table to store user settings
** Unit Test:	
** Auth:		Tim Caceres
** Date:		2019-12-12
**************************
** Change History
**************************
** Ver	Date		Author		Description 
** ---  --------	-------		------------------------------------
** 1    2020-01-25	TC			Initial Script creation
**
*******************************/
CREATE TABLE [app].[TUSERSETTING]
(
	[id] SMALLINT IDENTITY NOT NULL,
	[user_id] SMALLINT NOT NULL,
	[setting_id] SMALLINT NOT NULL,
	[value] NVARCHAR(128) NOT NULL,
	CONSTRAINT PK_APP_TUSERSETTING PRIMARY KEY ([id] ASC),
	CONSTRAINT FK_APP_TUSERSETING_USER FOREIGN KEY ([user_id]) REFERENCES [app].[TUSER] ([id]),
	CONSTRAINT FK_APP_TUSERSETING_SETTING FOREIGN KEY ([setting_id]) REFERENCES [app].[TSETTING] ([id])
)
