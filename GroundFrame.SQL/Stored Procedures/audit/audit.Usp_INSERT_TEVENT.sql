/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Tables\audit\audit.TEVENT.sql
** Name:		audit.TEVENT
** Desc:		Stored proc to insert a debug event in [audit].[USp_INSERT_TEVENT]
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
CREATE PROCEDURE [audit].[Usp_INSERT_TEVENT]
	@debug_session_id UNIQUEIDENTIFIER,
	@source_object_id INT,
	@event NVARCHAR(2048),
	@id INT = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	--Variables--
	DECLARE @logged_in BIT = ISNULL(CONVERT(BIT,SESSION_CONTEXT(N'logged_in')),0); 

	--Check user is logged in
	IF @logged_in = 0
	BEGIN;
		THROW 50000, 'Error executing [audit].[USp_INSERT_TEVENT]: - The user is not logged in.', 1;
	END;

	DECLARE @application INT = CONVERT(INT,SESSION_CONTEXT(N'application'));  
	DECLARE @app_user INT = CONVERT(INT,SESSION_CONTEXT(N'app_user'));  

	--Check that the source object exists
	IF @source_object_id != 0 AND OBJECT_NAME(@source_object_id) IS NULL
	BEGIN;
		DECLARE @error_message NVARCHAR(256) = 'Error executing [audit].[USp_INSERT_TEVENT]: - @object_id ' + CONVERT(NVARCHAR(16),@source_object_id) + ' is not a valid object.';
		THROW 50000, @error_message, 1;
	END;

	INSERT INTO [audit].[TEVENT]
	(
		[debug_session_id],
		[source_object_id],
		[event],
		[app_id],
		[app_user_id]
	)
	VALUES
	(
		@debug_session_id,
		@source_object_id,
		@event,
		@application,
		@app_user
	);

	SET @id = CAST(SCOPE_IDENTITY() AS INT);

	SET NOCOUNT OFF;
END