
PRINT ''
PRINT 'Data Tables.sql Starting';

USE CVGS;

GO

--********************
-- Tables about places
--********************

--=========
-- Location
--=========

PRINT '>>> Creating Event Table';

CREATE TABLE Event
(
 EventID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
 Name VarChar(200),
 Description Varchar(2000),
 StartDate Date,
 EndDate Date,
 Country Varchar(50),
 StateProvice Varchar(100),
 City Varchar(100),
 Address Varchar(100)
 );

GO