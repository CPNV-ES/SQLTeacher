-- Date: March 2019
-- Author: Benjamin Delacombaz
-- Goal: Populating SQLTeacher database

-- Select database
USE SQLTeacher

-- Clean scores table
DELETE FROM scores
-- Clean participants table
DELETE FROM participants
-- Clean queries table
DELETE FROM queries
-- Clean exercises table
DELETE FROM exercises
-- Clean people table
DELETE FROM people
-- Clean roles table
DELETE FROM roles
-- Clean classes table
DELETE FROM classes


-- ***********************************************************************************
-- Classes
-- ***********************************************************************************
-- Create classes
INSERT INTO classes(name) VALUES ('SI-T1a'), ('SI-T2a'), ('SI-T1b'), ('SI-T2b')
-- ***********************************************************************************

-- ***********************************************************************************
-- Roles
-- ***********************************************************************************
-- Create roles
INSERT INTO roles(name) VALUES ('teacher'), ('student')
-- ***********************************************************************************

-- ***********************************************************************************
-- People
-- ***********************************************************************************
-- Create people
INSERT INTO people(firstname, lastname, email, acronym, classe_id, role_id, pin_code) VALUES 
('Benjamin','Delacombaz','benjamin.delacombaz@cpnv.ch','BDZ',2,2,ABS(CHECKSUM(RAND())) % 9999),
('Razmo','Lux','razmo.lux@cpnv.ch','RLX',2,2,ABS(CHECKSUM(RAND())) % 9999),
('Bernard','Ramirez','bernard.ramirez@cpnv.ch','BRZ',2,2,ABS(CHECKSUM(RAND())) % 9999),
('Marcel','Liota','marcel.liota@cpnv.ch','MLA',2,2,ABS(CHECKSUM(RAND())) % 9999),
('Daniel','Schaad','daniel.schaad@cpnv.ch','DSD',2,2,ABS(CHECKSUM(RAND())) % 9999),
('Xavier','Carrel','xavier.carrel@cpnv.ch','XCL',2,1,ABS(CHECKSUM(RAND())) % 9999)
-- ***********************************************************************************

-- ***********************************************************************************
-- Exercises
-- ***********************************************************************************
-- Create exercises
INSERT INTO exercises(db_script, title) VALUES ('Good luck', 'Cars')
-- ***********************************************************************************

-- ***********************************************************************************
-- Queries
-- ***********************************************************************************
-- Create queries
INSERT INTO queries([statement], formulation, rank, exercise_id) VALUES 
('SELECT model FROM car','Je veux récupérer tous les modèles de voiture',1,1),
('SELECT brand.name, car.model FROM car INNER JOIN brand ON car.brand_id = brand.id','Je veux récupérer tous les modèle de voiture avec leur marque',2,1)
-- ***********************************************************************************

-- ***********************************************************************************
-- Participants
-- ***********************************************************************************
-- Create participants
INSERT INTO participants(classe_id,exercise_id) VALUES (2,1)
-- ***********************************************************************************

-- ***********************************************************************************
-- Scores
-- ***********************************************************************************
-- Create scores
INSERT INTO scores(success,attempts,people_id,querie_id) VALUES (1,4,1,1),(1,1,2,1),(0,10,3,1),(1,3,4,2)
-- ***********************************************************************************
