DELETE FROM Administrador
DELETE FROM Médico
DELETE FROM EspecialistaTécnicoMédico
DELETE FROM GerenteMédico
DELETE FROM AdministradorCentroDeControl
DELETE FROM CoordinadorTécnicoMédico
DELETE FROM Funcionario
DELETE FROM Paciente
DELETE FROM Pertenece
DELETE FROM Usuario
DELETE FROM AspNetUsers
DELETE FROM Permite
DELETE FROM Perfil
DELETE FROM Permiso
DELETE FROM Persona

INSERT INTO AspNetUsers
VALUES
    ('ba4d8abf-4eaa-41c3-bbec-d1614bd5277e','admin@admin.com','ADMIN@ADMIN.COM','admin@admin.com','ADMIN@ADMIN.COM','false','AQAAAAEAACcQAAAAEOMAG8dzBZIAIQ5cNqg4ej4WQ/m+lq2JEjiK/LX/8dampRmacvkfImYHLEnsXyBTEQ==','P6SGRZZKE3CLKRVQHAGRBJ52XYJP3G2D','26ceb620-5db9-4876-8345-50ef99d7c851',NULL,0,0,NULL,1,0)
    
INSERT INTO Persona (Cédula, Nombre, PrimerApellido)
VALUES ('12345678', 'Admin', 'Admin');

INSERT INTO Usuario (Id, CédulaPersona)
VALUES
    ('ba4d8abf-4eaa-41c3-bbec-d1614bd5277e', '12345678')

INSERT INTO Funcionario (CédulaFuncionario)
VALUES ('12345678');

INSERT INTO Permiso (IdPermiso, Descripción_Permiso)
VALUES
    -- Administrador
    (1,'Puede hacer uso de todos los módulos de la aplicación'),
    (2,'Puede crear usuarios, así como modificar los perfiles de un usuario y los permisos de un perfil'),
    -- Especialista técnico médico
    (3,'Puede hacer todo lo relacionado a incidentes en aquellos a los que este asignado'),
    (4,'Puede hacer todo lo relacionado a listas de chequeo instanciadas de aquellos incidentes a los cuales esta asignado'),
    (5,'Desde el módulo de incidentes puede solamente ver los expedientes de los pacientes que atiende durante el traslado'),
    (6,'Puede hacer todos los pasos posteriores al de la creación del incidente para los traslados que él atiende'),
    (7,'Puede ver el expediente solamente en modo de consulta'),
    -- Médico
    (8,'Puede administrar todo lo que tenga que ver con expedientes para aquellos pacientes que atiende'),
    (9,'Puede entrar a los incidentes para los cuales él es el encargado'),
    (10,'Puede adjuntar multimedia a las listas de chequeo de los incidentes de los pacientes atendidos por él'),
    (11,'Solo puede ver los expedientes de los pacientes de los cuales él es el encargado'),
    -- Gerente médico
    (12,'Puede ver y administrar todos los expedientes sin importar si los pacientes fueron asignados a él'),
    (13,'Puede entrar a los incidentes por medio de los expedientes, pero unicamente en modo consulta'),
    (14,'Puede ver las estadisticas del sistema'),
    -- Coordinador técnico médico
    (15,'Puede crear plantillas para las listas de chequeo'),
    (16,'Puede administrar todos los incidentes, sin importar si fueron asignados a él'),
        -- se repite el 14
    (17,'Puede hacer todos los pasos de la creación de incidentes para todos ellos'),
    (18,'Desde el módulo de incidentes puede ver los expedientes en modo consulta'),
    -- Administrador del centro de control
    (19,'Puede ver toda la información de los incidentes menos el contenido multimedia'),
    (20,'Puede solamente registrar un incidente, no puede hacer ningún paso previo a la creación'),
    (21,'No puede manipular datos médicos, solo información demográfica, fechas, origen, destino, etc');

INSERT INTO Perfil (NombrePerfil)
VALUES ('Administrador'),
       ('Especialista técnico médico'),
       ('Médico'),
       ('Gerente médico'),
       ('Coordinador técnico médico'),
       ('Administrador de la central de control');

INSERT INTO Permite (IdPermiso, NombrePerfil)
VALUES  (1,'Administrador'),
        (2,'Administrador'),
        (3,'Especialista técnico médico'),
        (4,'Especialista técnico médico'),
        (5,'Especialista técnico médico'),
        (6,'Especialista técnico médico'),
        (7,'Especialista técnico médico'),
        (8,'Médico'),
        (9,'Médico'),
        (10,'Médico'),
        (11,'Médico'),
        (12,'Gerente médico'),
        (13,'Gerente médico'),
        (14,'Gerente médico'),
        (15,'Coordinador técnico médico'),
        (16,'Coordinador técnico médico'),
        (14,'Coordinador técnico médico'),
        (17,'Coordinador técnico médico'),
        (18,'Coordinador técnico médico'),
        (19,'Administrador de la central de control'),
        (20,'Administrador de la central de control'),
        (21,'Administrador de la central de control');

INSERT INTO Pertenece(IdUsuario, NombrePerfil)
VALUES ('ba4d8abf-4eaa-41c3-bbec-d1614bd5277e', 'Administrador');