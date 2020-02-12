/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Tables\simsig.TLOCATION.sql
** Name:		simsig
** Desc:		Table to store location data.
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
CREATE TABLE [simsig].[TLOCATION]
(
	[id] INT NOT NULL IDENTITY,
	[sim_id] SMALLINT NOT NULL,
	[tiploc] NVARCHAR(16) NULL,
	[name] NVARCHAR(64) NOT NULL,
	[simsig_code] NVARCHAR(16) NOT NULL,
	[simsig_entry_point] BIT NOT NULL CONSTRAINT DF_SIMSIG_TLOCATION_SIMSIG_ENTRY_POINT DEFAULT (0),
	[location_type_id] TINYINT NULL,
	[testdata_id] UNIQUEIDENTIFIER,
	[createdby_id] SMALLINT NOT NULL,
	[createdby_app_id] SMALLINT NOT NULL,
	[createdon] DATETIMEOFFSET NOT NULL,
	[modifiedby_id] SMALLINT NOT NULL,
	[modifiedon] DATETIMEOFFSET NOT NULL,
	[modifiedby_app_id] SMALLINT NOT NULL,
	CONSTRAINT PK_SIMSIG_TLOCATION PRIMARY KEY ([sim_id], [id] ASC),
	CONSTRAINT FK_SIMSIG_TLOCATION_TSIM FOREIGN KEY ([sim_id]) REFERENCES [simsig].[TSIM] ([id]),
	CONSTRAINT FK_SIMSIG_TLOCATION_TLOCATIONTYPE FOREIGN KEY ([location_type_id]) REFERENCES [simsig].[TLOCATIONTYPE] ([id]),
	CONSTRAINT FK_SIMSIG_TLOCATION_CREATEDBY_APP_TUSER FOREIGN KEY ([createdby_id]) REFERENCES [app].[TUSER] ([id]),
	CONSTRAINT FK_SIMSIG_TLOCATION_CREATEDBY_APP FOREIGN KEY ([createdby_app_id]) REFERENCES [app].[TAPP] ([id]),
	CONSTRAINT FK_SIMSIG_TLOCATION_MODIFIEDBY_APP_TUSER FOREIGN KEY ([modifiedby_id]) REFERENCES [app].[TUSER] ([id]),
	CONSTRAINT FK_SIMSIG_TLOCATION_MODIFIEDBY_APP FOREIGN KEY ([modifiedby_app_id]) REFERENCES [app].[TAPP] ([id]),
	CONSTRAINT UQ_SIMSIG_TLOCATION_SIM_SIMCODE UNIQUE ([sim_id],[simsig_code])
)
