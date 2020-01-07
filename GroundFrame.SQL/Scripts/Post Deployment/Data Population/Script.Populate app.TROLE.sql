/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Script\Post Deployment\Data Population\Script.Populate app.TROLE.sql
** Name:		Script.Populate app.TROLE.sql
** Desc:		Populates the roles
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

MERGE [app].[TROLE] AS T
USING
(
	SELECT
		[id],
		[name],
		[description]
	FROM 
	(
		VALUES
		(1, N'Standard',N'The default role which allows a standard user to view, create and edit timetables'),
		(2, N'Editor',N'The editor role which allows a user to create and edit simulation data. Can only be assigned by a user in the Admin role'),
		(4, N'Admin',N'The admin role which allows a user god rights')
	)
	AS ROLES(		
		[id],
		[name],
		[description]
	)
) AS S
ON T.[id] = S.[id]
WHEN NOT MATCHED BY TARGET
THEN INSERT 
(
		[id],
		[name],
		[description]
)
VALUES 
(
		S.[id],
		S.[name],
		S.[description]
);
GO
