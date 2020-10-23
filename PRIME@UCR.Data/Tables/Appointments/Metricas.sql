CREATE TABLE [dbo].[Metricas]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,
	[CitaId] INT NOT NULL,
	PRIMARY KEY (Id, CitaId),
	FOREIGN KEY (CitaId)
		REFERENCES Cita(Id)
)
