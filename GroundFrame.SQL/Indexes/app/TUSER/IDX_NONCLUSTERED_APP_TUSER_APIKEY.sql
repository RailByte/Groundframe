/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Indexes\app\app.TUSER\IDX_NONCLUSTERED_APP_TUSER_APIKEY.sql
** Name:		[IDX_NONCLUSTERED_APP_TUSER_APIKEY]
** Desc:		Ensures the API key is unique when it's populated
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
CREATE UNIQUE NONCLUSTERED INDEX [IDX_NONCLUSTERED_APP_TUSER_APIKEY]
	ON [app].[TUSER]
	([api_key] ASC)
	WHERE ([api_key] IS NOT NULL);
