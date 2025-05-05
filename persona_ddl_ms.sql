-- Crear la base de datos
CREATE DATABASE persona_db;
GO

-- Usar la base de datos
USE persona_db;
GO

-- -----------------------------------------------------
-- Tabla persona
-- -----------------------------------------------------
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

-- -----------------------------------------------------
-- Tabla profesion
-- -----------------------------------------------------
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

-- -----------------------------------------------------
-- Tabla estudios
-- -----------------------------------------------------
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

-- -----------------------------------------------------
-- Tabla telefono
-- -----------------------------------------------------
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
