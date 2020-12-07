/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


INSERT INTO Cita(FechaHoraCreacion, FechaHoraEstimada, IdExpediente)
VALUES
    (GETDATE(), GETDATE(), 18),
    (GETDATE(), GETDATE(), 18),
    (GETDATE(), GETDATE(), 18),
    (GETDATE(), GETDATE(), 18),
    (GETDATE(), GETDATE(), 18);

INSERT INTO CitaMedica(ExpedienteId, CedMedicoAsignado, CentroMedicoId, EstadoId, CitaId)
VALUES
    (18, 22222222, 2, 7, 45),
    (18, 22222222, 2, 7, 46),
    (18, 22222222, 2, 7, 47),
    (18, 22222222, 2, 7, 48),
    (18, 22222222, 2, 7, 49);


