/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Views\simsig\simsig.VVERSION.sql
** Name:		simsig.VVERSION
** Desc:		View containing version data joined to relevant views.
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

CREATE VIEW [simsig].[VVERSION]
AS
SELECT
	V.[id],
	V.[name],
	V.[description],
	V.[simsig_version_from],
	V.[simsig_version_to],
	V.[version_status_id],
	V.[testdata_id],
	version_status_name = VS.[name],
	version_status_description = VS.[description]
FROM [simsig].[TVERSION] AS V
INNER JOIN [simsig].[TVERSIONSTATUS] AS VS ON V.[version_status_id] = VS.[id]
