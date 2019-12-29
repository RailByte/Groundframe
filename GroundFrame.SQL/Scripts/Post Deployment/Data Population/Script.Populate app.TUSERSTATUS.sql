/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Script\Post Deployment\Data Population\Script.Populate app.TUSERSTATUS.sql
** Name:		Script.Populate app.TAPPSTATUS.sql
** Desc:		Populates and maintains app.TAPPSTATUS as part of the post deployment script
** Unit Test:	N/a
** Auth:		Tim Caceres
** Date:		2019-12-13
**************************
** Change History
**************************
** Ver	Date		Author		Description 
** ---  --------	-------		------------------------------------
** 1    2019-12-12	TC			Initial Script creation
**
*******************************/
MERGE [app].[TAPPSTATUS] AS T
USING
(
	SELECT 
		[id], 
		[name], 
		[description]
	FROM 
	(
		VALUES
		(1, N'Active', N'Apps with this status can execute procs'),
		(2, N'Disabled', N'Apps with this status cannot execute procs')
	)
	AS USERSTATUS([id], [name], [description])
) AS S
ON T.[id] = S.[id]
WHEN MATCHED
THEN UPDATE SET [T].[name] = S.[name], T.[description] = S.[description]
WHEN NOT MATCHED BY TARGET
THEN INSERT ([id], [name], [description])
VALUES (S.[id], S.[name], S.[description]);
GO
