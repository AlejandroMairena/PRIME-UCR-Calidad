CREATE TABLE [dbo].[MetricasCitaMedica]
(
	[Id] INT NOT NULL,
	[CitaId] INT NOT NULL,
	[Presion] VARCHAR(50),
	[Peso] DECIMAL(3, 3),
	[Altura] DECIMAL(3, 3),
	PRIMARY KEY (Id, CitaId),
	FOREIGN KEY (Id, CitaId)
		REFERENCES Metricas(Id, CitaId),
)
