CREATE TABLE [dbo].[Domicilio_Ubicacion]
(
	Id			INT IDENTITY(1,1),
	IdDomicilio INT NOT NULL,
	UbicacionId	INT NOT NULL,
	FOREIGN KEY (IdDomicilio) REFERENCES Domicilio(Id),
	FOREIGN KEY (UbicacionId) REFERENCES Ubicacion(Id)
);
