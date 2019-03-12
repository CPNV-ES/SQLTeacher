USE master
GO

IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = 'TestExercice1')) BEGIN USE master
    ALTER DATABASE TestExercice1 SET SINGLE_USER WITH ROLLBACK IMMEDIATE

DROP DATABASE TestExercice1
END
GO

SET NOCOUNT ON
GO

CREATE TABLE #ResultSet (Directory varchar(200))
INSERT INTO #ResultSet EXEC master.sys.xp_subdirs 'c:\'
IF NOT EXISTS (Select * FROM #ResultSet where Directory = 'DATA')
    EXEC master.sys.xp_create_subdir 'C:\DATA\'

DELETE FROM #ResultSet
INSERT INTO #ResultSet EXEC master.sys.xp_subdirs 'c:\DATA'
IF NOT EXISTS (Select * FROM #ResultSet where Directory = 'MSSQL')
    EXEC master.sys.xp_create_subdir 'C:\DATA\MSSQL'

DROP TABLE #ResultSet
GO

CREATE DATABASE TestExercice1 ON
    PRIMARY( NAME = 'TestExercice1_data', FILENAME = 'C:\DATA\MSSQL\TestExercice1.mdf' , SIZE = 20480KB , MAXSIZE = 51200KB , FILEGROWTH = 1024KB )
    LOG ON ( NAME = 'TestExercice1_log', FILENAME = 'C:\DATA\MSSQL\TestExercice1.ldf' , SIZE = 10240KB , MAXSIZE = 20480KB , FILEGROWTH = 1024KB )
GO

USE TestExercice1
GO

CREATE TABLE brand (
    id INT NOT NULL IDENTITY PRIMARY KEY,
    name VARCHAR(20) NOT NULL)

CREATE TABLE car (
    id INT NOT NULL IDENTITY PRIMARY KEY,
    model VARCHAR(45) NOT NULL,
    tested BIT NOT NULL,
    brand_id INT NOT NULL,
    color_id INT NOT NULL)

CREATE TABLE color (
    id INT NOT NULL IDENTITY PRIMARY KEY,
    name VARCHAR(50) NOT NULL)

ALTER TABLE car ADD 
    CONSTRAINT fk_car_brand FOREIGN KEY (brand_id) REFERENCES brand (id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT fk_car_color FOREIGN KEY (color_id) REFERENCES color (id) ON DELETE NO ACTION ON UPDATE NO ACTION

INSERT INTO color(name) VALUES ('red'),('blue'),('white'),('black')
INSERT INTO brand(name) VALUES ('Volkswagen'),('Subaru'),('Opel'),('Mazda')
INSERT INTO car(model, tested, brand_id, color_id) VALUES ('T5 Beach',1,1,3),('Impreza WRX STI',1,2,2),('Mokka',1,3,1),('RX8',1,4,4)
