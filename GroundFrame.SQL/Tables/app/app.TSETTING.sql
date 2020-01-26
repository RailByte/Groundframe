/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Tables\app\app.TSETTING.sql
** Name:		app.TSETTING
** Desc:		Table to store available settings
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
CREATE TABLE [app].[TSETTING]
(
	[id] SMALLINT NOT NULL,
	[key] NVARCHAR(256) NOT NULL,
	[description] NVARCHAR(2048) NULL,
	[data_type] VARCHAR(16) NOT NULL,
	[default_value] NVARCHAR(128) NOT NULL,
	CONSTRAINT PK_APP_TSETTING PRIMARY KEY (id ASC),
	CONSTRAINT UQ_APP_TSETTING_KEY UNIQUE ([key])
)
