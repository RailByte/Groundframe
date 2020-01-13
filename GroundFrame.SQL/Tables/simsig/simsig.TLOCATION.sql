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
	CONSTRAINT PK_SIMSIG_TLOCATION PRIMARY KEY ([sim_id], [id] ASC),
	CONSTRAINT FK_SIMSIG_TLOCATION_TSIM FOREIGN KEY ([sim_id]) REFERENCES [simsig].[TSIM] ([id]),
	CONSTRAINT UQ_SIMSIG_TLOCATION_SIM_SIMCODE UNIQUE ([sim_id],[simsig_code])
)
