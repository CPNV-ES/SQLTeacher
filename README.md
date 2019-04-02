# SQLTeacher

SQLTeacher is an asp.net core app for testing student in SQL

## Requirements

Windows Sql Server
Windows Sql Server Management Studio
Visual Studio 2017
Net Core 2.1

## Open project

`git clone https://github.com/CPNV-ES/SQLTeacher-ASP.git` and open SQLTeacher.sln with Visual Studio 2017

## Database

With Sql Server Management Studio, execute `/doc/database.sql`

Seed database with `/doc/seeder.sql`

And execute the `/doc/exercice_1.sql` file to have a database for testing.

## Authentication

If you want to change the authenticated user, you must change the pinCode in the `Config.cs` file.

You can find the pinCode in the `people` table in the database.

With the seeder, only `Xavier Carrel` is a teacher.

If the pinCode in the config doesn't exists. You are considered like a student but not you aren't going to be authenticated
