CREATE TABLE [dbo].[Domicilio_Ubicacion]
(
	Id			INT,
	IdDomicilio INT NOT NULL,
	UbicacionId	INT NOT NULL,
	PRIMARY KEY (Id),
	FOREIGN KEY (IdDomicilio) REFERENCES Domicilio(Id),
	FOREIGN KEY (UbicacionId) REFERENCES Ubicacion(Id)
);
