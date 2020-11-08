CREATE TABLE [dbo].[Alergias]
(
	[Id]				INT				NOT NULL,
	[IdListaAlergia]	INT				NOT NULL,
	[IdExpediente]		INT				NOT NULL,
	PRIMARY KEY(Id,IdListaAlergia,IdExpediente),
	CONSTRAINT Alergia_Expediente_FK FOREIGN KEY (IdExpediente) REFERENCES Expediente(Id),
	CONSTRAINT Alergia_ListaAlergia_FK FOREIGN KEY (IdListaAlergia) REFERENCES ListaAlergia(Id)
)
