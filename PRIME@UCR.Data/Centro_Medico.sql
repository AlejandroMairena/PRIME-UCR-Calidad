CREATE TABLE [dbo].[Centro_Medico]
(
	Id			INT,
	UbicadoEn	INT,
	Latitud		INT,
	Longitud	INT,
	Nombre		VARCHAR(30),
	PRIMARY KEY (Id),
	FOREIGN KEY (UbicadoEn) REFERENCES Distrito(Id)
);
