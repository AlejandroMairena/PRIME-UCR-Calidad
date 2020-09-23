CREATE TABLE [dbo].[Distrito]
(
	NombreDistrito	VARCHAR(30),
	IdCanton		INT NOT NULL,
	Id				INT,
	PRIMARY KEY (Id),
	FOREIGN KEY (IdCanton) REFERENCES Canton(Id)
);
