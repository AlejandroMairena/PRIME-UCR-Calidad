CREATE TABLE [dbo].[EstadoIncidente]
(
    CodigoIncidente VARCHAR(50) NOT NULL,
    NombreEstado    VARCHAR(50) NOT NULL,
    FechaHora       DATETIME    NOT NULL,
    Activo          BIT         NOT NULL,
    FOREIGN KEY (NombreEstado)
        REFERENCES Estado (Nombre),
    FOREIGN KEY (CodigoIncidente)
        REFERENCES Incidente (Codigo),
    PRIMARY KEY (CodigoIncidente, NombreEstado)
);
