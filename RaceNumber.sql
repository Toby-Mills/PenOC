/*
   28 May 201408:54:36 PM
   User: sa
   Server: sdrsa50\sql2008r2
   Database: PenOC
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.tblResult ADD
	strRaceNumber nvarchar(20) NULL
GO
ALTER TABLE dbo.tblResult SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
