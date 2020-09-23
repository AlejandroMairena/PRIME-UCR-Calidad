CREATE TABLE [dbo].[Domicilio_Ubicacion]
(
	Id			INT,
	IdDomicilio INT,
	UbicacionId	INT,
	PRIMARY KEY (Id),
	FOREIGN KEY (IdDomicilio) REFERENCES Domicilio(Id),
	FOREIGN KEY (UbicacionId) REFERENCES Ubicacion(Id)
);
