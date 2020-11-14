﻿DELETE FROM Cita
DELETE FROM MetricasCitaMedica
DELETE FROM MetricasIncidentes
DELETE FROM Metricas
DELETE FROM Accion
DELETE FROM TipoAccion

DBCC CHECKIDENT ('Cita', RESEED, 0)
DBCC CHECKIDENT ('Metricas', RESEED, 0)

INSERT INTO TipoAccion (Nombre, EsDeCitaMedica, EsDeIncidente)
VALUES
	('Complicación en traslado', 0, 1),
	('Condición de colecta de paciente', 0, 1),
	('Condición de entrega de paciente', 0, 1),
	('Síntomas', 1, 1),
	('Examen', 1, 0),
	('Diagnóstico', 1, 0),
	('Recetas médicas', 1, 0),
	('Referencia a especialista', 1, 0)

INSERT INTO Cita (FechaHoraCreacion, FechaHoraEstimada)
VALUES
	(GETDATE(),DATEADD(day, -1, getdate())),
	(GETDATE(),DATEADD(day, -2, getdate())),
	(GETDATE(),DATEADD(day, -2, getdate())),
	(GETDATE(),DATEADD(day, -2, getdate())),
	(GETDATE(),DATEADD(day, -3, getdate())),
	(GETDATE(),DATEADD(day, -3, getdate())),
	(GETDATE(),DATEADD(day, -4, getdate())),
	(GETDATE(),DATEADD(day, -5, getdate())),
	(GETDATE(),DATEADD(day, -6, getdate())),
	(GETDATE(),DATEADD(day,-6, getdate())),
	(GETDATE(),DATEADD(day, -6, getdate())),

	--Created by Atenineses
	(GETDATE(),DATEADD(day, 1, getdate())),
	(GETDATE(),DATEADD(day, 1, getdate())),
	(GETDATE(),DATEADD(day, 1, getdate())),
	(GETDATE(),DATEADD(day, 1, getdate())),
	(GETDATE(),DATEADD(day, 2, getdate())),
	(GETDATE(),DATEADD(day, 2, getdate())),
	(GETDATE(),DATEADD(day, 2, getdate())),
	(GETDATE(),DATEADD(day, 2, getdate())),
	(GETDATE(),DATEADD(day, 3, getdate())),
	(GETDATE(),DATEADD(day, 3, getdate())),
	(GETDATE(),DATEADD(day, 3, getdate())),
	(GETDATE(),DATEADD(day, 3, getdate())),
	(GETDATE(),DATEADD(day, 4, getdate())),
	(GETDATE(),DATEADD(day, 4, getdate())),
	(GETDATE(),DATEADD(day, 5, getdate())),
	(GETDATE(),DATEADD(day, 5, getdate())),
	(GETDATE(),DATEADD(day, 5, getdate())),
	(GETDATE(),DATEADD(day, 6, getdate())),
	(GETDATE(),DATEADD(day, 7, getdate())),
	(GETDATE(),DATEADD(day, 7, getdate())),
	(GETDATE(),DATEADD(day, 7, getdate())),
	(GETDATE(),DATEADD(day, 7, getdate())),
	(GETDATE(),DATEADD(day, 7, getdate())),
	(GETDATE(),DATEADD(day, 7, getdate())),
	(GETDATE(),DATEADD(day, 8, getdate())),
	(GETDATE(),DATEADD(day, 8, getdate())),
	(GETDATE(),DATEADD(day, 8, getdate())),
	(GETDATE(),DATEADD(day, 9, getdate())),
	(GETDATE(),DATEADD(day, 9, getdate())),
	(GETDATE(),DATEADD(day, 9, getdate())),
	(GETDATE(),DATEADD(day, 9, getdate())),
	(GETDATE(),DATEADD(day, 10, getdate()))


