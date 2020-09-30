DELETE FROM EstadoIncidente
DELETE FROM Estado
DELETE FROM Incidente
DELETE FROM Unidad_De_Transporte
DELETE FROM Modalidad
DELETE FROM Centro_Ubicacion
DELETE FROM Centro_Medico
DELETE FROM Internacional
DELETE FROM Domicilio
DELETE FROM Ubicacion
DELETE FROM Distrito
DELETE FROM Canton
DELETE FROM Provincia
DELETE FROM Pais

DBCC CHECKIDENT ('Canton', RESEED, 0)
DBCC CHECKIDENT ('Centro_Medico', RESEED, 0)
DBCC CHECKIDENT ('Distrito', RESEED, 0)
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
    ('San José','San José'),
    ('San José','Escazú'),
    ('San José','Desamparados'), 
    ('San José','Puriscal'),
    ('San José','Terrazú'),
    ('San José','Aserrí'),
    ('San José','Mora'),
    ('San José','Goicoechea'),
    ('San José','Santa Ana'),
    ('San José','Alajuelita'),
    ('San José','Vázquez de Coronado'),
    ('San José','Acosta'),
    ('San José','Tibás'),
    ('San José','Moravia'),
    ('San José','Montes de Oca'),
    ('San José','Turrubares'),
    ('San José','Dota'), 
    ('San José','Curridabat'),
    ('San José','Pérez Zeledón'),
    ('San José','León Cortés Castro'),

    ('Alajuela','Alajuela'),
    ('Alajuela','San Ramón'),
    ('Alajuela','Grecia'),
    ('Alajuela','San Mateo'),
    ('Alajuela','Atenas'),
    ('Alajuela','Naranjo'),
    ('Alajuela','Palmares'),
    ('Alajuela','Poas'),
    ('Alajuela','Orotina'),
    ('Alajuela','San Carlos'),
    ('Alajuela','Zarcero'),
    ('Alajuela','Sarchí'),
    ('Alajuela','Upala'), 
    ('Alajuela','Los Chiles'),
    ('Alajuela','Guatuso'),
    ('Alajuela','Río Cuarto'),

    ('Cartago','Cartago'),
    ('Cartago','Paraiso'),
    ('Cartago','La Unión'),
    ('Cartago','Jiménez'),
    ('Cartago','Turrialba'),
    ('Cartago','Alvarado'),
    ('Cartago','Oreamuno'), 
    ('Cartago','El Guarco'),

    ('Heredia','Heredia'), 
    ('Heredia','Barva'),
    ('Heredia','Santo Domingo'),
    ('Heredia','Santa Bárbara'),
    ('Heredia','San Rafael'),
    ('Heredia','San Isidro'),
    ('Heredia','Belén'),
    ('Heredia','Flores'),
    ('Heredia','San Pablo'),
    ('Heredia','Sarapiquí'), 

    ('Guanacaste','Liberia'),
    ('Guanacaste','Nicoya'),
    ('Guanacaste','Santa Cruz'),
    ('Guanacaste','Bagaces'),
    ('Guanacaste','Carrillo'),
    ('Guanacaste','Cañas'),
    ('Guanacaste','Abangares'),
    ('Guanacaste','Tilarán'),
    ('Guanacaste','Nandayure'),
    ('Guanacaste','La Cruz'),
    ('Guanacaste','Hojancha'), 

    ('Limón','Limón'),
    ('Limón','Pococí'),
    ('Limón','Siquirres'),
    ('Limón','Talamanca'),
    ('Limón','Matina'),
    ('Limón','Guácimo'),
    
    ('Puntarenas','Puntarenas'),
    ('Puntarenas','Esparza'), 
    ('Puntarenas','Buenos Aires'),
    ('Puntarenas','Montes de Oro'),
    ('Puntarenas','Osa'),
    ('Puntarenas','Quepos'),
    ('Puntarenas','Golfito'),
    ('Puntarenas','Coto Brus'),
    ('Puntarenas','Parrita'),
    ('Puntarenas','Corredores'),
    ('Puntarenas','Garabito'); 

