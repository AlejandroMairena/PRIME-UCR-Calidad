﻿CREATE TABLE [dbo].[Provincia]
(
	NombreProvincia	VARCHAR(30),
	NombrePais	VARCHAR(30) NOT NULL,
	PRIMARY KEY (NombreProvincia),
	FOREIGN KEY (NombrePais) REFERENCES Pais(NombrePais)
);
