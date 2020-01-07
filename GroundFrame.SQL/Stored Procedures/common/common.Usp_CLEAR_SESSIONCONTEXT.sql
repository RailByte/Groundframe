/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Stored Procedures\common\common.Usp_CLEAR_SESSIONCONTEXT.sql
** Name:		common.Usp_CLEAR_SESSIONCONTEXT
** Desc:		Stored procedure to clear the session context information about the user logged in
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
CREATE PROCEDURE [common].[Usp_CLEAR_SESSIONCONTEXT]
AS
BEGIN
	SET NOCOUNT ON;

	EXEC sys.sp_set_session_context @key = N'application', @value = NULL, @read_only = 0; 
	EXEC sys.sp_set_session_context @key = N'app_user', @value = NULL, @read_only = 0; 
	EXEC sys.sp_set_session_context @key = N'logged_in', @value = 0, @read_only = 0; 
	EXEC sys.sp_set_session_context @key = N'testdata_id', @value = NULL, @read_only = 0; 

	SET NOCOUNT OFF;
END