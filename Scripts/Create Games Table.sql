USE CVGS;

GO

drop table [Game]

CREATE TABLE [Game]
(
 id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
 [title] varChar(50),
 [platform] varchar(25),
 [Description] varchar(200),
  developer varchar(50),
   publisher varchar(50),
   genre varchar(25),
   EsrbRating varchar(5),
   price decimal(19,4),
   publishDate date
 );

GO