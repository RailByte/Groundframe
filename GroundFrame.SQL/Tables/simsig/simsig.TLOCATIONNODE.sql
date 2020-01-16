/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Tables\simsig.TLOCATIONNODE.sql
** Name:		simsig
** Desc:		This stores a record for each unique location, version and era within a simulation
** Unit Test:	
** Auth:		Tim Caceres
** Date:		2019-12-12
**************************
** Change History
**************************
** Ver	Date		Author		Description 
** ---  --------	-------		------------------------------------
** 1    2019-12-12	TC			Initial Script creation
** 2	2020-01-16	TC			Added Path, Line and added Modified / Created fields
**
*******************************/
CREATE TABLE [simsig].[TLOCATIONNODE]
(
	[id] INT NOT NULL IDENTITY,
	[sim_id] SMALLINT NOT NULL,
	[location_id] INT NOT NULL,
	[simera_id] SMALLINT NOT NULL,
	[version_id] SMALLINT NOT NULL,
	[simsig_platform_code] VARCHAR(8) NULL,
	[simsig_elec_bitmap] TINYINT NOT NULL CONSTRAINT DF_SIMSIG_TLOCATION_SIMSIG_ELEC_BITMAP DEFAULT (0),
	[location_type_id] TINYINT NOT NULL,
	[length] SMALLINT NULL,
	[freight_only] BIT NOT NULL CONSTRAINT DF_SIMSIG_TLOCATION_FREIGHT_ONLY DEFAULT (0),
	[simsig_line] VARCHAR(4) NULL,
	[simsig_path] VARCHAR(4) NULL,
	[testdata_id] UNIQUEIDENTIFIER NULL,
	[createdby_id] SMALLINT NOT NULL,
	[createdby_app_id] SMALLINT NOT NULL,
	[createdon] DATETIMEOFFSET NOT NULL,
	[modifiedby_id] SMALLINT NOT NULL,
	[modifiedon] DATETIMEOFFSET NOT NULL,
	[modifiedby_app_id] SMALLINT NOT NULL,
	CONSTRAINT PK_SIMSIG_TLOCATIONNODE PRIMARY KEY ([id],[sim_id] ASC),
	CONSTRAINT FK_SIMSIG_TLOCATIONNODE_TSIM FOREIGN KEY ([sim_id]) REFERENCES [simsig].[TSIM] ([id]),
	CONSTRAINT FK_SIMSIG_TLOCATIONNODE_TLOCATION FOREIGN KEY ([sim_id], [location_id]) REFERENCES [simsig].[TLOCATION] ([sim_id], [id]),
	CONSTRAINT FK_SIMSIG_TLOCATIONNODE_TSIMERA FOREIGN KEY ([sim_id], [simera_id]) REFERENCES [simsig].[TSIMERA] ([sim_id], [id]),
	CONSTRAINT FK_SIMSIG_TLOCATIONNODE_TVERSION FOREIGN KEY ([version_id]) REFERENCES [simsig].[TVERSION] ([id]),
	CONSTRAINT FK_SIMSIG_TLOCATIONNODE_TLOCATIONTYPE FOREIGN KEY ([location_type_id]) REFERENCES [simsig].[TLOCATIONTYPE] ([id]),
	CONSTRAINT FG_SIMSIG_TLOCATIONNODE_CREATEDBY_APP_TUSER FOREIGN KEY ([createdby_id]) REFERENCES [app].[TUSER] ([id]),
	CONSTRAINT FG_SIMSIG_TLOCATIONNODE_CREATEDBY_APP FOREIGN KEY ([createdby_app_id]) REFERENCES [app].[TAPP] ([id]),
	CONSTRAINT FG_SIMSIG_TLOCATIONNODE_MODIFIEDBY_APP_TUSER FOREIGN KEY ([modifiedby_id]) REFERENCES [app].[TUSER] ([id]),
	CONSTRAINT FG_SIMSIG_TLOCATIONNODE_MODIFIEDBY_APP FOREIGN KEY ([modifiedby_app_id]) REFERENCES [app].[TAPP] ([id])
) AS NODE;
