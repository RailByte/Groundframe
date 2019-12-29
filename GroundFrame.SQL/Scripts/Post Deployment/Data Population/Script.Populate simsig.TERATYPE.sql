/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Script\Post Deployment\Data Population\Script.Populate simsig.TERATYPE.sql
** Name:		Script.Populate simsig.TERATYPE.sql
** Desc:		Populates and maintains simsig.TERATYPE as part of the post deployment script
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
MERGE [simsig].[TERATYPE] AS T
USING
(
	SELECT 
		[id], 
		[name], 
		[description]
	FROM 
	(
		VALUES
		(1, N'WTT', N'Is an era that can have a WTT created against it'),
		(2, N'Template', N'An internal template which can be cloned to create a WTT era')
	)
	AS ERATYPES([id], [name], [description])
) AS S
ON T.[id] = S.[id]
WHEN MATCHED
THEN UPDATE SET [T].[name] = S.[name], T.[description] = S.[description]
WHEN NOT MATCHED BY TARGET
THEN INSERT ([id], [name], [description])
VALUES (S.[id], S.[name], S.[description]);
GO
