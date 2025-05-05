-- Insertar datos en la tabla persona
INSERT INTO persona (cc, nombre, apellido, genero, edad) 
VALUES 
(1001, 'Juan', 'Pérez', 'M', 30),
(1002, 'Ana', 'González', 'F', 28),
(1003, 'Carlos', 'Sánchez', 'M', 35),
(1004, 'María', 'Lopez', 'F', 25);
GO

-- Insertar datos en la tabla profesion
INSERT INTO profesion (id, nom, des)
VALUES 
(1, 'Ingeniería de Sistemas', 'Profesión en tecnologías de la información y computación'),
(2, 'Medicina', 'Profesión en la ciencia de la salud'),
(3, 'Derecho', 'Profesión en leyes y justicia'),
(4, 'Arquitectura', 'Profesión relacionada con el diseño y construcción');
GO

-- Insertar datos en la tabla estudios
INSERT INTO estudios (id_prof, cc_per, fecha, univer)
VALUES 
(1, 1001, '2015-06-15', 'Universidad Nacional'),
(2, 1002, '2018-09-01', 'Universidad de Bogotá'),
(3, 1003, '2016-03-12', 'Universidad de Medellín'),
(4, 1004, '2020-01-10', 'Universidad de Cali');
GO

-- Insertar datos en la tabla telefono
INSERT INTO telefono (num, oper, duenio) 
VALUES 
('3001234567', 'Claro', 1001),
('3012345678', 'Movistar', 1002),
('3023456789', 'Tigo', 1003),
('3034567890', 'ETB', 1004);
GO
