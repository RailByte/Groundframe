/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

--Look up tables first. These tables would never be changed by an application

SET NOCOUNT ON;

Print 'Running Script.PostDeployment.sql...';

:r ".\Post Deployment\Data Population\Script.Populate app.TUSERTYPE.sql"
Print '.\Post Deployment\Data Population\Script.Populate app.TUSERTYPE.sql completed (' + CAST(SYSDATETIME() AS NVARCHAR(200)) + ')';
:r ".\Post Deployment\Data Population\Script.Populate app.TUSERSTATUS.sql"
Print '.\Post Deployment\Data Population\Script.Populate app.TUSERSTATUS.sql completed (' + CAST(SYSDATETIME() AS NVARCHAR(200)) + ')';
:r ".\Post Deployment\Data Population\Script.Populate app.TAPPTYPE.sql"
Print '.\Post Deployment\Data Population\Script.Populate app.TAPPTYPE.sql completed (' + CAST(SYSDATETIME() AS NVARCHAR(200)) + ')';
:r ".\Post Deployment\Data Population\Script.Populate app.TAPPSTATUS.sql"
Print '.\Post Deployment\Data Population\Script.Populate app.TAPPSTATUS.sql completed (' + CAST(SYSDATETIME() AS NVARCHAR(200)) + ')';
:r ".\Post Deployment\Data Population\Script.Populate simsig.TERATYPE.sql"
Print '.\Post Deployment\Data Population\Script.Populate simsig.TERATYPE.sql completed (' + CAST(SYSDATETIME() AS NVARCHAR(200)) + ')';
--Populate internal users and app
:r ".\Post Deployment\Data Population\Script.Populate app.TROLE.sql"
Print '.\Post Deployment\Data Population\Script.Populate app.TROLE.sql completed (' + CAST(SYSDATETIME() AS NVARCHAR(200)) + ')';

:r ".\Post Deployment\Data Population\Script.Populate app.TUSER.sql"
Print '.\Post Deployment\Data Population\Script.Populate app.TUSER.sql completed (' + CAST(SYSDATETIME() AS NVARCHAR(200)) + ')';
:r ".\Post Deployment\Data Population\Script.Populate app.TAPP.sql"
Print '.\Post Deployment\Data Population\Script.Populate app.TAPP.sql completed (' + CAST(SYSDATETIME() AS NVARCHAR(200)) + ')';

Print 'Running Script.PostDeployment.sql completed.';

SET NOCOUNT OFF;