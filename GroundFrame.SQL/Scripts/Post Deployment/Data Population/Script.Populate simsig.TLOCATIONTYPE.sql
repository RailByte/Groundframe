/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Script\Post Deployment\Data Population\Script.Populate simsig.TLOCATIONTYPE.sql
** Name:		Script.Populate simsig.TLOCATIONTYPE.sql
** Desc:		Populates and maintains simsig.TLOCATIONTYPE as part of the post deployment script
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
MERGE [simsig].[TLOCATIONTYPE] AS T
USING
(
	SELECT 
		[id], 
		[name], 
		[description]
	FROM 
	(
		VALUES
		(1, N'Station', NULL),
		(2, N'Junction', NULL),
		(3, N'Yard / Siding', NULL),
		(4, N'Depot', NULL),
		(5, N'Timing Point', NULL),
		(6, N'Reversing Point', NULL)
	)
	AS LOCATIONTYPES([id], [name], [description])
) AS S
ON T.[id] = S.[id]
WHEN MATCHED
THEN UPDATE SET [T].[name] = S.[name], T.[description] = S.[description]
WHEN NOT MATCHED BY TARGET
THEN INSERT ([id], [name], [description])
VALUES (S.[id], S.[name], S.[description]);
GO
