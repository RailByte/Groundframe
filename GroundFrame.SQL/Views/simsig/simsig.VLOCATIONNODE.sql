/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Views\simsig\simsig.VLOCATIONNODE.sql
** Name:		simsig.VLOCATIONNODE
** Desc:		View containing location node data join to relevant views.
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

CREATE VIEW [simsig].[VLOCATIONNODE]
AS
SELECT
	LN.[id],
	LN.[sim_id],
	[sim_name] = S.[name],
	LN.[location_id],
	L.[name],
	L.[tiploc],
	L.[simsig_code],
	L.[simsig_entry_point],
	LN.[simera_id],
	LN.[version_id],
	LN.[simsig_platform_code],
	LN.[simsig_line],
	LN.[simsig_path],
	LN.[simsig_elec_bitmap],
	LN.[freight_only],
	LN.[length],
	[location_type_id] = TL.[id],
	[location_type] = TL.[name],
	L.[createdby_id],
	[created_by_username] = CB.[username],
	L.[createdon],
	[created_by_app] = CA.[name],
	L.[modifiedby_id],
	[modified_by_username] = MB.[username],
	L.[modifiedon],
	[modified_by_app] = MA.[name],
	L.[testdata_id]
FROM [simsig].[TLOCATIONNODE] AS LN
INNER JOIN [simsig].[TLOCATION] AS L ON LN.[location_id] = L.[id]
INNER JOIN [app].[VUSER] AS CB ON L.[createdby_id] = CB.[id]
INNER JOIN [app].[VUSER] AS MB ON L.[modifiedby_id] = MB.[id]
INNER JOIN [app].[TAPP] AS CA ON L.[createdby_app_id] = CA.[id]
INNER JOIN [app].[TAPP] AS MA ON L.[modifiedby_app_id] = MA.[id]
INNER JOIN [simsig].[VSIM] AS S ON L.[sim_id] = S.[id]
INNER JOIN [simsig].[TLOCATIONTYPE] AS TL ON LN.[location_type_id] = TL.[id]