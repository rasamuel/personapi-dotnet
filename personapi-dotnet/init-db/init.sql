-- Crear la base de datos solo si no existe
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'persona_db')
BEGIN
    CREATE DATABASE persona_db;
END
GO

-- Usar la base de datos
USE persona_db;
GO

-- Tabla persona
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'persona')
BEGIN
    CREATE TABLE persona (
        cc INT NOT NULL,
        nombre VARCHAR(45) NOT NULL,
        apellido VARCHAR(45) NOT NULL,
        genero CHAR(1) NOT NULL CHECK (genero IN ('M', 'F')),
        edad INT NULL,
        PRIMARY KEY (cc)
    );
END
GO

-- Tabla profesion
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'profesion')
BEGIN
    CREATE TABLE profesion (
        id INT NOT NULL,
        nom VARCHAR(90) NOT NULL,
        des TEXT NULL,
        PRIMARY KEY (id)
    );
END
GO

-- Tabla estudios
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'estudios')
BEGIN
    CREATE TABLE estudios (
        id_prof INT NOT NULL,
        cc_per INT NOT NULL,
        fecha DATE NULL,
        univer VARCHAR(50) NULL,
        PRIMARY KEY (id_prof, cc_per),
        CONSTRAINT estudio_persona_fk FOREIGN KEY (cc_per) REFERENCES persona (cc),
        CONSTRAINT estudio_profesion_fk FOREIGN KEY (id_prof) REFERENCES profesion (id)
    );
END
GO

-- Tabla telefono
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'telefono')
BEGIN
    CREATE TABLE telefono (
        num VARCHAR(15) NOT NULL,
        oper VARCHAR(45) NOT NULL,
        duenio INT NOT NULL,
        PRIMARY KEY (num),
        CONSTRAINT telefono_persona_fk FOREIGN KEY (duenio) REFERENCES persona (cc)
    );
END
GO

-- Insertar datos en persona
INSERT INTO persona (cc, nombre, apellido, genero, edad) 
VALUES 
(1001, 'Juan', 'Pérez', 'M', 30),
(1002, 'Ana', 'González', 'F', 28),
(1003, 'Carlos', 'Sánchez', 'M', 35),
(1004, 'María', 'Lopez', 'F', 25);
GO

-- Insertar datos en profesion
INSERT INTO profesion (id, nom, des)
VALUES 
(1, 'Ingeniería de Sistemas', 'Profesión en tecnologías de la información y computación'),
(2, 'Medicina', 'Profesión en la ciencia de la salud'),
(3, 'Derecho', 'Profesión en leyes y justicia'),
(4, 'Arquitectura', 'Profesión relacionada con el diseño y construcción');
GO

-- Insertar datos en estudios
INSERT INTO estudios (id_prof, cc_per, fecha, univer)
VALUES 
(1, 1001, '2015-06-15', 'Universidad Nacional'),
(2, 1002, '2018-09-01', 'Universidad de Bogotá'),
(3, 1003, '2016-03-12', 'Universidad de Medellín'),
(4, 1004, '2020-01-10', 'Universidad de Cali');
GO

-- Insertar datos en telefono
INSERT INTO telefono (num, oper, duenio) 
VALUES 
('3001234567', 'Claro', 1001),
('3012345678', 'Movistar', 1002),
('3023456789', 'Tigo', 1003),
('3034567890', 'ETB', 1004);
GO
