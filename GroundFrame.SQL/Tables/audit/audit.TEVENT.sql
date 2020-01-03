/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Tables\debug\audit.TEVENT.sql
** Name:		audit.TEVENT
** Desc:		Table to store event information raised from debugging
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
CREATE TABLE [audit].[TEVENT]
(
	[id] INT NOT NULL IDENTITY,
	[debug_session_id] UNIQUEIDENTIFIER NOT NULL,
	[event_datetime] DATETIME2 CONSTRAINT DF_DEBUG_TEVENT_EVENT_DATETIME DEFAULT (GETUTCDATE()),
	[source_object_id] INT NOT NULL,
	[event] NVARCHAR(MAX),
	[db_user] NVARCHAR(256) NOT NULL CONSTRAINT DF_DEBUG_TEVENT_DB_USER DEFAULT (SUSER_NAME()),
	[app_id] SMALLINT NOT NULL,
	[app_user_id] INT NOT NULL,
	CONSTRAINT PK_DEBUG_TEVENT PRIMARY KEY ([id] ASC),
	CONSTRAINT FK_DEBUG_TEVENT_COMMON_TAPP FOREIGN KEY ([app_id]) REFERENCES [app].[TAPP]([id])
)
