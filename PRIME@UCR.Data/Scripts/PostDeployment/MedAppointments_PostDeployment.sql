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
    (GETDATE(), DATEADD(month,-5,getdate()), 18),
	(GETDATE(), DATEADD(month,-4,getdate()), 18),
	(GETDATE(), DATEADD(month,-3,getdate()), 18),
	(GETDATE(), DATEADD(month,-2,getdate()), 18),
	(GETDATE(), DATEADD(month,-1,getdate()), 18);

INSERT INTO CitaMedica(ExpedienteId, CedMedicoAsignado, CentroMedicoId, EstadoId, CitaId)
VALUES
    (18, 22222222, 2, 7, 45),
    (18, 22222222, 2, 7, 46),
    (18, 22222222, 2, 7, 47),
    (18, 22222222, 2, 7, 48),
    (18, 22222222, 2, 7, 49);

/*Inserted by Atenienses ++*/

INSERT INTO MetricasCitaMedica(CitaId, Presion, Peso, Altura)
VALUES
    (45, 100, 55, 165),
    (46, 110, 57, 165),
    (47, 120, 60, 165),
    (48, 100, 62, 165),
    (49, 110, 60, 165);

