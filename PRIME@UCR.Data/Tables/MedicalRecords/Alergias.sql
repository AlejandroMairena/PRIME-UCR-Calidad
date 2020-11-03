CREATE TABLE [dbo].[Alergias]
(
	[Id]			INT				NOT NULL	IDENTITY(1, 1),
	[Nombre]		VARCHAR(100)	NOT NULL,
	[IdExpediente]	INT				NULL,
	primary key(Id),
	FOREIGN KEY (IdExpediente) REFERENCES Expediente(Id)
)
