CREATE TABLE [dbo].[Pais_Ubicacion]
(
	Id			INT IDENTITY(1,1),
	NombrePais	VARCHAR(30) NOT NULL,
	UbicacionId	INT NOT NULL,
	PRIMARY KEY (Id),
	FOREIGN KEY (NombrePais) REFERENCES Pais(NombrePais),
	FOREIGN KEY (UbicacionId)REFERENCES Ubicacion(Id)
);
