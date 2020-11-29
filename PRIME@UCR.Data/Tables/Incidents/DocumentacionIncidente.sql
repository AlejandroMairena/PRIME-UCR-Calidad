CREATE TABLE [dbo].[DocumentacionIncidente]
(
	CodigoIncidente VARCHAR(50)     NOT NULL,
	RazonDeRechazo     NVARCHAR(200)    NULL,
	FOREIGN KEY (CodigoIncidente)
        REFERENCES Incidente (Codigo),
)
