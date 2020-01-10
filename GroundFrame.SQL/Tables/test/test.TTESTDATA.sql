/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Tables\test\test.TTESTDATA.sql
** Name:		test
** Desc:		Table to store testdata ID records to help ensure clean teardown of testing data in the database
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
CREATE TABLE [test].[TTESTDATA]
(
	[id] UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_TEST_ID DEFAULT (NEWID()),
	[key] NVARCHAR(128) NOT NULL,
	[description] NVARCHAR(2048) NULL,
	CONSTRAINT PK_TEST_TESTDATA PRIMARY KEY ([id] ASC),
	CONSTRAINT UK_TEST_TESTDATA_KEY UNIQUE ([key])
)
