/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Stored Procedures\simsig\simsig.USp_UPSERT_TLOCATIONNODE.sql
** Name:		simsig.USp_UPSERT_TLOCATIONNODE
** Desc:		Stored procedure create or update a SimSig location node
** Unit Test:	
** Auth:		Tim Caceres
** Date:		2019-12-12
**************************
** Change History
**************************
** Ver	Date		Author		Description 
** ---  --------	-------		------------------------------------
** 1    2020-01-16	TC			Initial Script creation
**
*******************************/
CREATE PROCEDURE [simsig].[USp_UPSERT_TLOCATIONNODE]
	@id INT = 0 OUTPUT,
	@sim_id SMALLINT,
	@location_id INT,
	@simera_id SMALLINT,
	@version_id SMALLINT,
	@simsig_platform_code VARCHAR(4),
	@simsig_elec_bitmap TINYINT,
	@location_type_id TINYINT,
	@length SMALLINT,
	@freight_only BIT,
	@simsig_line VARCHAR(4),
	@simsig_path VARCHAR(4),
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
		SET @debug_message = 'Executing [simsig].[Usp_UPSERT_TLOCATIONNODE] started.';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;

		SET @debug_message = 'Parameters passed: @id = ' + ISNULL(CONVERT(NVARCHAR(16),@id), '<NULL>') 
		+ ' | @sim_id = ' + ISNULL(CONVERT(NVARCHAR(16),@sim_id), '<NULL>') 
		+ ' | @location_id = ' + ISNULL(CONVERT(NVARCHAR(16),@location_id), '<NULL>') 
		+ ' | @simera_id = ' + ISNULL(CONVERT(NVARCHAR(16),@simera_id), '<NULL>') 
		+ ' | @version_id = ' + ISNULL(CONVERT(NVARCHAR(16),@version_id), '<NULL>') 
		+ ' | @simsig_platform_code = ' + ISNULL(@simsig_platform_code, '<NULL>')
		+ ' | @simsig_elec_bitmap = ' + ISNULL(CONVERT(NVARCHAR(4),@simsig_elec_bitmap), '<NULL>') 
		+ ' | @location_type_id = ' + ISNULL(CONVERT(NVARCHAR(16),@version_id), '<NULL>') 
		+ ' | @freight_only = ' + ISNULL(CONVERT(CHAR(1),@version_id), '<NULL>') 
		+ ' | @length = ' + ISNULL(CONVERT(NVARCHAR(16),@length), '<NULL>') 
		+ ' | @simsig_line = ' + ISNULL(@simsig_line, '<NULL>') 
		+ ' | @simsig_path = ' + ISNULL(@simsig_path, '<NULL>') 
		+ ' | @datetime = ' + CASE WHEN @datetime IS NULL THEN '<NULL>' ELSE CONVERT(NVARCHAR(40), @datetime, 127) END 
		+ '.';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
	END

	--Clean Up ID
	IF @id IS NULL SET @id = 0;

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

	--Check user has admin / editor role

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

	IF NOT EXISTS (SELECT 1 FROM [simsig].[TSIM] WHERE [id] = @sim_id)
	BEGIN;
		IF @debug = 1
		BEGIN
			SET @debug_message = 'No valid @sim_id parameter was supplied';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		THROW 50000, 'No valid simulation was supplied for the location node.', 1;
	END

	IF NOT EXISTS (SELECT 1 FROM [simsig].[TSIMERA] WHERE [sim_id] = @sim_id AND [id] = @simera_id)
	BEGIN;
		IF @debug = 1
		BEGIN
			SET @debug_message = 'No valid @simera_id parameter was supplied';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		THROW 50000, 'No valid simulation era was supplied for the location node.', 1;
	END

	IF NOT EXISTS (SELECT 1 FROM [simsig].[TLOCATION] WHERE [sim_id] = @sim_id AND [id] = @location_id)
	BEGIN;
		IF @debug = 1
		BEGIN
			SET @debug_message = 'No valid @location_id parameter was supplied';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		THROW 50000, 'No valid location was supplied for the location node.', 1;
	END

	IF NOT EXISTS (SELECT 1 FROM [simsig].[TLOCATIONTYPE] WHERE [id] = @location_type_id)
	BEGIN;
		IF @debug = 1
		BEGIN
			SET @debug_message = 'No valid @location_type_id parameter was supplied';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		THROW 50000, 'No valid location type was supplied for the location node.', 1;
	END

	IF @length < 0
	BEGIN;
		IF @debug = 1
		BEGIN
			SET @debug_message = 'No valid @length parameter was supplied. Must be <NULL> or greater than zero';
			EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		THROW 50000, 'No valid length was supplied for the location node.', 1;
	END

	IF @id = 0
	BEGIN

		--Insert new record

		BEGIN TRAN TRAN_UPSERTLOCATIONNODE

		BEGIN TRY

			INSERT INTO [simsig].[TLOCATIONNODE]
			(
				[sim_id],
				[location_id],
				[simera_id],
				[version_id],
				[simsig_platform_code],
				[simsig_elec_bitmap],
				[location_type_id],
				[length],
				[freight_only],
				[simsig_line],
				[simsig_path],
				[createdby_id],
				[createdby_app_id],
				[createdon],
				[modifiedby_id],
				[modifiedby_app_id],
				[modifiedon],
				[testdata_id]
			)
			VALUES
			(
				@sim_id,
				@location_id,
				@simera_id,
				@version_id,
				NULLIF(@simsig_platform_code,''),
				@simsig_elec_bitmap,
				@location_type_id,
				NULLIF(@length,0),
				ISNULL(@freight_only,0),
				NULLIF(@simsig_line,''),
				NULLIF(@simsig_path,''),
				@app_user_id,
				@app_id,
				@datetime,
				@app_user_id,
				@app_id,
				@datetime,
				@testdata_id
			);

			COMMIT TRAN TRAN_UPSERTLOCATIONNODE

			SET @id = CAST(SCOPE_IDENTITY() AS SMALLINT);
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN TRAN_UPSERTLOCATIONNODE

			IF @debug = 1
			BEGIN
				SET @debug_message = 'An error has occurred trying to insert a record into [simsig].[TLOCATIONNODE] for sim_id' + CAST(@sim_id AS NVARCHAR(8)) + ':- ' + ERROR_MESSAGE();
				EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
			END;

			SET @error_message = 'An error has occurred creating a location node record for Simulation' + CAST(@sim_id AS NVARCHAR(8))  + ':- ' + ERROR_MESSAGE();
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

		BEGIN TRAN TRAN_UPSERTLOCATIONNODE

		BEGIN TRY
			UPDATE [simsig].[TLOCATIONNODE]
			SET
				[sim_id] = @sim_id,
				[location_id] = @location_id,
				[simera_id] = @simera_id,
				[version_id] = @version_id,
				[simsig_platform_code] = NULLIF(@simsig_platform_code,''),
				[simsig_elec_bitmap] = @simsig_elec_bitmap,
				[location_type_id] = @location_type_id,
				[length] = NULLIF(@length,0),
				[freight_only] = ISNULL(@freight_only,0),
				[simsig_line] = NULLIF(@simsig_line,''),
				[simsig_path] = NULLIF(@simsig_path,''),
				[modifiedon] = @datetime,
				[modifiedby_id] = @app_user_id,
				[modifiedby_app_id] = @app_id
			WHERE
				[id] = @id;

			COMMIT TRAN TRAN_UPSERTLOCATIONNODE

			IF @debug = 1
			BEGIN
				SET @debug_message = 'Record [id] = ' + CAST(@id AS NVARCHAR(16)) + ' updated successfully';
				EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
			END;
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN TRAN_UPSERTLOCATIONNODE

			IF @debug = 1
			BEGIN
				SET @debug_message = 'An error has occured trying to update [simsig].[TLOCATIONNODE] record [id] = ' + CAST(@id AS NVARCHAR(16)) + ': - ' + ERROR_MESSAGE();
				EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
			END;

			SET @error_message = 'An error has occurred updating location node [id] = ' + CAST(@id AS NVARCHAR(16)) + ':- ' + ERROR_MESSAGE();
			THROW 50000, @error_message, 1;
		END CATCH
	END

	IF @debug = 1
	BEGIN
		SET @debug_message = 'Executing [simsig].[Usp_UPSERT_TLOCATIONNODE] completed.';
		EXEC [audit].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
	END

	SET NOCOUNT OFF;
END