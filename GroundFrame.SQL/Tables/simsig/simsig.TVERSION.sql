/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Tables\simsig.TVERSION.sql
** Name:		simsig
** Desc:		Table to store SimSig versions. One row can cover a range of SimSig versions. A new entry should be only added when the SimSig TimeTable schema changes
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
CREATE TABLE [simsig].[TVERSION]
(
	[id] INT NOT NULL IDENTITY,
	[simsig_version_from] NUMERIC(4,1) NOT NULL,
	[simsig_version_to] NUMERIC(4,1) NULL,
	[name] NVARCHAR(128) NOT NULL,
	[decription] NVARCHAR(2048) NULL,
	CONSTRAINT PK_SIMSIG_TVERSION PRIMARY KEY ([id] ASC)
)
