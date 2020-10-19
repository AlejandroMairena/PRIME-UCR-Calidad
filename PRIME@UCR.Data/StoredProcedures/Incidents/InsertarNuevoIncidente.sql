CREATE PROCEDURE [dbo].[InsertarNuevoIncidente]
    @MatriculaTrans           VARCHAR(30),
    @IdEspecialista           INT,
    @CedulaAdmin              INT,
    @CedulaTecnicoCoordinador INT,
    @CedulaTecnicoRevisor     INT,
    @CodigoCita               INT,
    @IdOrigen                 INT,
    @IdDestino                INT,
    @Modalidad                VARCHAR(30),
    @FechaHoraRegistro        DATETIME,
    @FechaHoraEstimada        DATETIME
AS
    DECLARE @codigo VARCHAR
    INSERT INTO Incidente
    (
        MatriculaTrans,
        IdEspecialista,
        CedulaAdmin,
        CedulaTecnicoCoordinador,
        CedulaTecnicoRevisor,
        CodigoCita,
        IdOrigen,
        IdDestino,
        Modalidad,
        FechaHoraRegistro,
        FechaHoraEstimada
    )
    VALUES
    (
        @MatriculaTrans,
        @IdEspecialista,
        @CedulaAdmin,
        @CedulaTecnicoCoordinador,
        @CedulaTecnicoRevisor,
        @CodigoCita,
        @IdOrigen,
        @IdDestino,
        @Modalidad,
        @FechaHoraRegistro,
        @FechaHoraEstimada
    )
    SELECT TOP 1 @codigo = Codigo 
    FROM Incidente
    ORDER BY Codigo DESC

