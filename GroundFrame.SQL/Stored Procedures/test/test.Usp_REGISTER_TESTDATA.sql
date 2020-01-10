/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Stored Procedures\test\test.Usp_REGISTER_TESTDATA.sql
** Name:		test.Usp_REGISTER_TESTDATA
** Desc:		Stored procedure to register a test dataset.
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
CREATE PROCEDURE [test].[Usp_REGISTER_TESTDATA]
	@key NVARCHAR(256),
	@testdata_id UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		@testdata_id = [id]
	FROM [test].[TTESTDATA]
	WHERE
		[key] = @key;

	IF @testdata_id IS NULL
	BEGIN
		SET @testdata_id = NEWID();

		INSERT INTO [test].[TTESTDATA]
		(
			[id],
			[key]
		)
		VALUES
		(
			@testdata_id,
			@key
		);
	END

	SET NOCOUNT OFF;
END