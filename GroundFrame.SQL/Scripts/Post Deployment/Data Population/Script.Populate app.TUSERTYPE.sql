/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Script\Post Deployment\Data Population\Script.Populate app.TUSERTYPE.sql
** Name:		Script.Populate app.TUSERTYPE.sql
** Desc:		Populates and maintains app.TUSERTYPE as part of the post deployment script
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
MERGE [app].[TUSERTYPE] AS T
USING
(
	SELECT 
		[id], 
		[name], 
		[description]
	FROM 
	(
		VALUES
		(1, N'External', N'A normal auth0 user account'),
		(2, N'Interal', N'An internal user account used for builds / testing')
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
