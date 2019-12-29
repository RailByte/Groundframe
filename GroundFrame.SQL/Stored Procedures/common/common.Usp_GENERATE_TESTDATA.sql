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

		INSERT INTO [simsig].[TSIMERA]
		(
			[sim_id],
			[name],
			[description],
			[era_type_id]
		)
		VALUES
		(
			@simulation_id,
			'Generated Era Name ' + @code,
			'Generated Era Description ' + @code,
			1
		);

		SET @counter = @counter + 1;
	END
END
GO
