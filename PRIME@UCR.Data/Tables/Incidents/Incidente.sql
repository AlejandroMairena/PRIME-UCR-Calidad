CREATE TABLE [dbo].[Incidente]
(
    Id                       INT         IDENTITY(1, 1) ,
    Codigo                   VARCHAR(50) NULL,
    MatriculaTrans           VARCHAR(30) NULL,
    IdEspecialista           INT         NULL, --****
    CedulaAdmin              INT         NULL, --****
    CedulaTecnicoCoordinador INT         NULL, --****
    CedulaTecnicoRevisor     INT         NULL, --****
    CodigoCita               INT         NULL, --****
    IdOrigen                 INT         NULL,
    IdDestino                INT         NULL,
    Modalidad                VARCHAR(30) NOT NULL,
    -- TODO: remove when union class is implemented
    FechaHoraRegistro        DATETIME    NOT NULL,
    FechaHoraEstimada        DATETIME    NOT NULL,
    PRIMARY KEY (Id),
    FOREIGN KEY (Modalidad) REFERENCES Modalidad (Tipo),
    FOREIGN KEY (IdDestino) REFERENCES Ubicacion (Id),
    FOREIGN KEY (IdOrigen) REFERENCES Ubicacion (Id),
    FOREIGN KEY (MatriculaTrans) REFERENCES Unidad_De_Transporte (Matricula),
)
