-- Date: Feb 2019
-- Author: X. Carrel
-- Goal: Creates the DB as ASP project material

USE master
GO

-- First delete the database if it exists
IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = 'SQLTeacher'))
BEGIN
	USE master
	ALTER DATABASE SQLTeacher SET SINGLE_USER WITH ROLLBACK IMMEDIATE; -- Disconnect users the hard way (we cannot drop the db if someone's connected)
	DROP DATABASE SQLTeacher -- Destroy it
END
GO

-- Second ensure we have the proper directory structure
SET NOCOUNT ON;
GO
CREATE TABLE #ResultSet (Directory varchar(200)) -- Temporary table (name starts with #) -> will be automatically destroyed at the end of the session

INSERT INTO #ResultSet EXEC master.sys.xp_subdirs 'c:\' -- Stored procedure that lists subdirectories

IF NOT EXISTS (Select * FROM #ResultSet where Directory = 'DATA')
	EXEC master.sys.xp_create_subdir 'C:\DATA\' -- create DATA

DELETE FROM #ResultSet -- start over for MSSQL subdir
INSERT INTO #ResultSet EXEC master.sys.xp_subdirs 'c:\DATA'

IF NOT EXISTS (Select * FROM #ResultSet where Directory = 'MSSQL')
	EXEC master.sys.xp_create_subdir 'C:\DATA\MSSQL'

DROP TABLE #ResultSet -- Explicitely delete it because the script may be executed multiple times during the same session
GO

-- Everything is ready, we can create the db
CREATE DATABASE SQLTeacher ON  PRIMARY 
( NAME = 'SQLTeacher_data', FILENAME = 'C:\DATA\MSSQL\SQLTeacher.mdf' , SIZE = 20480KB , MAXSIZE = 51200KB , FILEGROWTH = 1024KB )
 LOG ON 
( NAME = 'SQLTeacher_log', FILENAME = 'C:\DATA\MSSQL\SQLTeacher.ldf' , SIZE = 10240KB , MAXSIZE = 20480KB , FILEGROWTH = 1024KB )

GO

-- Create tables 

USE SqlTeacher
GO

-- -----------------------------------------------------
-- Table classes
-- -----------------------------------------------------
CREATE TABLE classes (
  id INT NOT NULL IDENTITY PRIMARY KEY,
  name VARCHAR(20) NOT NULL,
  teacher_id INT NULL);

-- -----------------------------------------------------
-- Table roles
-- -----------------------------------------------------
CREATE TABLE roles (
  id INT NOT NULL IDENTITY PRIMARY KEY,
  name VARCHAR(45) NOT NULL)

-- -----------------------------------------------------
-- Table people
-- -----------------------------------------------------
CREATE TABLE people (
  id INT NOT NULL IDENTITY PRIMARY KEY,
  firstname VARCHAR(50) NOT NULL,
  lastname VARCHAR(50) NOT NULL,
  email VARCHAR(100) NOT NULL,
  acronym VARCHAR(3) NULL,
  classe_id INT NOT NULL,
  role_id INT NOT NULL,
  pin_code INT NOT NULL UNIQUE);

-- -----------------------------------------------------
-- Table exercises
-- -----------------------------------------------------
CREATE TABLE exercises (
  id INT NOT NULL IDENTITY PRIMARY KEY,
  db_script VARCHAR(5000) NOT NULL,
  title VARCHAR(5000) NOT NULL,
  is_active BIT NOT NULL DEFAULT 0);


-- -----------------------------------------------------
-- Table queries
-- -----------------------------------------------------
CREATE TABLE queries (
  id INT NOT NULL IDENTITY PRIMARY KEY,
  statement VARCHAR(5000) NOT NULL,
  formulation VARCHAR(5000) NOT NULL,
  rank INT NOT NULL,
  exercise_id INT NOT NULL);
  
-- -----------------------------------------------------
-- Table participants
-- -----------------------------------------------------
CREATE TABLE participants (
  id INT NOT NULL IDENTITY PRIMARY KEY,
  classe_id INT NOT NULL,
  exercise_id INT NOT NULL);
  
-- -----------------------------------------------------
-- Table scores
-- -----------------------------------------------------
CREATE TABLE scores (
  id INT NOT NULL IDENTITY PRIMARY KEY,
  success INT NOT NULL DEFAULT 0,
  attempts INT NOT NULL,
  people_id INT NOT NULL,
  querie_id INT NOT NULL);

ALTER TABLE people ADD 
    CONSTRAINT fk_students_classes FOREIGN KEY (classe_id) REFERENCES classes (id) ON DELETE NO ACTION ON UPDATE NO ACTION, 
	CONSTRAINT fk_persons_roles FOREIGN KEY (role_id) REFERENCES roles (id) ON DELETE NO ACTION ON UPDATE NO ACTION; 
ALTER TABLE queries ADD 
	CONSTRAINT fk_queries_exercices FOREIGN KEY (exercise_id) REFERENCES exercises (id) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE participants ADD 
	CONSTRAINT fk_participants_exercices FOREIGN KEY (exercise_id) REFERENCES exercises (id) ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT fk_participants_classes FOREIGN KEY (classe_id) REFERENCES classes (id) ON DELETE NO ACTION ON UPDATE NO ACTION; 
ALTER TABLE classes ADD 
	CONSTRAINT fk_classes_teachers FOREIGN KEY (teacher_id) REFERENCES people (id) ON DELETE NO ACTION ON UPDATE NO ACTION; 
ALTER TABLE scores ADD 
	CONSTRAINT fk_scores_persons FOREIGN KEY (people_id) REFERENCES people (id) ON DELETE NO ACTION ON UPDATE NO ACTION, 
	CONSTRAINT fk_scores_queries FOREIGN KEY (querie_id) REFERENCES queries (id) ON DELETE NO ACTION ON UPDATE NO ACTION;
