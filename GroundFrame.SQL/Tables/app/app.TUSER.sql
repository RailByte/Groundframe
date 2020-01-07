/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Tables\app\app.TUSER.sql
** Name:		app.TUSER
** Desc:		Table to store details of users with access to the system
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
CREATE TABLE [app].[TUSER]
(
	[id] SMALLINT NOT NULL IDENTITY,
	[username] NVARCHAR(256) NOT NULL,
	[last_name] NVARCHAR(128) NOT NULL,
	[first_name] NVARCHAR(128) NOT NULL,
	[email] NVARCHAR(512) NULL,
	[api_key] VARCHAR(40) NULL, --Auth0 API Key
	[user_status_id] TINYINT NOT NULL CONSTRAINT DF_APP_TUSER_STATUS_ID DEFAULT (1),
	[user_type_id] TINYINT NOT NULL,
	[datetime_last_loggedin] DATETIME2 NULL,
	[role_bitmap] BIGINT NOT NULL CONSTRAINT DF_APP_TUSER_ROLE_BITMAP DEFAULT (1),
	[createdby_id] SMALLINT NOT NULL,
	[createdon] DATETIMEOFFSET NOT NULL,
	[modifiedby_id] SMALLINT NOT NULL,
	[modifiedon] DATETIMEOFFSET NOT NULL,
	CONSTRAINT PK_APP_TUSER PRIMARY KEY (id ASC),
	CONSTRAINT FK_APP_TUSER_TUSERSTATUS FOREIGN KEY ([user_status_id]) REFERENCES [app].[TUSERSTATUS] ([id]),
	CONSTRAINT FK_APP_TUSER_TUSERTYPE FOREIGN KEY ([user_type_id]) REFERENCES [app].[TUSERTYPE] ([id]),
	CONSTRAINT FK_APP_TUSER_CREATEDON_TUSER FOREIGN KEY ([createdby_id]) REFERENCES [app].[TUSER] ([id]),
	CONSTRAINT FK_APP_TUSER_MODIFIEDON_TUSER FOREIGN KEY ([modifiedby_id]) REFERENCES [app].[TUSER] ([id]),
	CONSTRAINT CK_APP_TUSER_APIKEY CHECK ([user_type_id] != 1 OR ([user_type_id] = 1 AND [api_key] IS NOT NULL)) ,--If the user type is External (1) the API Key must be populated,
	CONSTRAINT UQ_APP_TUSER_USERNAME UNIQUE ([username]),
	CONSTRAINT UQ_APP_TUSER_EMAIL UNIQUE ([email])
)
