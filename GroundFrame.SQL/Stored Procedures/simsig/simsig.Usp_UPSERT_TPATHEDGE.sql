/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Stored Procedures\simsig\simsig.Usp_UPSERT_TPATHEDGE.sql
** Name:		simsig.Usp_UPSERT_TPATHEDGE
** Desc:		Stored procedure create or update a SimSig path edge
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
CREATE PROCEDURE [simsig].[Usp_UPSERT_TPATHEDGE]
	@from_locationnode_id INT,
	@to_locationnode_id INT,
	@simsig_elec_bitmap TINYINT,
	@path_direction TINYINT,
	@length SMALLINT,
	@datetime DATETIMEOFFSET,
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
		SET @debug_message = 'Executing [simsig].[Usp_UPSERT_TPATHEDGE] started.';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;

		SET @debug_message = 'Parameters passed: @from_locationnode_id = ' + CONVERT(NVARCHAR(16),@from_locationnode_id) + ' | @to_locationnode_id = ' + CONVERT(NVARCHAR(16),@to_locationnode_id) + ' | @simsig_elec_bitmap = ' + CONVERT(NVARCHAR(3),@simsig_elec_bitmap) + ' | @path_direction = ' + CONVERT(NVARCHAR(1),@path_direction) + ' | @length = ' + CONVERT(NVARCHAR(16),@length) + ' | @datetime = ' + CASE WHEN @datetime IS NULL THEN '<NULL>' ELSE CONVERT(NVARCHAR(40), @datetime, 127) END  + '.';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
	END

	--Variables--
	DECLARE @logged_in BIT = ISNULL(CONVERT(BIT,SESSION_CONTEXT(N'logged_in')),0); 
	DECLARE @app_user_id INT = ISNULL(CONVERT(INT,SESSION_CONTEXT(N'app_user')),0); 
	DECLARE @app_id SMALLINT = ISNULL(CONVERT(SMALLINT,SESSION_CONTEXT(N'application')),0); 
	DECLARE @testdata_id UNIQUEIDENTIFIER = CONVERT(UNIQUEIDENTIFIER,SESSION_CONTEXT(N'testdata_id'))

	--Set Default DateTime

	IF @datetime IS NULL SET @datetime = SYSDATETIMEOFFSET();

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

	--Check user has admin role

	IF ISNULL((SELECT [role_bitmap] FROM [app].[TUSER] WHERE [id] = @app_user_id),0) & 6 = 0
	BEGIN;
		IF @debug = 1
		BEGIN
			SET @debug_message = 'The user isn''t in the editor or admin role. Check their permissions';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;
		
		THROW 50000, 'The user does not have permission to perform this action.', 1;
	END;

	--Paramter checks

	IF @debug = 1
	BEGIN
		SET @debug_message = 'Running paramter checks';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
	END;

	IF NOT EXISTS (SELECT 1 FROM [simsig].[TLOCATIONNODE] WHERE [id] = @from_locationnode_id)
	BEGIN;
		IF @debug = 1
		BEGIN
			SET @debug_message = 'Invalid @from_locationnode_id parameter was supplied';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		THROW 50000, 'Invalid from locationnode supplied', 1;
	END

	IF NOT EXISTS (SELECT 1 FROM [simsig].[TLOCATIONNODE] WHERE [id] = @to_locationnode_id)
	BEGIN;
		IF @debug = 1
		BEGIN
			SET @debug_message = 'Invalid @to_locationnode_id parameter was supplied';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		THROW 50000, 'Invalid to locationnode supplied', 1;
	END

	IF EXISTS (SELECT 1 FROM [simsig].[TLOCATIONNODE] AS F WHERE F.[id] = @from_locationnode_id AND NOT EXISTS (SELECT 1 FROM [simsig].[TLOCATIONNODE] AS T WHERE T.[id] = @to_locationnode_id AND F.sim_id = T.sim_id AND F.simera_id = T.simera_id AND F.version_id = T.version_id)) 
	BEGIN;
		IF @debug = 1
		BEGIN
			SET @debug_message = 'The 2 locations supplied are for different simulation / era / version';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		THROW 50000, 'The locations supplied must be for the same simulation / era / version.', 1;
	END

	BEGIN TRAN TRAN_UPSERTPATHEDGE;

	BEGIN TRY
		MERGE [simsig].[TPATHEDGE] AS T
		USING
		(
			(SELECT 
				[from_id] = @from_locationnode_id,
				[to_id] = @to_locationnode_id,
				[simsig_elec_bitmap] = @simsig_elec_bitmap, 
				[path_direction] = @path_direction, 
				[length] = @length
			) AS E ([from_id], [to_id], [simsig_elec_bitmap], [path_direction], [length])
			INNER JOIN [simsig].[TLOCATIONNODE] AS FLN ON E.[from_id] = FLN.[id]
			INNER JOIN [simsig].[TLOCATIONNODE] AS TLN ON E.[to_id] = TLN.[id]
		)
		ON MATCH(FLN-(T)->TLN)
		WHEN MATCHED
		THEN UPDATE SET [T].[simsig_elec_bitmap] = E.[simsig_elec_bitmap], T.[path_direction] = E.[path_direction], T.[length] = E.[length]
		WHEN NOT MATCHED BY TARGET
		THEN INSERT ($from_id, $to_id, [simsig_elec_bitmap], [path_direction], [length])
		VALUES (FLN.$node_id, TLN.$node_id, E.[simsig_elec_bitmap], E.[path_direction], E.[length]);

		COMMIT TRAN TRAN_UPSERTPATHEDGE;

		IF @debug = 1
		BEGIN
			SET @debug_message = 'Path Edge record for from_id = ' + CONVERT(NVARCHAR(16),@from_locationnode_id) + ' and to_id = ' + CONVERT(NVARCHAR(16),@to_locationnode_id) + ' updated successfully';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN TRAN_UPSERTPATHEDGE;

		IF @debug = 1
		BEGIN
			SET @debug_message = 'An error has occurred trying to merge a record into [simsig].[TPATHEDGE] for from_id = ' + CONVERT(NVARCHAR(16),@from_locationnode_id) + ' and to_id = ' + CONVERT(NVARCHAR(16),@to_locationnode_id) + ':- ' + ERROR_MESSAGE();
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		SET @error_message = 'An error has occurred merging a path end record for from_id = ' + CONVERT(NVARCHAR(16),@from_locationnode_id) + ' and to_id = ' + CONVERT(NVARCHAR(16),@to_locationnode_id) + ':- ' + ERROR_MESSAGE();
		THROW 50000, @error_message, 1;
	END CATCH

	IF @debug = 1
	BEGIN
		SET @debug_message = 'Executing [simsig].[Usp_UPSERT_TPATHEDGE] completed.';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
	END

	SET NOCOUNT OFF;
END
GO