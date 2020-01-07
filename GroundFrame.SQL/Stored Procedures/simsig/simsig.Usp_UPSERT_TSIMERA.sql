/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Stored Procedures\simsig\simsig.Usp_UPSERT_TSIMERA.sql
** Name:		simsig.Usp_UPSERT_TSIMERA
** Desc:		Stored procedure create or update a SimSig simulation era
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
CREATE PROCEDURE [simsig].[Usp_UPSERT_TSIMERA]
	@id SMALLINT = 0 OUTPUT,
	@sim_id SMALLINT,
	@name NVARCHAR(128),
	@description NVARCHAR(2048),
	@era_type_id TINYINT,
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
		SET @debug_message = 'Executing [simsig].[Usp_UPSERT_TSIMERA] started.';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;

		SET @debug_message = 'Parameters passed: @id = ' + CONVERT(NVARCHAR(16),@id) + ' | @sim_id = ' + CONVERT(NVARCHAR(16),@sim_id) + ' | @name = ' + ISNULL(@name,'<NULL>') + ' | @description = ' + ISNULL(@description,'<NULL>') + ' | @era_type_id = ' + CONVERT(NVARCHAR(16),@era_type_id) + '.';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
	END

	--Variables--
	DECLARE @logged_in BIT = ISNULL(CONVERT(BIT,SESSION_CONTEXT(N'logged_in')),0); 
	DECLARE @app_user_id INT = ISNULL(CONVERT(INT,SESSION_CONTEXT(N'app_user')),0); 
	DECLARE @testdata_id UNIQUEIDENTIFIER = CONVERT(UNIQUEIDENTIFIER,SESSION_CONTEXT(N'testdata_id'))

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

	--Check user has editor or sys admin role

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

		THROW 50000, 'No valid name was supplied for the simulation era.', 1;
	END

	IF NOT EXISTS (SELECT 1 FROM [simsig].[TSIM] WHERE [id] = @sim_id)
	BEGIN;
		IF @debug = 1
		BEGIN
			SET @debug_message = 'The @sim_id supplied doesn''t exist in [simsig].[TSIM]';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		THROW 50000, 'A valid simulation wasn''t suppled for the simulation era.', 1;
	END

	IF NOT EXISTS (SELECT 1 FROM [simsig].[TERATYPE] WHERE [id] = @era_type_id)
	BEGIN;
		IF @debug = 1
		BEGIN
			SET @debug_message = 'The @era_type_id supplied doesn''t exist in [simsig].[TERATYPE]';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		THROW 50000, 'A valid era type wasn''t suppled for the simulation era.', 1;
	END

	IF @id = 0
	BEGIN
		IF @debug = 1
		BEGIN
			SET @debug_message = 'Checking to ensure the simulation era doesn''t already exist and the record active';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		SET @id = ISNULL((SELECT [id] FROM [simsig].[TSIMERA] WHERE [name] = @name AND [sim_id] = @sim_id),0)

		IF @debug = 1 AND @id = 0
		BEGIN
			SET @debug_message = 'Simulation era doesn''t exists - a new record will be created';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END
		ELSE IF @debug = 1 AND @id != 0
		BEGIN
			SET @debug_message = 'Simulation era already exists ([id] = ' + CONVERT(NVARCHAR(16),@id) + ') - the existing record will be updated';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;
	END

	IF @id = 0
	BEGIN
		--Insert new record

		BEGIN TRAN TRAN_UPSERTSIMERA

		BEGIN TRY
			INSERT INTO [simsig].[TSIMERA]
			(
				[sim_id],
				[name],
				[description],
				[era_type_id],
				[testdata_id]
			)
			VALUES
			(
				@sim_id,
				@name,
				@description,
				@era_type_id,
				@testdata_id
			);

			COMMIT TRAN TRAN_UPSERTSIMERA

			SET @id = CAST(SCOPE_IDENTITY() AS SMALLINT);
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN TRAN_UPSERTSIMERA

			IF @debug = 1
			BEGIN
				SET @debug_message = 'An error has occurred trying to insert a record into [app].[TSIMERA] for ' + @name + ':- ' + ERROR_MESSAGE();
				EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
			END;

			SET @error_message = 'An error has occurred creating a simulation era record for ' + @name + ':- ' + ERROR_MESSAGE();
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

		BEGIN TRAN TRAN_UPSERTSIMERA

		BEGIN TRY
			UPDATE [simsig].[TSIMERA]
			SET
				[name] = NULLIF(@name,''),
				[description] = NULLIF(@description,''),
				[era_type_id] = @era_type_id
			WHERE
				[id] = @id;

			COMMIT TRAN TRAN_UPSERTSIMERA

			IF @debug = 1
			BEGIN
				SET @debug_message = 'Record [id] = ' + CAST(@id AS NVARCHAR(16)) + ' updated successfully';
				EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
			END;
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN TRAN_UPSERTSIMERA

			IF @debug = 1
			BEGIN
				SET @debug_message = 'An error has occured trying to update [app].[TSIMERA] record [id] = ' + CAST(@id AS NVARCHAR(16)) + ': - ' + ERROR_MESSAGE();
				EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
			END;

			SET @error_message = 'An error has occurred updating simulation era [id] = ' + CAST(@id AS NVARCHAR(16)) + ':- ' + ERROR_MESSAGE();
			THROW 50000, @error_message, 1;
		END CATCH
	END

	IF @debug = 1
	BEGIN
		SET @debug_message = 'Executing [simsig].[Usp_UPSERT_TSIMERA] completed.';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
	END

	SET NOCOUNT OFF;
END