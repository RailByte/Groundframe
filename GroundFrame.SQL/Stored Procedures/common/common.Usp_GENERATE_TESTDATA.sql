/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Stored Procedures\common\common.Usp_GENERATE_TESTDATA.sql
** Name:		common.Usp_GENERATE_TESTDATA
** Desc:		Generates a set of test data
** Unit Test:	
** Auth:		Tim Caceres
** Date:		2019-12-19
**************************
** Change History
**************************
** Ver	Date		Author		Description 
** ---  --------	-------		------------------------------------
** 1    2019-12-19	TC			Initial Script creation
**
*******************************/
CREATE PROCEDURE [common].[Usp_GENERATE_TESTDATA]
	@records_to_generate TINYINT = 5,
	@testdata_id UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	
	--Issues a new test GUID
	SET @testdata_id = NEWID();
	
	--Variables
	DECLARE @counter TINYINT = 1;
	DECLARE @simulation_id INT;
	DECLARE @location_counter TINYINT = 1;

	WHILE @counter <= @records_to_generate
	BEGIN
		--Generate random code to stop duplicate values
		DECLARE @code NVARCHAR(8) = LEFT(CAST(NEWID() AS NVARCHAR(40)),8);

		INSERT INTO [simsig].[TSIM]
		(
			[name],
			[description],
			[simsig_wiki_link],
			[simsig_code],
			[createdby_id],
			[createdon],
			[modifiedby_id],
			[modifiedon],
			[testdata_id]
		)
		VALUES
		(
			'Generated Name ' + @code,
			'Generated Description ' + @code,
			'Generated Wiki Link ' + @code,
			'Generated ' + @code,
			2,
			SYSDATETIMEOFFSET(),
			2,
			SYSDATETIMEOFFSET(),
			@testdata_id
		)

		SET @simulation_id = CAST(SCOPE_IDENTITY() AS INT);

		--Generate Locations

		SET @location_counter = 1;

		WHILE (@location_counter <= @records_to_generate)
		BEGIN
			INSERT INTO [simsig].[TLOCATION]
			(
				[sim_id],
				[tiploc],
				[name],
				[simsig_code],
				[simsig_entry_point],
				[testdata_id],
				[createdby_id],
				[createdby_app_id],
				[createdon],
				[modifiedby_id],
				[modifiedby_app_id],
				[modifiedon]
			)
			VALUES
			(
				@simulation_id,
				'TIPLOC' + CONVERT(NVARCHAR(8), @simulation_id) + CONVERT(NVARCHAR(3), @location_counter),
				'Loc Name ' + CONVERT(NVARCHAR(8), @simulation_id) + CONVERT(NVARCHAR(3), @location_counter),
				'Loc Code ' + CONVERT(NVARCHAR(8), @simulation_id) + CONVERT(NVARCHAR(3), @location_counter),
				CAST(@location_counter % 1 AS BIT),
				@testdata_id,
				4,
				2,
				SYSDATETIMEOFFSET(),
				4,
				2,
				SYSDATETIMEOFFSET()
			)

			SET @location_counter = @location_counter + 1;
		END

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
			@simulation_id,
			'Generated Era Name ' + @code,
			'Generated Era Description ' + @code,
			1,
			@testdata_id
		);

		INSERT INTO [simsig].[TVERSION]
		(
			[name],
			[description],
			[simsig_version_from],
			[simsig_version_to],
			[version_status_id],
			[testdata_id]
		)
		VALUES
		(
			'Generated Version Name ' + @code,
			'Generated Version Description ' + @code,
			CAST(@counter AS NUMERIC(4,1)),
			CASE WHEN @counter = @records_to_generate THEN NULL ELSE CAST(@counter AS NUMERIC(4,1)) + 0.9 END,
			CASE WHEN @counter = @records_to_generate THEN 2 ELSE  1 END,
			@testdata_id
		);

		SET @counter = @counter + 1;
	END
END
GO
