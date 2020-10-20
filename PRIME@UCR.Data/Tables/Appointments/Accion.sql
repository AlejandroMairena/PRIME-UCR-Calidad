CREATE TABLE [dbo].[Accion]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,
	[CitaId] INT NOT NULL,
	[NombreAccion] VARCHAR(50) NOT NULL,
	[Descripcion] VARCHAR(500) NULL,
	PRIMARY KEY (Id, CitaId),
	FOREIGN KEY (CitaId)
		REFERENCES Cita(Id),
	FOREIGN KEY (NombreAccion)
		REFERENCES TipoAccion(Nombre)
)
