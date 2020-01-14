/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Stored Procedures\simsig\simsig.USp_UPSERT_TLOCATION.sql
** Name:		simsig.USp_UPSERT_TLOCATION
** Desc:		Stored procedure create or update a SimSig location
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
CREATE PROCEDURE [simsig].[USp_UPSERT_TLOCATION]
	@id SMALLINT = 0 OUTPUT,
	@sim_id SMALLINT,
	@tiploc NVARCHAR(16),
	@name NVARCHAR(64),
	@simsig_code NVARCHAR(16),
	@simsig_entry_point BIT,
	@datetime DATETIMEOFFSET,
	@debug BIT = 0,
	@debug_session_id UNIQUEIDENTIFIER = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	--Variables
	DECLARE @debug_message NVARCHAR(2048);
	DECLARE @error_message NVARCHAR(2048);

	IF @id IS NULL SET @id = 0;

	--Set the @debug_session_id if in debug mode and @debug_session_id <NULL>
	IF @debug = 1 AND @debug_session_id IS NULL
	BEGIN
		SET @debug_session_id = NEWID();
	END

	IF @debug = 1
	BEGIN
		SET @debug_message = 'Executing [simsig].[Usp_UPSERT_TLOCATION] started.';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;

		SET @debug_message = 'Parameters passed: @id = ' + CONVERT(NVARCHAR(16),@id) + ' | @sim_id = ' + CONVERT(NVARCHAR(16),@sim_id) + ' | @tiploc = ' + ISNULL(@tiploc,'<NULL>') + ' | @name = ' + ISNULL(@name,'<NULL>') + ' | @simsig_code = ' + ISNULL(@simsig_code,'<NULL>') + ' | @simsig_entry_point = ' + ISNULL(CAST(@simsig_entry_point AS NVARCHAR(1)),'<NULL>') + ' | @datetime = ' + CASE WHEN @datetime IS NULL THEN '<NULL>' ELSE CONVERT(NVARCHAR(40), @datetime, 127) END + '.';
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

	IF NULLIF(@name,'') IS NULL 
	BEGIN;
		IF @debug = 1
		BEGIN
			SET @debug_message = 'No valid @name parameter was supplied';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		THROW 50000, 'No valid name was supplied for the location.', 1;
	END

	IF NULLIF(@simsig_code,'') IS NULL 
	BEGIN;
		IF @debug = 1
		BEGIN
			SET @debug_message = 'No valid @simsig_code parameter was supplied';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		THROW 50000, 'No valid simsig code was supplied for the location.', 1;
	END

	IF @simsig_entry_point IS NULL 
	BEGIN;
		IF @debug = 1
		BEGIN
			SET @debug_message = 'No valid @simsig_entry_point parameter was supplied';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		THROW 50000, 'No valid simsig entry point flag was supplied for the location.', 1;
	END

	IF NOT EXISTS (SELECT 1 FROM [simsig].[TSIM] WHERE [id] = @sim_id)
	BEGIN;
		IF @debug = 1
		BEGIN
			SET @debug_message = 'No the @sim_id parameter supplied does''t existing in [simsig].[TSIM]';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		THROW 50000, 'No valid simulation was supplied for the location.', 1;
	END

	IF @id = 0
	BEGIN
		IF @debug = 1
		BEGIN
			SET @debug_message = 'Checking to ensure the location doesn''t already exist';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		SET @id = ISNULL((SELECT [id] FROM [simsig].[TLOCATION] WHERE [name] = @name AND [sim_id] = @sim_id),0)

		IF @debug = 1 AND @id = 0
		BEGIN
			SET @debug_message = 'Location doesn''t exists - a new record will be created';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END
		ELSE IF @debug = 1 AND @id != 0
		BEGIN
			SET @debug_message = 'Location already exists ([id] = ' + CONVERT(NVARCHAR(16),@id) + ') - the existing record will be updated';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;
	END

	IF @id = 0
	BEGIN

		--Insert new record

		BEGIN TRAN TRAN_UPSERTLOCATION

		BEGIN TRY

			INSERT INTO [simsig].[TLOCATION]
			(
				[sim_id],
				[name],
				[tiploc],
				[simsig_code],
				[simsig_entry_point],
				[createdon],
				[createdby_id],
				[createdby_app_id],
				[modifiedon],
				[modifiedby_id],
				[modifiedby_app_id],
				[testdata_id]
			)
			VALUES
			(
				@sim_id,
				@name,
				@tiploc,
				@simsig_code,
				@simsig_entry_point,
				@datetime,
				@app_user_id,
				@app_id,
				@datetime,
				@app_user_id,
				@app_id,
				@testdata_id
			);

			COMMIT TRAN TRAN_UPSERTLOCATION

			SET @id = CAST(SCOPE_IDENTITY() AS SMALLINT);
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN TRAN_UPSERTLOCATION

			IF @debug = 1
			BEGIN
				SET @debug_message = 'An error has occurred trying to insert a record into [simsig].[TLOCATION] for ' + @name + ':- ' + ERROR_MESSAGE();
				EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
			END;

			SET @error_message = 'An error has occurred creating a location record for ' + @name + ':- ' + ERROR_MESSAGE();
			THROW 50000, @error_message, 1;
		END CATCH

		IF @debug = 1
		BEGIN
			SET @debug_message = 'New record created successfully ([id] = ' + CAST(@id AS NVARCHAR(16)) + ')';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;
	END
	ELSE
	BEGIN
		--Update the existing record

		BEGIN TRAN TRAN_UPSERTLOCATION

		BEGIN TRY
			UPDATE [simsig].[TLOCATION]
			SET
				[tiploc] = NULLIF(@tiploc,''),
				[name] = NULLIF(@name,''),
				[simsig_code] = @simsig_code,
				[simsig_entry_point] = @simsig_entry_point,
				[modifiedon] = @datetime,
				[modifiedby_id] = @app_user_id,
				[modifiedby_app_id] = @app_id
			WHERE
				[id] = @id;

			COMMIT TRAN TRAN_UPSERTLOCATION

			IF @debug = 1
			BEGIN
				SET @debug_message = 'Record [id] = ' + CAST(@id AS NVARCHAR(16)) + ' updated successfully';
				EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
			END;
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN TRAN_UPSERTLOCATION

			IF @debug = 1
			BEGIN
				SET @debug_message = 'An error has occured trying to update [simsig].[TLOCATION] record [id] = ' + CAST(@id AS NVARCHAR(16)) + ': - ' + ERROR_MESSAGE();
				EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
			END;

			SET @error_message = 'An error has occurred updating location [id] = ' + CAST(@id AS NVARCHAR(16)) + ':- ' + ERROR_MESSAGE();
			THROW 50000, @error_message, 1;
		END CATCH
	END

	IF @debug = 1
	BEGIN
		SET @debug_message = 'Executing [simsig].[Usp_UPSERT_TLOCATION] completed.';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
	END

	SET NOCOUNT OFF;
END