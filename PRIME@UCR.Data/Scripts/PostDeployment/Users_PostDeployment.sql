DELETE FROM EstadoIncidente
DELETE FROM Estado
DELETE FROM Incidente
DELETE FROM Cita
DELETE FROM Expediente
DELETE FROM AdministradorCentroDeControl
DELETE FROM Trabaja_En
DELETE FROM Centro_Ubicacion
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
    ('a6f7aa70-a038-419f-9945-7c77b093d58f','juan.guzman@prime.com','JUAN.GUZMAN@PRIME.COM','juan.guzman@prime.com','JUAN.GUZMAN@PRIME.COM',0,'AQAAAAEAACcQAAAAEKBfjZVSMkEvJ3kJikd/FETuy1hxI3csK3qM2EwHBlQpgixfBX3tUaxpposHbUfakg==','M7SUOG4MXMPBKLX2BN34HVOG7GRGNIDQ','8caf2844-e5ad-452c-b89f-016d71b5d09e',NULL,0,0,NULL,1,0),
    ('e5cb3edd-44c2-45bd-bd03-a35a4bca66fa','pedro.lopez@prime.com','PEDRO.LOPEZ@PRIME.COM','pedro.lopez@prime.com','PEDRO.LOPEZ@PRIME.COM',0,'AQAAAAEAACcQAAAAEE8FiiPlsBjDIWFC8BcdydBip1i4qXfCkrgIblFJLUl8w+6DXj4wgvdXEaa8yFoMew==','AKPWZUTG53SQX6MZO3HPRDYB5F3V7JVU','6c28fda7-48d1-4ea8-a63b-44d26bf09f6e',NULL,0,0,NULL,1,0),
    ('312e5edf-cf52-4dac-a4c9-1565cb73e13e','maria.loria@prime.com','MARIA.LORIA@PRIME.COM','maria.loria@prime.com','MARIA.LORIA@PRIME.COM',0,'AQAAAAEAACcQAAAAEJSZG3DWeI1LEpwB2rAT/vG2nnDHe8fcO55g5UbNdSkOVSScN08l1172YqVGJlJseA==','6BPGZU6X3TD65J6SJ4JMXGYJO5UW57PU','e6c7f00d-e907-4fad-866f-d45fcc496750',NULL,0,0,NULL,1,0),
    ('5c14b733-ae39-42da-aa31-3eccf37ae33f','diego.carmona@prime.com','DIEGO.CARMONA@PRIME.COM','diego.carmona@prime.com','DIEGO.CARMONA@PRIME.COM',0,'AQAAAAEAACcQAAAAELKV77rFE3S1T//lXdgEYOlHX8+yW3Hng36stZupTvU/bX3Gbcq6r7eBlAgLO+m7fw==','DXEUSGTKRDZISW4R2LKGCK377IHGR7NF','5155b9fd-a9d9-4324-b68b-08b31322b959',NULL,0,0,NULL,1,0),
    ('021d330b-5ffa-4bfc-8159-5393ee0c60d9','wilbert.lopez@prime.com','WILBERT.LOPEZ@PRIME.COM','wilbert.lopez@prime.com','WILBERT.LOPEZ@PRIME.COM',0,'AQAAAAEAACcQAAAAECCsm21knu8qOLl+dGlE6tA+Ut9m4CZ/ty5cNTeElQJDXr3JqeG3YMvNz5PzL8eSpg==','RXP7MNASZSLAKXBK36XF2S5NWX7ALBWH','ab435603-9545-4934-b7b6-1eb6d6cac19b',NULL,0,0,NULL,1,0),
    ('07f9b44f-157e-441b-a428-da0b8affed2e','fabiola.mora@prime.com','FABIOLA.MORA@PRIME.COM','fabiola.mora@prime.com','FABIOLA.MORA@PRIME.COM',0,'AQAAAAEAACcQAAAAEE8QhqiNIwplTUZAh5V/VL2KH/mgtqElC6ctlmlmnXAF+9qKpxSzN0WNC+cMPE536Q==','77K2TE54GPP2X5XZYN2D437NST66SI47','4d53a186-b20d-48ee-b382-f2e0b935e54d',NULL,0,0,NULL,1,0),
    ('25dd51e7-abf3-4efd-82b8-0c790433523c','ana.torres@prime.com','ANA.TORRES@PRIME.COM','ana.torres@prime.com','ANA.TORRES@PRIME.COM',0,'AQAAAAEAACcQAAAAEAK7urGc990Wj5FLe+8L5QRdlNYn0qf+b5LdmBYSJ4p3L+kpAgI6I1lMV7+6putYog==','CLXO4EI5VIBL2M3UEYKM66O6TTB6RQY3','6993e788-1079-4ce2-b7a7-6cf6d3a76f60',NULL,0,0,NULL,1,0),
    ('b97aac93-cfaf-4485-81d4-cb12e652ef68','fabian.hernandez@prime.com','FABIAN.HERNANDEZ@PRIME.COM','fabian.hernandez@prime.com','FABIAN.HERNANDEZ@PRIME.COM',0,'AQAAAAEAACcQAAAAELTZe/dQ0H81AzD632vv2DyJ4XnOrzO5uFlLUlS1KJuSXUhRh+6L2KQMe90GBK3KaQ==','XW7YV5A3STAFWJKB6LNHDKMYSTIFKYCM','43d5cb32-4d1a-4e1e-8285-c585d1c3670b',NULL,0,0,NULL,1,0),
    ('8af8648e-2ccc-4261-a69a-1ad92a691399','teodoro.barquero@prime.com','TEODORO.BARQUERO@PRIME.COM','teodoro.barquero@prime.com','TEODORO.BARQUERO@PRIME.COM',0,'AQAAAAEAACcQAAAAEHAmVY3K6Gp6Eck1SnW5ZGUsKZUTCnXumQIl57pnb60T1cOzifua1IxOUgNbynNopw==','V2RDSXVBAISGPR3DTGK7REY2DWTC3RPX','e20ff5fb-b260-4a6c-b82d-a0a59b369c77',NULL,0,0,NULL,1,0),
    ('df025dd6-57c9-4c3e-8ae2-e319080ca07b','shannon.zuniga@prime.com','SHANNON.ZUNIGA@PRIME.COM','shannon.zuniga@prime.com','SHANNON.ZUNIGA@PRIME.COM',0,'AQAAAAEAACcQAAAAEFK8Ee1g/O7ntPAXnnYKvwoNEwT290f/h+q/hBsz6ybcX1pVBrLG22rtZ78mScSV5g==','FYWVZRUHB46QLLFPFJIOTHJKJMYKAIMM','24e2c1eb-578b-4d2f-9fd4-7cf379447f61',NULL,0,0,NULL,1,0),
    ('e8b07151-040d-4b2c-95dd-03314508c40f','jaikel.rivas@prime.com','JAIKEL.RIVAS@PRIME.COM','jaikel.rivas@prime.com','JAIKEL.RIVAS@PRIME.COM',0,'AQAAAAEAACcQAAAAEII1jGldBK6jolZ2bPIvV84xZsAXe/+ODtiiVvbcKDWzd2QMRUUKnxgfkqCcSXI2pg==','IWPN2PPJ5GEAHFYDJVFRRNEM7PISBQXX','e8270c87-935a-4fcb-b096-8a048264b171',NULL,0,0,NULL,1,0),
    ('95b3d7ae-03ff-4b50-af8b-0e1582750640','irene.ruiz@prime.com','IRENE.RUIZ@PRIME.COM','irene.ruiz@prime.com','IRENE.RUIZ@PRIME.COM',0,'AQAAAAEAACcQAAAAEEDTux/AmLwnRZjedeXcuPSKa/LF1rEbGVb1xUTHzMpV2KDK32Mp8LcFoBfqcLFdmg==','OZOMTPSXPOE7ZI2UJXVKLWSRTU6TM6LE','d06ad0e2-973d-4b03-bdb2-50e0092eb97a',NULL,0,0,NULL,1,0);

