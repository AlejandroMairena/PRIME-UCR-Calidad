CREATE TABLE [dbo].[EstadoIncidente]
(
    IncidenteId     INT         NOT NULL,
    NombreEstado    VARCHAR(50) NOT NULL,
    FechaHora       DATETIME    NOT NULL,
    Activo          BIT         NOT NULL,
    FOREIGN KEY (NombreEstado)
        REFERENCES Estado (Nombre),
    FOREIGN KEY (IncidenteId)
        REFERENCES Incidente (Id),
    PRIMARY KEY (IncidenteId, NombreEstado)
);
