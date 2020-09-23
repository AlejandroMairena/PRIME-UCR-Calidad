CREATE TABLE [dbo].[Pais_Ubicacion]
(
	Id			INT,
	NombrePais	VARCHAR(30) NOT NULL,
	UbicacionId	INT NOT NULL,
	PRIMARY KEY (ID),
	FOREIGN KEY (NombrePais) REFERENCES Pais(NombrePais),
	FOREIGN KEY (UbicacionId)REFERENCES Ubicacion(Id)
);
