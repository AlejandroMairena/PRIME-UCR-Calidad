CREATE TABLE [dbo].[Centro_Ubicacion]
(
	Id			INT,
	IdCentro	INT NOT NULL,
	NumeroCama	INT,
	PRIMARY KEY (Id),
	FOREIGN KEY (Id) REFERENCES Ubicacion(Id),
	FOREIGN KEY (IdCentro) REFERENCES Centro_Medico(Id)
);
