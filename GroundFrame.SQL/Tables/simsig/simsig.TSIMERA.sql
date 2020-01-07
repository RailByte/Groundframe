/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Tables\simsig\simsig.TSIMERA.sql
** Name:		simsig
** Desc:		Table to store SimSig simulation eras.
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
CREATE TABLE [simsig].[TSIMERA]
(
	[id] SMALLINT NOT NULL IDENTITY,
	[sim_id] SMALLINT NOT NULL,
	[name] NVARCHAR(128) NOT NULL,
	[description] NVARCHAR(2048) NULL,
	[era_type_id] TINYINT NOT NULL CONSTRAINT DF_SIMSIG_TSIMERA_ERA_TYPE DEFAULT (1),
	[testdata_id] UNIQUEIDENTIFIER CONSTRAINT DF_SIMSIG_TSIMERA_TESTDATA DEFAULT (NULL),--Used to identify a test set of data. Tests can then be carried out on just data added by a test to ensure to 'contamination' of data
	CONSTRAINT PK_SIMSIG_TSIMERA PRIMARY KEY ([sim_id], [id] ASC),
	CONSTRAINT FK_SIMSIG_TSIMERA_TSIM FOREIGN KEY ([sim_id]) REFERENCES [simsig].[TSIM] ([id]),
	CONSTRAINT FK_SIMSIG_TSIMERA_TERATYPE  FOREIGN KEY ([era_type_id]) REFERENCES [simsig].[TERATYPE] ([id]),
)
