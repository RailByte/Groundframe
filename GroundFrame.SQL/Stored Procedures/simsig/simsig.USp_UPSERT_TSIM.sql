/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Stored Procedures\simsig\simsig.USp_UPSERT_TSIM.sql
** Name:		simsig.USp_UPSERT_TSIM
** Desc:		Stored procedure create or update a SimSig simulation
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
CREATE PROCEDURE [simsig].[USp_UPSERT_TSIM]
	@id SMALLINT = 0 OUTPUT,
	@name NVARCHAR(128),
	@description NVARCHAR(2048),
	@simsig_wiki_link NVARCHAR(512),
	@simsig_code NVARCHAR(32),
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
		SET @debug_message = 'Executing [simsig].[Usp_UPSERT_TSIM] started.';
		EXEC [debug].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;

		SET @debug_message = 'Parameters passed: @id = ' + CONVERT(NVARCHAR(16),@id) + ' | @name = ' + ISNULL(@name,'<NULL>') + ' | @description = ' + ISNULL(@description,'<NULL>') + ' | @simsig_wiki_link = ' + ISNULL(@simsig_wiki_link,'<NULL>') + ' | @simsig_code = ' + ISNULL(@simsig_code,'<NULL>') + ' | @datetime = ' + CASE WHEN @datetime IS NULL THEN '<NULL>' ELSE CONVERT(NVARCHAR(40), @datetime, 127) END + '.';
		EXEC [debug].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
	END

	--Variables--
	DECLARE @logged_in BIT = ISNULL(CONVERT(BIT,SESSION_CONTEXT(N'logged_in')),0); 
	DECLARE @app_user_id INT = ISNULL(CONVERT(INT,SESSION_CONTEXT(N'app_user')),0); 

	--Set Default DateTime

	IF @datetime IS NULL SET @datetime = SYSDATETIMEOFFSET();

	--Check user is logged in
	IF @logged_in = 0
	BEGIN;
		IF @debug = 1
		BEGIN
			SET @debug_message = 'The user isn''t logged in. Check that [common].[Usp_SET_SESSIONCONTEXT] has fired when the connection to the database was made';
			EXEC [debug].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;
		
		THROW 50000, 'The user is not logged in.', 1;
	END;

	--Paramter checks

	IF @debug = 1
	BEGIN
		SET @debug_message = 'Running paramter checks';
		EXEC [debug].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
	END;

	IF NULLIF(@name,'') IS NULL 
	BEGIN;
		IF @debug = 1
		BEGIN
			SET @debug_message = 'No valid @name parameter was supplied';
			EXEC [debug].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		THROW 50000, 'No valid name was supplied for the simulation.', 1;
	END

	IF NULLIF(@simsig_code,'') IS NULL 
	BEGIN;
		IF @debug = 1
		BEGIN
			SET @debug_message = 'No valid @simsig_code parameter was supplied';
			EXEC [debug].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		THROW 50000, 'No valid simsig_code was supplied for the simulation.', 1;
	END

	IF @id = 0
	BEGIN
		IF @debug = 1
		BEGIN
			SET @debug_message = 'Checking to ensure the simulation doesn''t already exist and the record active';
			EXEC [debug].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		SET @id = ISNULL((SELECT [id] FROM [simsig].[TSIM] WHERE [name] = @name),0)

		IF @debug = 1 AND @id = 0
		BEGIN
			SET @debug_message = 'Simulation doesn''t exists - a new record will be created';
			EXEC [debug].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END
		ELSE IF @debug = 1 AND @id != 0
		BEGIN
			SET @debug_message = 'Simulation already exists ([id] = ' + CONVERT(NVARCHAR(16),@id) + ') - the existing record will be updated';
			EXEC [debug].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;
	END

	IF @id = 0
	BEGIN
		--Insert new record

		BEGIN TRY
			INSERT INTO [simsig].[TSIM]
			(
				[name],
				[description],
				[simsig_wiki_link],
				[simsig_code],
				[createdon],
				[createdby_id],
				[modifiedon],
				[modifiedby_id]
			)
			VALUES
			(
				@name,
				@description,
				@simsig_wiki_link,
				LOWER(@simsig_code),
				@datetime,
				@app_user_id,
				@datetime,
				@app_user_id
			);

			SET @id = CAST(SCOPE_IDENTITY() AS INT);
		END TRY
		BEGIN CATCH
			IF @debug = 1
			BEGIN
				SET @debug_message = 'An error has occurred trying to insert a record into [app].[TSIM] for ' + @name + ':- ' + ERROR_MESSAGE();
				EXEC [debug].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
			END;

			SET @error_message = 'An error has occurred creating a simulation record for ' + @name + ':- ' + ERROR_MESSAGE();
			THROW 50000, @error_message, 1;
		END CATCH

		IF @debug = 1
		BEGIN
			SET @debug_message = 'New record created successfully ([id] = ' + CAST(@id AS NVARCHAR(16)) + ')';
			EXEC [debug].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;

			SET @debug_message = 'Creating sim era template for simulation [id] = ' + CAST(@id AS NVARCHAR(16)) + ')';
			EXEC [debug].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
		END;

		BEGIN TRY
			INSERT INTO [simsig].[TSIMERA]
			(
				[sim_id],
				[name],
				[description],
				[era_type_id]
			)
			VALUES
			(
				@id,
				@name + ' Template',
				N'Default era template.',
				2 
			);

			IF @debug = 1
			BEGIN
				SET @debug_message = 'Default era template for simulation [id] = ' + CAST(@id AS NVARCHAR(16)) + ') create ([id] = ' + CAST(CAST(SCOPE_IDENTITY() AS INT) AS NVARCHAR(16)) + ')';
				EXEC [debug].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
			END
		END TRY
		BEGIN CATCH
			IF @debug = 1
			BEGIN
				SET @debug_message = 'An error has occured trying to create default era tempate for simulation [id] = ' + CAST(@id AS NVARCHAR(16)) + ': - ' + ERROR_MESSAGE();
				EXEC [debug].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
			END;

			SET @error_message = 'An error has occurred creating default era for simulation ' + @name + ':- ' + ERROR_MESSAGE();
			THROW 50000, @error_message, 1;
		END CATCH
	END
	ELSE
	BEGIN
		--Update the existing record

		BEGIN TRY
			UPDATE [simsig].[TSIM]
			SET
				[description] = NULLIF(@description,''),
				[simsig_wiki_link] = NULLIF(@simsig_wiki_link,''),
				[modifiedon] = @datetime,
				[modifiedby_id] = @app_user_id
			WHERE
				[id] = @id;

			IF @debug = 1
			BEGIN
				SET @debug_message = 'Record [id] = ' + CAST(@id AS NVARCHAR(16)) + ' updated successfully';
				EXEC [debug].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
			END;
		END TRY
		BEGIN CATCH
			IF @debug = 1
			BEGIN
				SET @debug_message = 'An error has occured trying to update [app].[TSIM] record [id] = ' + CAST(@id AS NVARCHAR(16)) + ': - ' + ERROR_MESSAGE();
				EXEC [debug].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
			END;

			SET @error_message = 'An error has occurred updating simulation [id] = ' + CAST(@id AS NVARCHAR(16)) + ':- ' + ERROR_MESSAGE();
			THROW 50000, @error_message, 1;
		END CATCH
	END

	IF @debug = 1
	BEGIN
		SET @debug_message = 'Executing [simsig].[Usp_UPSERT_TSIM] completed.';
		EXEC [debug].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
	END

	SET NOCOUNT OFF;
END