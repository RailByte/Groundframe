/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Tables\common\common.TAPIKEY.sql
** Name:		common.TAPIKEY
** Desc:		Table to store each API key issued
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
CREATE TABLE [common].[TAPIKEY]
(
	[api_key] VARCHAR(16) NOT NULL,
	CONSTRAINT PK_COMMON_TAPIKEY PRIMARY KEY ([api_key] ASC)
)