INSERT INTO Persona (Cédula, Nombre, PrimerApellido, FechaNacimiento)
VALUES  ('12345678', 'Juan', 'Guzman','2020-10-10'),
        ('23456789', 'Pedro', 'Lopez','2020-10-10'),
        ('34567890', 'Maria', 'Loria','2020-10-10'),
        ('45678901', 'Diego', 'Carmona','2020-10-10'),
        ('56789012', 'Ana', 'Torres','2020-10-10'),
        ('67890123', 'Teodoro', 'Barquero','2020-10-10'),
        ('78901234', 'Shannon', 'Zuñiga','2020-10-10'),
        ('89012345', 'Wilbert', 'Lopez','2020-10-10'),
        ('90123456', 'Irene', 'Ruiz','2020-10-10'),
        ('01234567', 'Fabian', 'Hernandez','2020-10-10'),
        ('11111111', 'Jaikel', 'Rivas','2020-10-10'),
        ('22222222', 'Fabiola', 'Mora','2020-10-10');

INSERT INTO Usuario (Id, CédulaPersona)
VALUES
    ('a6f7aa70-a038-419f-9945-7c77b093d58f','12345678'),
    ('e5cb3edd-44c2-45bd-bd03-a35a4bca66fa','23456789'),
    ('312e5edf-cf52-4dac-a4c9-1565cb73e13e','34567890'),
    ('5c14b733-ae39-42da-aa31-3eccf37ae33f','45678901'),
    ('021d330b-5ffa-4bfc-8159-5393ee0c60d9','56789012'),
    ('07f9b44f-157e-441b-a428-da0b8affed2e','67890123'),
    ('25dd51e7-abf3-4efd-82b8-0c790433523c','78901234'),
    ('b97aac93-cfaf-4485-81d4-cb12e652ef68','89012345'),
    ('8af8648e-2ccc-4261-a69a-1ad92a691399','90123456'),
    ('df025dd6-57c9-4c3e-8ae2-e319080ca07b','01234567'),
    ('e8b07151-040d-4b2c-95dd-03314508c40f','11111111'),
    ('95b3d7ae-03ff-4b50-af8b-0e1582750640','22222222');

