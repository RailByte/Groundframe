/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Tables\simsig\simsig.TSIM.sql
** Name:		simsig.TSIM
** Desc:		Table to store simulation data.
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
CREATE TABLE [simsig].[TSIM]
(
	[id] SMALLINT NOT NULL IDENTITY,
	[name] NVARCHAR(128) NOT NULL,
	[description] NVARCHAR(2048) NULL,
	[simsig_code] NVARCHAR(32) NOT NULL,
	[simsig_wiki_link] NVARCHAR(512) NULL,
	[createdby_id] SMALLINT NOT NULL,
	[createdon] DATETIMEOFFSET NOT NULL,
	[modifiedby_id] SMALLINT NOT NULL,
	[modifiedon] DATETIMEOFFSET NOT NULL,
	[testdata_id] UNIQUEIDENTIFIER NULL,
	CONSTRAINT PK_SIMSIG_TSIM PRIMARY KEY ([id] ASC),
	CONSTRAINT FG_SIMSIG_TSIM_CREATEDBY_APP_TUSER FOREIGN KEY ([createdby_id]) REFERENCES [app].[TUSER] ([id]),
	CONSTRAINT FG_SIMSIG_TSIM_MODIFIEDBY_APP_TUSER FOREIGN KEY ([modifiedby_id]) REFERENCES [app].[TUSER] ([id]),
	CONSTRAINT UQ_SIMSIG_TSIM_SIMSIG_CODE UNIQUE ([simsig_code])
)