-- Distritos
INSERT INTO Distrito (IdCanton, Nombre)
VALUES
    (1, 'San José'),
    (1, 'Pavas'),
    (2, 'Escazú'),
    (3, 'Desamparados'),
    (4, 'Santiago'),
    (5, 'San Marcos'),
    (6, 'Aserrí'),
    (7, 'Ciudad Colon'),
    (8, 'Guadalupe'),
    (9, 'Santa Ana'),
    (10, 'Alajuelita'),
    (11, 'San Isidro'),
    (12, 'San Ignacio'),
    (13, 'San Ignacio'),
    (14, 'San Vicente'),
    (15, 'San Pedro'),
    (16, 'San Pablo'),
    (17, 'Santa María'),
    (18, 'Curridabat'),
    (19, 'San Isidro de El General'),
    (20, 'San Pablo'),

    (21, 'Alajuela'),
    (22, 'San Ramón'),
    (23, 'Grecia'),
    (24, 'San Mateo'),
    (25, 'Atenas'),
    (26, 'Naranjo'),
    (27, 'Palmares'),
    (28, 'San Pedro'),
    (29, 'Orotina'),
    (30, 'Quesada'),
    (31, 'Zarcero'),
    (32, 'Sarchí Norte'),
    (33, 'Upala'),
    (34, 'Los Chiles'),
    (35, 'San Rafael'),
    (36, 'Río Cuarto'),

    (37, 'Cartago'),
    (38, 'Paraíso'),
    (39, 'Tres Ríos'),
    (40, 'Juan Viñas'),
    (41, 'Turrialba'),
    (42, 'Pacayas'),
    (43, 'San Rafael'),
    (44, 'El Tejar'),

    (45, 'Heredia'),
    (46, 'Barva'),
    (47, 'Santo Domingo'),
    (48, ' Santa Bárbara'),
    (49, 'San Rafael'),
    (50, 'San Isidro'),
    (51, 'San Antonio'),
    (52, 'San Joaquín'),
    (53, 'San Pablo'),
    (54, 'Puerto Viejo'),

    (55, 'Liberia'),
    (56, 'Nicoya'),
    (57, 'Santa Cruz'),
    (58, 'Bagaces'),
    (59, 'Filadelfia'),
    (60, 'Cañas'),
    (61, 'Las Juntas'),
    (62, 'Tilarán'),
    (63, 'Carmona'),
    (64, 'La Cruz'),
    (65, 'Hojancha'),

    (66, 'Limón'),
    (67, 'Guápiles'),
    (68, 'Siquirres'),
    (69, 'Bribri'),
    (70, 'Matina'),
    (71, 'Guácimo'),
    
    (72, 'Puntarenas'),
    (73, 'Esparza'),
    (74, 'Buenos Aires'),
    (75, 'Miramar'),
    (76, 'Osa'),
    (77, 'Quepos'),
    (78, 'Golfito'),
    (79, 'Coto Brus'),
    (80, 'Parrita'),
    (81, 'Ciudad Neily'),
    (82, 'Jacó');


-- Ubicación
INSERT INTO Ubicacion DEFAULT VALUES
INSERT INTO Ubicacion DEFAULT VALUES
INSERT INTO Ubicacion DEFAULT VALUES
INSERT INTO Ubicacion DEFAULT VALUES
INSERT INTO Ubicacion DEFAULT VALUES
INSERT INTO Ubicacion DEFAULT VALUES
INSERT INTO Ubicacion DEFAULT VALUES
INSERT INTO Ubicacion DEFAULT VALUES

-- Domicilio
INSERT INTO Domicilio (Id, Direccion, DistritoId, Latitud, Longitud)
VALUES
    (1, 'Santa Ana 420 metros este de City Place', 9, 205, 200),
    (2, 'Pavas al lado del aeropuerto', 1, 124, 260);

-- Internacional
INSERT INTO Internacional (Id, NombrePais)
VALUES 
    (3, 'Costa Rica'),
    (4, 'Panamá'),
    (5, 'Nicaragua');

-- Centro Médicos
INSERT INTO Centro_Medico (UbicadoEn, Latitud, Longitud, Nombre)
VALUES
    (8, 23, 23, 'Centro Nacional de Rehabilitación Humberto Araya Rojas'),
    (8, 12, 34.3,'Hospital México'),
    (2, 69, 42.0, 'Hospital Cima'),
    (3, 48, 23, 'Hospital CEACO');

-- Centro_Ubicacion
INSERT INTO Centro_Ubicacion (Id, IdCentro)
VALUES 
    (6, 1),
    (7, 2),
    (8, 3);

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
INSERT INTO Incidente (Codigo, MatriculaTrans, IdEspecialista, CedulaAdmin,
    CedulaTecnicoCoordinador, CedulaTecnicoRevisor, CodigoCita, IdOrigen, IdDestino,
    Modalidad, FechaHoraRegistro, FechaHoraEstimada)
VALUES
    ('TERR123', 'BPC087', 123, 111111111, 117222222, 1173333333, 1, 1, NULL, 'Terrestre', GETDATE(), GETDATE()),
    ('AER123', 'PHP999', 456, 117111111, 117112222, 1171133333, 1, 2, NULL, 'Aéreo', GETDATE(), GETDATE());

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

