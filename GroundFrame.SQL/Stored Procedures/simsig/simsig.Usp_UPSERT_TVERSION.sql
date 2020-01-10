/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Stored Procedures\simsig\simsig.USp_UPSERT_TVERSION.sql
** Name:		simsig.USp_UPSERT_TVERSION
** Desc:		Stored procedure create or update a SimSig verion
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
CREATE PROCEDURE [simsig].[USp_UPSERT_TVERSION]
	@id SMALLINT = 0 OUTPUT,
	@name NVARCHAR(128),
	@description NVARCHAR(2048),
	@version NUMERIC(4,1),
	@version_status_id TINYINT,
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
		SET @debug_message = 'Executing [simsig].[Usp_UPSERT_TVERSION] started.';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;

		SET @debug_message = 'Parameters passed: @id = ' + CONVERT(NVARCHAR(16),@id) + ' | @name = ' + ISNULL(@name,'<NULL>') + ' | @description = ' + ISNULL(@description,'<NULL>') + ' | @version = ' + ISNULL(CAST(@version AS NVARCHAR(8)),'<NULL>') + ' | @version_status_id = ' + ISNULL(CAST(@version_status_id AS NVARCHAR(8)),'<NULL>') + '.';
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

	--Check user has admin role

	IF ISNULL((SELECT [role_bitmap] FROM [app].[TUSER] WHERE [id] = @app_user_id),0) & 4 = 0
	BEGIN;
		IF @debug = 1
		BEGIN
			SET @debug_message = 'The user isn''t in the admin role. Check their permissions';
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

		THROW 50000, 'No valid name was supplied for the simulation.', 1;
	END

	IF NOT EXISTS (SELECT 1 FROM [simsig].[TVERSIONSTATUS] WHERE [id] = @version_status_id)
	BEGIN;
		IF @debug = 1
		BEGIN
			SET @debug_message = 'No valid @version_status_id parameter was supplied';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		THROW 50000, 'No valid version_status_id was supplied for the simulation.', 1;
	END

	IF @id = 0
	BEGIN
		IF @debug = 1
		BEGIN
			SET @debug_message = 'Checking to ensure the version doesn''t already exist and the record active';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		SET @id = ISNULL((SELECT [id] FROM [simsig].[TVERSION] WHERE [name] = @name),0)

		IF @debug = 1 AND @id = 0
		BEGIN
			SET @debug_message = 'Version doesn''t exists - a new record will be created';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END
		ELSE IF @debug = 1 AND @id != 0
		BEGIN
			SET @debug_message = 'Version already exists ([id] = ' + CONVERT(NVARCHAR(16),@id) + ') - the existing record will be updated';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;
	END

	IF @id = 0
	BEGIN
		SET @version_status_id = 2;
		DECLARE @previous_id SMALLINT = ISNULL((SELECT [id] FROM [simsig].[TVERSION] WHERE [simsig_version_to] IS NULL),0);

		--Get the previous record (if a previous record exists)		

		IF @previous_id = 0 AND (SELECT COUNT(*) FROM [simsig].[TVERSION]) > 0
		BEGIN
			IF @debug = 1
			BEGIN
				SET @debug_message = 'Couldn''t ascertain the previous version to close off.';
				EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
			END;

			THROW 50000, 'An error has occurred creating new version. An active previous version couldn''t be found', 1;
		END

		IF @debug = 1
		BEGIN
			SET @debug_message = 'Checking to ensure that new version is <= to the latest version';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		IF @previous_id != 0 AND (SELECT COUNT(*) FROM [simsig].[TVERSION]) > 0
		BEGIN
			DECLARE @previous_version NUMERIC(4,1) = (SELECT [simsig_version_from] FROM [simsig].[TVERSION] WHERE [id] = @previous_id);

			IF @version <= @previous_version
			BEGIN
				IF @debug = 1
				BEGIN
					SET @debug_message = 'An attempt was made to split the existing latest version.';
					EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
				END;

				THROW 50000, 'An error has occurred creating a new version. You cannot specify a new version equal or less to the exsting version', 1;
			END
		END

		--Insert new record

		BEGIN TRAN TRAN_UPSERTVERSION

		BEGIN TRY

			--Close previous version

			IF @previous_id != 0
			BEGIN
				UPDATE [simsig].[TVERSION]
				SET [simsig_version_to] = (@version - 0.1)
				WHERE
					[id] = @previous_id;
			END

			INSERT INTO [simsig].[TVERSION]
			(
				[name],
				[description],
				[simsig_version_from],
				[version_status_id],
				[testdata_id]
			)
			VALUES
			(
				@name,
				@description,
				@version,
				2, --Versions can only be created with a 'Development' status
				@testdata_id
			);

			COMMIT TRAN TRAN_UPSERTVERSION

			SET @id = CAST(SCOPE_IDENTITY() AS SMALLINT);
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN TRAN_UPSERTVERSION

			IF @debug = 1
			BEGIN
				SET @debug_message = 'An error has occurred trying to insert a record into [app].[TVERSION] for ' + @name + ':- ' + ERROR_MESSAGE();
				EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
			END;

			SET @error_message = 'An error has occurred creating a version record for ' + @name + ':- ' + ERROR_MESSAGE();
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

		BEGIN TRAN TRAN_UPSERTVERSION

		BEGIN TRY
			UPDATE [simsig].[TVERSION]
			SET
				[description] = NULLIF(@description,''),
				[name] = NULLIF(@name,''),
				[version_status_id] = @version_status_id
			WHERE
				[id] = @id;

			COMMIT TRAN TRAN_UPSERTVERSION

			IF @debug = 1
			BEGIN
				SET @debug_message = 'Record [id] = ' + CAST(@id AS NVARCHAR(16)) + ' updated successfully';
				EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
			END;
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN TRAN_UPSERTVERSION

			IF @debug = 1
			BEGIN
				SET @debug_message = 'An error has occured trying to update [app].[TVERSION] record [id] = ' + CAST(@id AS NVARCHAR(16)) + ': - ' + ERROR_MESSAGE();
				EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
			END;

			SET @error_message = 'An error has occurred updating version [id] = ' + CAST(@id AS NVARCHAR(16)) + ':- ' + ERROR_MESSAGE();
			THROW 50000, @error_message, 1;
		END CATCH
	END

	IF @debug = 1
	BEGIN
		SET @debug_message = 'Executing [simsig].[Usp_UPSERT_TVERSION] completed.';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
	END

	SET NOCOUNT OFF;
END