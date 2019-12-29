/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Tables\simsig.TLOCATION.sql
** Name:		simsig
** Desc:		Stores a record between each node in TLOCATIONNODE to represent a path between each location
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
CREATE TABLE [simsig].[TPATHEDGE]
(
	[simsig_elec_bitmap] TINYINT NOT NULL CONSTRAINT DF_SIMSIG_TPATHEDGE_SIMSIG_ELEC_BITMAP DEFAULT (0),
	[location_type_id] TINYINT NOT NULL,
	[length] SMALLINT NULL,
) AS EDGE
