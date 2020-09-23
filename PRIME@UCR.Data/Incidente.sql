CREATE TABLE [dbo].[Incidente]
(
	Codigo			INT,
	MatriculaTrans				VARCHAR(30),
	Estado						VARCHAR(30),
	IdEspecialista				INT, --****
	CedulaAdmin					INT, --****
	CedulaTecnicoCoordinador	INT, --****
	CedulaTecnicoRevisor		INT, --****
	CodigoCita					INT, --****
	IdOrigen					INT, 
	IdDestino					INT,
	Modalidad					VARCHAR(30),
	PRIMARY KEY (Codigo),
	FOREIGN KEY (Modalidad) REFERENCES Modalidad(Tipo),
	FOREIGN KEY (IdDestino) REFERENCES Ubicacion(Id),

)
