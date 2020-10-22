CREATE PROCEDURE [dbo].[InsertarNuevoIncidente]
    @MatriculaTrans           VARCHAR(30),
    @IdEspecialista           INT,
    @CedulaAdmin              NVARCHAR(12),
    @CedulaTecnicoCoordinador INT,
    @CedulaTecnicoRevisor     INT,
    @IdOrigen                 INT,
    @IdDestino                INT,
    @Modalidad                VARCHAR(30),
    @FechaHoraRegistro        DATETIME,
    @FechaHoraEstimada        DATETIME
AS
    INSERT INTO Cita
    (
        FechaHoraCreacion,
        FechaHoraEstimada
    )
    VALUES
    (
        @FechaHoraRegistro,
        @FechaHoraEstimada
    )

    DECLARE @citaId INT
    SET @citaId = SCOPE_IDENTITY()

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
        Modalidad
    )
    VALUES
    (
        @MatriculaTrans,
        @IdEspecialista,
        @CedulaAdmin,
        @CedulaTecnicoCoordinador,
        @CedulaTecnicoRevisor,
        @citaId,
        @IdOrigen,
        @IdDestino,
        @Modalidad
    )

    SELECT TOP 1 @codigo = Codigo 
    FROM Incidente
    WHERE CodigoCita = @citaId

