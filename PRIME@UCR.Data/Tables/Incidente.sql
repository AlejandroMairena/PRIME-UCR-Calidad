CREATE TABLE [dbo].[Incidente]
(
    Codigo                   VARCHAR(30),
    MatriculaTrans           VARCHAR(30) NOT NULL,
    Estado                   VARCHAR(30) NOT NULL,
    IdEspecialista           INT         NOT NULL, --****
    CedulaAdmin              INT         NOT NULL, --****
    CedulaTecnicoCoordinador INT         NOT NULL, --****
    CedulaTecnicoRevisor     INT         NOT NULL, --****
    CodigoCita               INT         NOT NULL, --****
    IdOrigen                 INT         NOT NULL,
    IdDestino                INT         NOT NULL,
    Modalidad                VARCHAR(30) NOT NULL,
    -- TODO: remove when union class is implemented
    FechaHoraRegistro        DATETIME    NOT NULL,
    FechaHoraEstimada        DATETIME    NOT NULL,
    PRIMARY KEY (Codigo),
    FOREIGN KEY (Modalidad) REFERENCES Modalidad (Tipo),
    FOREIGN KEY (IdDestino) REFERENCES Ubicacion (Id),
    FOREIGN KEY (IdOrigen) REFERENCES Ubicacion (Id),
    FOREIGN KEY (MatriculaTrans) REFERENCES Unidad_De_Transporte (Matricula),

)
