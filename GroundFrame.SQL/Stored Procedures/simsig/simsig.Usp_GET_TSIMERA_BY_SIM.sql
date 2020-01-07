/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Stored Procedures\simsig\simsig.Usp_GET_TSIMERA_BY_SIM.sql
** Name:		simsig.Usp_GET_TSIMERA_BY_SIM
** Desc:		Gets a list of the Simulation Eras for the Supplied
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
CREATE PROCEDURE [simsig].[Usp_GET_TSIMERA_BY_SIM]
	@sim_id SMALLINT
AS
BEGIN
	DECLARE @testdata_id UNIQUEIDENTIFIER = CONVERT(UNIQUEIDENTIFIER,SESSION_CONTEXT(N'testdata_id'))

	SELECT
		E.[id],
		E.[sim_id],
		E.[name],
		E.[description],
		E.[era_type_id],
		sim_era_name = ET.[name],
		sim_era_description = ET.[description]
	FROM [simsig].[TSIMERA] AS E
	INNER JOIN [simsig].[TERATYPE] AS ET ON E.[era_type_id] = ET.[id]
	WHERE
		[sim_id] = @sim_id
		AND (E.[testdata_id] = @testdata_id OR @testdata_id IS NULL) --Used to ensure if the connection is a test only records effected by the test are returned
END