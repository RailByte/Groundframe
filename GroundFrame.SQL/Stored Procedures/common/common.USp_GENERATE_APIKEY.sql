/******************
** File:		~\GroundFrame\GroundFrame.SQL\Tables\common\common.USp_GENERATE_APIKEY.sql
** Name:		common.USp_GENERATE_APIKEY
** Desc:		Stored proc to generate a unique API Key
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
CREATE PROCEDURE [common].[Usp_GENERATE_APIKEY]
	@length INT = NULL,
	@api_key VARCHAR(16) = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @min_length TINYINT = 10;
	DECLARE @max_length TINYINT = 16;

	--Clear @api_key
	SET @api_key = NULL

	--If no length provided created random length between @min_length and @max_length

	IF @length IS NULL
	BEGIN
		SET @length = ABS(CHECKSUM(NEWID()) % (@max_length - @min_length + 1)) + @min_length
	END

	--Set ranges

	IF @max_length > 16 
	BEGIN
		SET @max_length = 16;
	END

	IF @min_length <10
	BEGIN
		SET @min_length = 10;
	END

	--Loop around until the generated key hasn't already been issued and insert into [common].[TAPIKEY] 

	WHILE NOT EXISTS (SELECT 1 FROM [common].[TAPIKEY] WHERE [api_key] = @api_key AND @api_key IS NOT NULL)
	BEGIN
		SET @api_key = LEFT(REPLACE(CONVERT(VARCHAR(46), NEWID()),'-',''),@length);

		INSERT INTO [common].[TAPIKEY]
		(
			[api_key]
		)
		VALUES
		(
			@api_key
		)
	END
	
	SET NOCOUNT OFF
END