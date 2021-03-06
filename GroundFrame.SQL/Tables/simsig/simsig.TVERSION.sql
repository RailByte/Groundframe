﻿/******************************
** File:		~\GroundFrame\GroundFrame.SQL\Tables\simsig.TVERSION.sql
** Name:		simsig
** Desc:		Table to store SimSig versions. One row can cover a range of SimSig versions. A new entry should be only added when the SimSig TimeTable schema changes
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
CREATE TABLE [simsig].[TVERSION]
(
	[id] SMALLINT NOT NULL IDENTITY,
	[simsig_version_from] NUMERIC(8,4) NOT NULL,
	[simsig_version_to] NUMERIC(8,4) NULL,
	[name] NVARCHAR(128) NOT NULL,
	[description] NVARCHAR(2048) NULL,
	[version_status_id] TINYINT NOT NULL,
	[testdata_id] UNIQUEIDENTIFIER NULL,
	CONSTRAINT PK_SIMSIG_TVERSION PRIMARY KEY ([id] ASC),
	CONSTRAINT FK_SIMSGI_TVERSION_TVERSIONSTATUS FOREIGN KEY ([version_status_id]) REFERENCES [simsig].[TVERSIONSTATUS]([id])
)
