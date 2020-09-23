CREATE TABLE [dbo].[Canton]
(	
	NombreCanton	VARCHAR(30),
	NombreProvincia	VARCHAR(30),
	Id				INT,
	PRIMARY KEY (Id),
	FOREIGN KEY (NombreProvincia) REFERENCES Provincia(NombreProvincia)
);
