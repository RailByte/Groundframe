/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Tables\simsig.TLOCATIONTYPE.sql
** Name:		simsig
** Desc:		Table to store the location type
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
CREATE TABLE [simsig].[TLOCATIONTYPE]
(
	[itemno] TINYINT NOT NULL,
	[name] NVARCHAR(32),
	[description] NVARCHAR(2048),
	CONSTRAINT PK_SIMSIG_TLOCATIONTYPE PRIMARY KEY ([itemno] ASC)
)
