/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Indexes\app\app.TUSER\IDX_NONCLUSTERED_APP_TUSER_EMAIL.sql
** Name:		[IDX_NONCLUSTERED_APP_TUSER_EMAIL]
** Desc:		Ensures the email is unique when it's populated
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
CREATE UNIQUE NONCLUSTERED INDEX [IDX_NONCLUSTERED_APP_TUSER_EMAIL]
	ON [app].[TUSER]
	([email] ASC)
	WHERE ([email] IS NOT NULL);
