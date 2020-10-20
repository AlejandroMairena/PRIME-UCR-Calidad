CREATE TABLE [dbo].[MetricasIncidentes]
(
	[Id] INT NOT NULL,
	[CitaId] INT NOT NULL,
	[CircToraxica] DECIMAL(3, 3),
	[CircAbdominal] DECIMAL(3, 3),
	[Talla] DECIMAL(3, 3),
	PRIMARY KEY (Id, CitaId),
	FOREIGN KEY (Id, CitaId)
		REFERENCES Metricas(Id, CitaId),
)
