CREATE TABLE [dbo].[Centro_Ubicacion]
(
	Id			INT,
	IdCentro	INT,
	UbicacionId	INT,
	PRIMARY KEY (Id),
	FOREIGN KEY (IdCentro) REFERENCES Centro_Medico(Id),
	FOREIGN KEY (UbicacionId) REFERENCES Ubicacion(Id)
);
