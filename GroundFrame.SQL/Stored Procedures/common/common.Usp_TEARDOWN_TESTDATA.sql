/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Stored Procedures\common\common.Usp_TEARDOWN_TESTDATA.sql
** Name:		common.Usp_TEARDOWN_TESTDATA
** Desc:		Tears down all data in the database. Should only be used as part of testing.
** Unit Test:	
** Auth:		Tim Caceres
** Date:		2019-12-12
**************************
** Change History
**************************
** Ver	Date		Author		Description 
** ---  --------	-------		------------------------------------
** 1    2019-12-19	TC			Initial Script creation
**
*******************************/
CREATE PROCEDURE [common].[Usp_TEARDOWN_TESTDATA]
	@testdata_id UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

	--Debug events are always logged for this proc theref
	
	DECLARE @debug_session_id UNIQUEIDENTIFIER = NEWID();
	DECLARE @debug_message NVARCHAR(256);
   
	--Check user is logged in
	DECLARE @logged_in BIT = ISNULL(CONVERT(BIT,SESSION_CONTEXT(N'logged_in')),0); 
	
	IF @logged_in = 0
	BEGIN;
		THROW 50000, 'The user is not logged in.', 1;
	END;
	
	SET @debug_message = 'Executing [common].[Usp_TEARDOWN_TESTDATA] started.';
	EXEC [debug].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;

	--Check tear down flag

	DECLARE @can_tear_down BIT = ISNULL(CONVERT(BIT,SESSION_CONTEXT(N'can_tear_down')),0);  --This is set by the GFSqlConnector library;

	IF @can_tear_down = 0
	BEGIN
		SET @debug_message = 'The tear down flag has not been set.';
		EXEC [debug].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
	
		THROW 50000, 'The Tear Down flag is not set therefore this proc cannot run', 1;
	END;

	BEGIN TRY
		--Gets a list of the Simulations in the Test Data set
		
		DECLARE @SIMS TABLE (id SMALLINT IDENTITY(1,1), sim_id INT);

		INSERT INTO @SIMS
		(
			[sim_id]
		)
		SELECT
			[id]
		FROM [simsig].[TSIM]
		WHERE
			[testdata_id] = @testdata_id;

		DECLARE @counter INT = 1;
		DECLARE @simulation_id INT;

		--Loop around each sim and delete the data

		WHILE @counter <= (SELECT COUNT(*) FROM @SIMS)
		BEGIN
			SELECT
				@simulation_id = [sim_id]
			FROM @SIMS
			WHERE
				[id] = @counter;

			DELETE FROM [simsig].[TSIMERA] WHERE sim_id = @simulation_id;
			DELETE FROM [simsig].[TSIM] WHERE id = @simulation_id;

			SET @counter = @counter +1;
		END
	
		SET @debug_message = 'Executing [common].[Usp_TEARDOWN_TESTDATA] completed.';
		EXEC [debug].[Usp_INSERT_TEVENT] @debug_session_id, @@PROCID, @debug_message;
	END TRY
	BEGIN CATCH
		DECLARE @error_message NVARCHAR(256) = 'The proc has failed trying to delete the test down data:- ' + ERROR_MESSAGE();
		THROW 50000, @error_message, 1;
	END CATCH
	SET NOCOUNT OFF;
END