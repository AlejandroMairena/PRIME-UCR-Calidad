DELETE FROM Cita
DELETE FROM MetricasCitaMedica
DELETE FROM MetricasIncidentes
DELETE FROM Metricas
DELETE FROM Accion
DELETE FROM TipoAccion

DBCC CHECKIDENT ('Cita', RESEED, 0)
DBCC CHECKIDENT ('Metricas', RESEED, 0)
DBCC CHECKIDENT ('Accion', RESEED, 0)

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
