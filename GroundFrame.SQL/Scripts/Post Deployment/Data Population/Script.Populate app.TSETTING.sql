/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Script\Post Deployment\Data Population\Script.Populate app.TSETTING.sql
** Name:		Script.Populate app.TSETTING.sql
** Desc:		Populates and maintains app.TSETTING as part of the post deployment script
** Unit Test:	N/a
** Auth:		Tim Caceres
** Date:		2020-01-25
**************************
** Change History
**************************
** Ver	Date		Author		Description 
** ---  --------	-------		------------------------------------
** 1    2020-01-12	TC			Initial Script creation
**
*******************************/
MERGE [app].[TSETTING] AS T
USING
(
	SELECT 
		[id], 
		[key], 
		[description],
		[data_type],
		[default_value]
	FROM 
	(
		VALUES
		(1, N'TIMEHALFCHAR', N'The character to indicate a half minute', 'system.int32', '72'),
		(2, N'CULTURE', N'The culture of the user', 'system.string', 'en-GB'),
		(3, N'PASSTIMECHAR', N'The character to indicate a passing time', 'system.string', ':')
	)
	AS SETTINGS([id], [key], [description], [data_type], [default_value])
) AS S
ON T.[id] = S.[id]
WHEN MATCHED
THEN UPDATE SET [T].[key] = S.[key], T.[description] = S.[description], T.[data_type] = S.[data_type], T.[default_value] = S.[default_value]
WHEN NOT MATCHED BY TARGET
THEN INSERT ([id], [key], [description], [data_type], [default_value])
VALUES (S.[id], S.[key], S.[description], [data_type], [default_value]);
GO
