CREATE TABLE [dbo].[Domicilio]
(
	Id			INT,
	Direccion	VARCHAR(30),
	DistridoId	INT NOT NULL,
	Latitud		INT,
	Longitud	INT,
	PRIMARY KEY (Id),
	FOREIGN KEY (DistridoId) REFERENCES Distrito(Id)
);
