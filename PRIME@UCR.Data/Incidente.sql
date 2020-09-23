﻿CREATE TABLE [dbo].[Incidente]
(
	Codigo						INT,
	MatriculaTrans				VARCHAR(30) NOT NULL,
	Estado						VARCHAR(30) NOT NULL,
	IdEspecialista				INT NOT NULL, --****
	CedulaAdmin					INT NOT NULL, --****
	CedulaTecnicoCoordinador	INT NOT NULL, --****
	CedulaTecnicoRevisor		INT NOT NULL, --****
	CodigoCita					INT NOT NULL, --****
	IdOrigen					INT NOT NULL, 
	IdDestino					INT NOT NULL,
	Modalidad					VARCHAR(30) NOT NULL,
	PRIMARY KEY (Codigo),
	FOREIGN KEY (Modalidad) REFERENCES Modalidad(Tipo),
	FOREIGN KEY (IdDestino) REFERENCES Ubicacion(Id),
	FOREIGN KEY (IdOrigen)  REFERENCES Ubicacion(Id),
	FOREIGN KEY (MatriculaTrans) REFERENCES Unidad_De_Transporte(Matricula),

)
