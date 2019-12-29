/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Views\simsig\simsig.VSIM.sql
** Name:		simsig.VSIM
** Desc:		View containing simulation data join to relevant views.
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

CREATE VIEW [simsig].[VSIM]
AS
SELECT
	S.[id],
	S.[name],
	S.[description],
	S.[simsig_code],
	S.[simsig_wiki_link],
	S.[createdby_id],
	[created_by_username] = CB.[username],
	S.[createdon],
	S.[modifiedby_id],
	[modified_by_username] = MB.[username],
	S.[modifiedon]
FROM [simsig].[TSIM] AS S
INNER JOIN [app].[VUSER] AS CB ON S.[createdby_id] = CB.[id]
INNER JOIN [app].[VUSER] AS MB ON S.[modifiedby_id] = MB.[id]