/******************
** File:		~\GroundFrame\GroundFrame.SQL\Functions\Scalar\app\app.Fn_GET_USERROLE.sql
** Name:		app.Fn_GET_USERROLE
** Desc:		Function to the get the logged in user role bit map
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
CREATE FUNCTION [app].[Fn_GET_USERROLE]
(
)
RETURNS BIGINT
AS
BEGIN
	DECLARE @app_user_id INT = ISNULL(CONVERT(INT,SESSION_CONTEXT(N'app_user')),0); 

	RETURN ISNULL((SELECT [role_bitmap] FROM [app].[TUSER] WHERE [id] = @app_user_id),0);
END

