CREATE TABLE [dbo].[Domicilio]
(
	Id			INT IDENTITY(1,1),
	Direccion	VARCHAR(150),
	DistridoId	INT NOT NULL,
	Latitud		FLOAT,
	Longitud	FLOAT,
	PRIMARY KEY (Id),
	FOREIGN KEY (DistridoId) REFERENCES Distrito(Id)
);
