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
    (GETDATE(), GETDATE(), 1),
    (GETDATE(), GETDATE(), 1),
    (GETDATE(), GETDATE(), 1),
    (GETDATE(), GETDATE(), 1),
    (GETDATE(), GETDATE(), 1);

INSERT INTO CitaMedica(ExpedienteId, CedMedicoAsignado, CentroMedicoId, EstadoId, CitaId)
VALUES
    (1, 22222222, 2, 1, 45),
    (1, 22222222, 2, 1, 46),
    (1, 22222222, 2, 1, 47),
    (1, 22222222, 2, 1, 48),
    (1, 22222222, 2, 1, 49);


