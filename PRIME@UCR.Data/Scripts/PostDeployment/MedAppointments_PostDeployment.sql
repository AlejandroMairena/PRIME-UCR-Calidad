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
    (GETDATE(), DATEADD(month,-5,getdate()), 17),
	(GETDATE(), DATEADD(month,-4,getdate()), 17),
	(GETDATE(), DATEADD(month,-3,getdate()), 17),
	(GETDATE(), DATEADD(month,-2,getdate()), 17),
	(GETDATE(), DATEADD(month,-1,getdate()), 17),

    (GETDATE(), DATEADD(month,-5,getdate()), 18),
	(GETDATE(), DATEADD(month,-4,getdate()), 18),
	(GETDATE(), DATEADD(month,-3,getdate()), 18),
	(GETDATE(), DATEADD(month,-2,getdate()), 18),
	(GETDATE(), DATEADD(month,-1,getdate()), 18);

INSERT INTO CitaMedica(ExpedienteId, CedMedicoAsignado, CentroMedicoId, EstadoId, CitaId)
VALUES
    (17, 22222222, 2, 7, 45),
    (17, 22222222, 2, 7, 46),
    (17, 22222222, 2, 7, 47),
    (17, 22222222, 2, 7, 48),
    (17, 22222222, 2, 7, 49), 

    (18, 22222222, 1, 7, 50),
    (18, 22222222, 1, 7, 51),
    (18, 22222222, 1, 7, 52),
    (18, 22222222, 1, 7, 53),
    (18, 22222222, 1, 7, 54);

/*Inserted by Atenienses ++*/

INSERT INTO MetricasCitaMedica(CitaId, Presion, Peso, Altura)
VALUES
    (50, 80, 41, 150),
    (51, 80, 40, 150),
    (52, 90, 43, 150),
    (53, 90, 42, 150),
    (54, 100, 42, 150),

    (45, 100, 55, 165),
    (46, 110, 57, 165),
    (47, 120, 60, 165),
    (48, 100, 62, 165),
    (49, 110, 60, 165);

