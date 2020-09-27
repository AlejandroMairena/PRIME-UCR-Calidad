DELETE FROM EstadoIncidente
DELETE FROM Estado
DELETE FROM Incidente
DELETE FROM Unidad_De_Transporte
DELETE FROM Modalidad
DELETE FROM Centro_Ubicacion
DELETE FROM Centro_Medico
DELETE FROM Pais_Ubicacion
DELETE FROM Domicilio_Ubicacion
DELETE FROM Domicilio
DELETE FROM Ubicacion
DELETE FROM Distrito
DELETE FROM Canton
DELETE FROM Provincia
DELETE FROM Pais

DBCC CHECKIDENT ('Canton', RESEED, 0)
DBCC CHECKIDENT ('Centro_Medico', RESEED, 0)
DBCC CHECKIDENT ('Centro_Ubicacion', RESEED, 0)
DBCC CHECKIDENT ('Distrito', RESEED, 0)
DBCC CHECKIDENT ('Domicilio', RESEED, 0)
DBCC CHECKIDENT ('Domicilio_Ubicacion', RESEED, 0)
DBCC CHECKIDENT ('Pais_Ubicacion', RESEED, 0)
DBCC CHECKIDENT ('Ubicacion', RESEED, 0)

-- Pais
INSERT INTO Pais (Nombre)
VALUES
    ('Costa Rica'),
    ('Panamá'),
    ('Nicaragua'),
    ('Guatemala'),
    ('Honduras'),
    ('El Salvador');

-- Provincia
Insert Into Provincia (Nombre, NombrePais)
Values
    ('San José', 'Costa Rica'),
    ('Heredia', 'Costa Rica'),
    ('Cartago', 'Costa Rica'),
    ('Limón', 'Costa Rica'),
    ('Guanacaste', 'Costa Rica'),
    ('Puntarenas', 'Costa Rica'),
    ('Alajuela', 'Costa Rica');


-- Canton
INSERT INTO Canton (NombreProvincia, Nombre)
VALUES
    ('San José', 'Desamparados'),
    ('San José','Escazú'),
    ('Heredia','Heredia'),
    ('Guanacaste','Liberia'),
    ('San José','Mora'),
    ('San José','San José'),
    ('San José','Santa Ana'),
    ('Alajuela','Alajuela')

-- Distritos
INSERT INTO Distrito (IdCanton, Nombre)
VALUES
    (6, 'Pavas'),
    (2, 'Escazú'),
    (2, 'San Rafael'),
    (6, 'Merced'),
    (8, 'Sabanilla'),
    (6, 'San Pedro'),
    (3, 'Ulloa'),
    (6, 'Uruca'),
    (7, 'Santa Ana');

-- Ubicación
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

-- Domicilio Ubicacion
INSERT INTO Domicilio_Ubicacion (IdDomicilio, UbicacionId)
VALUES
    (1,1),
    (2,2);

-- Pais_Ubicacion
INSERT INTO Pais_Ubicacion (NombrePais, UbicacionId)
VALUES 
    ('Costa Rica', 6),
    ('Panamá', 7),
    ('Nicaragua', 8);

-- Centro Médicos
INSERT INTO Centro_Medico (UbicadoEn, Latitud, Longitud, Nombre)
VALUES
    (8, 23, 23, 'Centro Nacional de Rehabilitación Humberto Araya Rojas'),
    (8, 12, 34.3,'Hospital México'),
    (2, 69, 42.0, 'Hospital Cima');

-- Centro_Ubicacion
INSERT INTO Centro_Ubicacion (IdCentro, UbicacionId)
VALUES 
    (1,3),
    (2,4),
    (3,5);

-- Modalidad 
INSERT INTO Modalidad (Tipo)
VALUES
    ('Terrestre'),
    ('Marítimo'),
    ('Aéreo');

-- Unidad Transporte
INSERT INTO Unidad_De_Transporte (Matricula, Estado, Modalidad)
VALUES
    ('BPC087', 'Disponible', 'Terrestre'),
    ('FMM420', 'Disponible', 'Terrestre'),
    ('PHP999', 'Disponible', 'Aéreo');

-- Incidente
INSERT INTO Incidente (Codigo, MatriculaTrans, Estado, IdEspecialista, CedulaAdmin,
    CedulaTecnicoCoordinador, CedulaTecnicoRevisor, CodigoCita, IdOrigen, IdDestino,
    Modalidad, FechaHoraRegistro, FechaHoraEstimada)
VALUES
    ('TERR123', 'BPC087', 'Registrado', 123, 111111111, 117222222, 1173333333, 1, 1, 2, 'Terrestre', GETDATE(), GETDATE()),
    ('AER123', 'PHP999', 'Registrado', 456, 117111111, 117112222, 1171133333, 1, 2, 1, 'Aéreo', GETDATE(), GETDATE());

-- Estado
INSERT INTO Estado
VALUES
    ('En proceso de creación'),
    ('Creado'),
    ('Rechazado'),
    ('Aceptado'),
    ('Asignado'),
    ('En preparación'),
    ('En ruta a origen'),
    ('Paciente recolectado en origen'),
    ('En traslado'),
    ('Entregado'),
    ('Reactivación'),
    ('Finalizado')

-- EstadoIncidente
INSERT INTO EstadoIncidente
VALUES
    ('TERR123', 'En proceso de creación', GETDATE(), 1),
    ('AER123', 'En proceso de creación', GETDATE(), 1)

