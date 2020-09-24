DELETE FROM Canton
DELETE FROM Centro_Medico
DELETE FROM Centro_Ubicacion
DELETE FROM Distrito
DELETE FROM Domicilio
DELETE FROM Domicilio_Ubicacion
DELETE FROM Incidente
DELETE FROM Modalidad
DELETE FROM Pais
DELETE FROM Pais_Ubicacion
DELETE FROM Provincia
DELETE FROM Ubicacion
DELETE FROM Unidad_De_Transporte

-- Pais
INSERT INTO Pais (NombrePais)
VALUES
    ('Costa Rica'),
    ('Panamá'),
    ('Nicaragua'),
    ('Guatemala'),
    ('Honduras'),
    ('Colombia'),
    ('Guatemala'),
    ('El Salvador');


-- Modalidad 
INSERT INTO Modalidad (Tipo)
VALUES
    ('Terrestre'),
    ('Marítimo'),
    ('Aéreo');

-- Provincia
Insert Into Provincia (NombreProvincia, NombrePais)
Values
    ('San José', 'Costa Rica'),
    ('Heredia', 'Costa Rica'),
    ('Cartago', 'Costa Rica'),
    ('Limón', 'Costa Rica'),
    ('Guanacaste', 'Costa Rica'),
    ('Puntarenas', 'Costa Rica'),
    ('Alajuela', 'Costa Rica');

-- Canton
INSERT INTO Canton (NombreProvincia, NombreCanton)
VALUES
    ('San José', 'Desamparados'),
    ('San José','Escazú'),
    ('Heredia','Heredia'),
    ('Guannacaste','Liberia'),
    ('San José','Mora'),
    ('San José','San José'),
    ('San José','Santa Ana'),
    ('Alajuela','Alajuela'),
    ('San José', 'Santa Ana');

-- Distritos
INSERT INTO Distrito (IdCanton, NombreDistrito)
VALUES
    (6,'Pavas'),
    (2,'Escazú'),
    (2,'San Rafael'),
    (6,'Merced'),
    (8,'Sabanilla'),
    (6,'San Pedro'),
    (3,'Ulloa'),
    (6,'Uruca'),
    (9, 'Santa Ana');

-- Centro Médicos
INSERT INTO Centro_Medico (UbicadoEn, Latitud, Longitud, Nombre)
VALUES
    (8, 23, 23, 'Centro Nacional de Rehabilitación Humberto Araya Rojas'),
    (8, 12, 34.3,'Hospital México'),
    (2, 69, 42.0, 'Hospital Cima');


-- Unidad Transporte
INSERT INTO Unidad_De_Transporte (Matricula, Estado, Modalidad)
VALUES
    ('BPC087', 'Disponible', 'Terrestre'),
    ('FMM420', 'Disponible', 'Terrestre'),
    ('PHP999', 'Disponible', 'Aéreo');

INSERT INTO Ubicacion (CedulaDeMedico)
VALUES
    (117800880),
    (127488581),
    (126305352),
    (826305352),
    (326308472),
    (426308573),
    (126182752),
    (878348179);

-- Domicilio
INSERT INTO Domicilio (Direccion, DistridoId, Latitud, Longitud)
VALUES
    ('Santa Ana 420 metros este de City Place', 9, 205, 200),
    ('Pavas al lado del aeropuerto', 1, 124, 260);



-- Incidentes
INSERT INTO Incidente (Codigo, MatriculaTrans, Estado, IdEspecialista, CedulaAdmin, CedulaTecnicoCoordinador, CedulaTecnicoRevisor, CodigoCita, IdOrigen, IdDestino, Modalidad)
VALUES
    ('TERR123', 'BPC087', 'Registrado', 123, 111111111, 117222222, 1173333333, 1, 1, 2, 'Terrestre'),
    ('AER123', 'PHP999', 'Registrado', 456, 117111111, 117112222, 1171133333, 1, 2, 1, 'Aéreo');

-- Domicilio Ubicacion
INSERT INTO Domicilio_Ubicacion (IdDomicilio, UbicacionId)
VALUES
    (1,1),
    (2,2);

-- Centro_Ubicacion
INSERT INTO Centro_Ubicacion (IdCentro, UbicacionId)
VALUES 
    (1,3),
    (2,4),
    (3,5);

-- Pais_Ubicacion
INSERT INTO Pais_Ubicacion (NombrePais, UbicacionId)
VALUES 
    ('Costa Rica', 6),
    ('Panamá', 7),
    ('Nicaragua', 8);