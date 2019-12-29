/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Tables\simsig\simsig.TERATYPE.sql
** Name:		simsig
** Desc:		Table to store SimSig simulation era type.
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
CREATE TABLE [simsig].[TERATYPE]
(
	[id] TINYINT NOT NULL,
	[name] NVARCHAR(128) NOT NULL,
	[description] NVARCHAR(2048) NULL,
	CONSTRAINT PK_SIMSIG_TERATYPE PRIMARY KEY ([id] ASC)
)
