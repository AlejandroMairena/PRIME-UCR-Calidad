CREATE TABLE [dbo].[Cita]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,
	[FechaHoraEstimada] DATETIME NOT NULL,
	[FechaHoraCreacion] DATETIME NOT NULL,
	[NumExpediente] INT NOT NULL,
	PRIMARY KEY (Id)
	-- TODO: FK to Expediente
)
