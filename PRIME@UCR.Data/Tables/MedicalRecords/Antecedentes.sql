CREATE TABLE [dbo].[Antecedentes]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[IdListaAntecedentes] INT NOT NULL,
	[IdExpediente] INT NOT NULL,
	PRIMARY KEY (Id, IdListaAntecedentes, IdExpediente),
	CONSTRAINT Antecedente_Expediente_FK FOREIGN KEY (IdExpediente) REFERENCES Expediente(Id),
	CONSTRAINT Antecedente_ListaAntecedente_FK FOREIGN KEY (IdListaAntecedentes) REFERENCES ListaAntecedentes(Id)
)
