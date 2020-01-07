/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Tables\simsig\simsig.TVERSIONSTATUS.sql
** Name:		simsig.TVERSIONSTATUS
** Desc:		Table to store the version status
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
CREATE TABLE [simsig].[TVERSIONSTATUS]
(
	[id] TINYINT NOT NULL,
	[name] NVARCHAR(32),
	[description] NVARCHAR(2048),
	CONSTRAINT PK_SIMSIG_TVERSIONSTATUS PRIMARY KEY ([id] ASC)
)
