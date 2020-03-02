/******************
** File:		~\GroundFrame\GroundFrame.SQL\Functions\Scalar\simsig\app.Fn_GET_LOCATIONNODE_NODEID.sql
** Name:		simsig.Fn_GET_LOCATIONNODE_NODEID
** Desc:		Function to getthe node_id of the supplied locationnode_id
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
CREATE FUNCTION [simsig].[Fn_GET_LOCATIONNODE_NODEID]
(
	@locationnode_id INT
)
RETURNS NVARCHAR(1000)
AS
BEGIN
	RETURN (SELECT $node_id FROM [simsig].[TLOCATIONNODE] WHERE [id] = @locationnode_id);
END

