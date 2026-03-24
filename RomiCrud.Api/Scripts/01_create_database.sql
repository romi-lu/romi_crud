-- Ejecutar una vez con un usuario con permisos (p. ej. root).
-- Las tablas y el historial de migraciones se crean al arrancar la API (DbSeeder -> MigrateAsync),
-- o manualmente: dotnet ef database update --project RomiCrud.Api
-- (MySQL 8+ o MariaDB 10.4+; el proyecto usa Microting.EntityFrameworkCore.MySql + MySqlConnector.)

CREATE DATABASE IF NOT EXISTS crud_romi
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;
