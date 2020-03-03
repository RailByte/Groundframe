/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Stored Procedures\simsig\simsig.Usp_GET_PATHEDGE_BY_FROM_LOCATIONNODE.sql
** Name:		simsig.Usp_GET_PATHEDGE_BY_FROM_LOCATIONNODE
** Desc:		Stored procedure to get a all the path edges from the supplied location node
** Unit Test:	
** Auth:		Tim Caceres
** Date:		2020-03-03
**************************
** Change History
**************************
** Ver	Date		Author		Description 
** ---  --------	-------		------------------------------------
** 1    2019-12-12	TC			Initial Script creation
**
*******************************/
CREATE PROCEDURE [simsig].[Usp_GET_PATHEDGE_BY_FROM_LOCATIONNODE]
	@from_locationnode_id INT,
	@debug BIT = 0,
	@debug_session_id UNIQUEIDENTIFIER = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	--Variables
	DECLARE @debug_message NVARCHAR(2048);
	DECLARE @error_message NVARCHAR(2048);
	DECLARE @testdata_id UNIQUEIDENTIFIER = CONVERT(UNIQUEIDENTIFIER,SESSION_CONTEXT(N'testdata_id'))

	--Set the @debug_session_id if in debug mode and @debug_session_id <NULL>
	IF @debug = 1 AND @debug_session_id IS NULL
	BEGIN
		SET @debug_session_id = NEWID();
	END

	IF @debug = 1
	BEGIN
		SET @debug_message = 'Executing [simsig].[Usp_GET_PATHEDGE_BY_FROM_LOCATIONNODE] started.';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;

		SET @debug_message = 'Parameters passed: @from_locationnode_id = ' + CONVERT(NVARCHAR(16),@from_locationnode_id) + '.';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
	END

	--Variables--
	DECLARE @logged_in BIT = ISNULL(CONVERT(BIT,SESSION_CONTEXT(N'logged_in')),0); 
	DECLARE @app_user_id INT = ISNULL(CONVERT(INT,SESSION_CONTEXT(N'app_user')),0); 

	--Check user is logged in
	IF @logged_in = 0
	BEGIN;
		IF @debug = 1
		BEGIN
			SET @debug_message = 'The user isn''t logged in. Check that [common].[Usp_SET_SESSIONCONTEXT] has fired when the connection to the database was made';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;
		
		THROW 50000, 'The user is not logged in.', 1;
	END;

	--Paramter checks

	
	BEGIN TRY
		WITH CTE
		AS
		(
		SELECT
			[from_locationnode_id] = FLN.id,
			[from_location_id] = FLN.location_id,
			[from_platform] = FLN.[simsig_platform_code],
			[from_line] = FLN.[simsig_line],
			[from_path] = FLN.[simsig_path],
			[to_locationnode_id] = TLN.id,
			[to_location_id] = TLN.location_id,
			[to_platform] = TLN.[simsig_platform_code],
			[to_line] = TLN.[simsig_line],
			[to_path] = TLN.[simsig_path],
			PE.[simsig_elec_bitmap],
			PE.[path_direction],
			PE.[length]
		FROM [simsig].[TPATHEDGE] AS PE, [simsig].[TLOCATIONNODE] AS FLN, [simsig].[TLOCATIONNODE] AS TLN
		WHERE
			MATCH(FLN-(PE)->TLN)
			AND FLN.id = @from_locationnode_id
		)
		SELECT
			CTE.[from_locationnode_id],
			CTE.[from_location_id],
			[from_location] = FL.[name],
			CTE.[from_platform],
			CTE.[from_line],
			CTE.[from_path],
			[to_location] = TL.[name],
			CTE.[to_locationnode_id],
			CTE.[to_location_id],
			CTE.[to_platform],
			CTE.[to_line],
			CTE.[to_path],
			CTE.[simsig_elec_bitmap],
			CTE.[path_direction],
			CTE.[length]
		FROM CTE
		INNER JOIN [simsig].[TLOCATION] AS FL ON CTE.[from_location_id] = FL.[id]
		INNER JOIN [simsig].[TLOCATION] AS TL ON CTE.[to_location_id] = TL.[id]
	END TRY
	BEGIN CATCH
		IF @debug = 1
		BEGIN
			SET @debug_message = 'An error has occured trying to retrieve [simsig].[TPATHEDGE] records for from location node [id] = ' + CAST(@from_locationnode_id AS NVARCHAR(16)) + ': - ' + ERROR_MESSAGE();
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		SET @error_message = 'An error has occurred retrieving path edges for from location node [id] = ' + CAST(@from_locationnode_id AS NVARCHAR(16)) + ':- ' + ERROR_MESSAGE();
		THROW 50000, @error_message, 1;
	END CATCH

	IF @debug = 1
	BEGIN
		SET @debug_message = 'Executing [simsig].[Usp_GET_PATHEDGE_BY_FROM_LOCATIONNODE] completed.';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
	END

	SET NOCOUNT OFF;
END