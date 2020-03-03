/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Stored Procedures\simsig\simsig.Usp_DELETE_TPATHEDGE.sql
** Name:		simsig.Usp_DELETE_TPATHEDGE
** Desc:		Stored procedure to delete a SimSig path edge
** Unit Test:	
** Auth:		Tim Caceres
** Date:		2019-12-23
**************************
** Change History
**************************
** Ver	Date		Author		Description 
** ---  --------	-------		------------------------------------
** 1    2019-12-23	TC			Initial Script creation
**
*******************************/

CREATE PROCEDURE [simsig].[Usp_DELETE_TPATHEDGE]
	@from_locationnode_id INT,
	@to_locationnode_id INT,
	@debug BIT = 0,
	@debug_session_id UNIQUEIDENTIFIER = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	--Variables
	DECLARE @debug_message NVARCHAR(2048);
	DECLARE @error_message NVARCHAR(2048);

	--Set the @debug_session_id if in debug mode and @debug_session_id <NULL>
	IF @debug = 1 AND @debug_session_id IS NULL
	BEGIN
		SET @debug_session_id = NEWID();
	END

	IF @debug = 1
	BEGIN
		SET @debug_message = 'Executing [simsig].[Usp_DELETE_TPATHEDGE] started.';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;

		SET @debug_message = 'Parameters passed: @from_locationnode_id = ' + CONVERT(NVARCHAR(16),@from_locationnode_id) + ' | @to_locationnode_id = ' + CONVERT(NVARCHAR(16), @to_locationnode_id) + '.';
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

	--Check user has editor or admin role

	IF ISNULL((SELECT [role_bitmap] FROM [app].[TUSER] WHERE [id] = @app_user_id),0) & 6 = 0
	BEGIN;
		IF @debug = 1
		BEGIN
			SET @debug_message = 'The user isn''t in the editor or admin role. Check their permissions';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;
		
		THROW 50000, 'The user does not have permission to perform this action.', 1;
	END;

	BEGIN TRAN TRAN_DELETEPATHEDGE

	BEGIN TRY
		DELETE E
		FROM simsig.TPATHEDGE AS E
		INNER JOIN simsig.TLOCATIONNODE AS FLN ON E.$from_id = FLN.$node_id
		INNER JOIN simsig.TLOCATIONNODE AS TLN ON E.$to_id = TLN.$node_id
		WHERE
			FLN.id = @from_locationnode_id
			AND TLN.id = @to_locationnode_id

		COMMIT TRAN TRAN_DELETEPATHEDGE;
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN TRAN_DELETEPATHEDGE;

		IF @debug = 1
		BEGIN
			SET @debug_message = 'An error has occured trying to delete [app].[TPATHEDGE] from location node [id] = ' + CAST(@from_locationnode_id AS NVARCHAR(16)) + ' to location node [id] = ' + CAST(@to_locationnode_id AS NVARCHAR(16)) + ': - ' + ERROR_MESSAGE();
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		SET @error_message = 'An error has occurred deleting path edge from location node [id] = ' + CAST(@from_locationnode_id AS NVARCHAR(16)) + ' to location node [id] = ' + CAST(@to_locationnode_id AS NVARCHAR(16)) + ': - ' + ERROR_MESSAGE();
		THROW 50000, @error_message, 1;
	END CATCH

	IF @debug = 1
	BEGIN
		SET @debug_message = 'Executing [simsig].[Usp_DELETE_TPATHEDGE] completed.';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
	END

	SET NOCOUNT OFF;
END