INSERT INTO Funcionario (Cédula)
VALUES ('12345678'),
       ('23456789'),
       ('34567890'),
       ('45678901'),
       ('56789012'),
       ('67890123'),
       ('78901234'),
       ('89012345'),
       ('90123456'),
       ('01234567'),
       ('11111111'),
       ('22222222');

INSERT INTO Médico(Cédula)
VALUES ('56789012'),
       ('67890123');

INSERT INTO Administrador(Cédula)
VALUES ('12345678'),
       ('23456789');

INSERT INTO AdministradorCentroDeControl(Cédula)
VALUES ('11111111'),
       ('22222222'),
       ('12345678');

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
VALUES ('a6f7aa70-a038-419f-9945-7c77b093d58f', 'Administrador'),
       ('e5cb3edd-44c2-45bd-bd03-a35a4bca66fa','Administrador'),
       ('312e5edf-cf52-4dac-a4c9-1565cb73e13e','Especialista técnico médico'),
       ('5c14b733-ae39-42da-aa31-3eccf37ae33f','Especialista técnico médico'),
       ('021d330b-5ffa-4bfc-8159-5393ee0c60d9','Médico'),
       ('07f9b44f-157e-441b-a428-da0b8affed2e','Médico'),
       ('25dd51e7-abf3-4efd-82b8-0c790433523c','Gerente médico'),
       ('b97aac93-cfaf-4485-81d4-cb12e652ef68','Gerente médico'),
       ('8af8648e-2ccc-4261-a69a-1ad92a691399','Coordinador técnico médico'),
       ('df025dd6-57c9-4c3e-8ae2-e319080ca07b','Coordinador técnico médico'),
       ('e8b07151-040d-4b2c-95dd-03314508c40f','Administrador de la central de control'),
       ('95b3d7ae-03ff-4b50-af8b-0e1582750640','Administrador de la central de control');
