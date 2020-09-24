CREATE TABLE [dbo].[Distrito]
(
	NombreDistrito	VARCHAR(30),
	IdCanton		INT NOT NULL,
	Id				INT IDENTITY(1,1),
	PRIMARY KEY (Id),
	FOREIGN KEY (IdCanton) REFERENCES Canton(Id)
);
