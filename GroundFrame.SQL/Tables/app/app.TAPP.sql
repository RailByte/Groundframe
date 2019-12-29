/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Tables\app\app.TAPP.sql
** Name:		app.TAPP
** Desc:		Table to store details of each application which accesses the system
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
CREATE TABLE [app].[TAPP]
(
	[id] SMALLINT NOT NULL IDENTITY,
	[name] NVARCHAR(256) NOT NULL,
	[description] NVARCHAR(2048) NOT NULL,
	[api_key] VARCHAR(16) NOT NULL,
	[app_status_id] TINYINT CONSTRAINT DF_APP_TAPP_STATUS_ID DEFAULT (1),
	[app_type_id] TINYINT,
	[app_url] VARCHAR(512) NULL,
	[owner_id] INT NOT NULL,
	[datetime_last_used] DATETIME2 NULL,
	[createdby_id] INT NOT NULL,
	[createdon] DATETIMEOFFSET NOT NULL,
	[modifiedby_id] INT NOT NULL,
	[modifiedon] DATETIMEOFFSET NOT NULL,
	CONSTRAINT PK_APP_TAPP PRIMARY KEY (id ASC),
	CONSTRAINT FK_APP_TAPP_COMMON_TAPIKEY FOREIGN KEY ([api_key]) REFERENCES [common].[TAPIKEY] ([api_key]),
	CONSTRAINT FK_APP_TAPP_TAPPSTATUS FOREIGN KEY ([app_status_id]) REFERENCES [app].[TAPPSTATUS] ([id]),
	CONSTRAINT FK_APP_TAPP_TAPPTYPE FOREIGN KEY ([app_type_id]) REFERENCES [app].[TAPPTYPE] ([id])
)
