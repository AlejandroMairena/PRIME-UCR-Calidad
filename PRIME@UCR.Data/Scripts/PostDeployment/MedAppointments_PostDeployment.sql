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

    (GETDATE(), DATEADD(month,-4,getdate()), 18),
	(GETDATE(), DATEADD(month,-3,getdate()), 18),
	(GETDATE(), DATEADD(month,-2,getdate()), 18),
	(GETDATE(), DATEADD(month,-1,getdate()), 18),
	(GETDATE(), DATEADD(month,-1,getdate()), 18),
    
    (GETDATE(), DATEADD(month,-3,getdate()), 16),
	(GETDATE(), DATEADD(month,-2,getdate()), 16),
	(GETDATE(), DATEADD(month,-1,getdate()), 16),
	(GETDATE(), DATEADD(day,-15,getdate()), 16),
	(GETDATE(), DATEADD(day,-1,getdate()), 16),
    
    (GETDATE(), DATEADD(month,-5,getdate()), 15),
	(GETDATE(), DATEADD(month,-4,getdate()), 15),
	(GETDATE(), DATEADD(month,-3,getdate()), 15),
	(GETDATE(), DATEADD(month,-2,getdate()), 15),
	(GETDATE(), DATEADD(month,-1,getdate()), 15),
    
    (GETDATE(), DATEADD(month,-2,getdate()), 14),
	(GETDATE(), DATEADD(month,-1,getdate()), 14),
	(GETDATE(), DATEADD(day,-20,getdate()), 14),
	(GETDATE(), DATEADD(day,-10,getdate()), 14),
    
    (GETDATE(), DATEADD(month,-3,getdate()), 13),
	(GETDATE(), DATEADD(month,-2,getdate()), 13),
	(GETDATE(), DATEADD(month,-1,getdate()), 13),

    (GETDATE(), DATEADD(month,-2,getdate()), 12),
	(GETDATE(), DATEADD(day,-12,getdate()), 12),

	(GETDATE(), DATEADD(day,-1,getdate()), 11);

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
    (18, 22222222, 1, 7, 54),
    
    (16, 22222222, 3, 7, 55),
    (16, 22222222, 3, 7, 56),
    (16, 22222222, 3, 7, 57),
    (16, 22222222, 3, 7, 58),
    (16, 22222222, 3, 7, 59),
    
    (15, 22222222, 4, 7, 60),
    (15, 22222222, 4, 7, 61),
    (15, 22222222, 4, 7, 62),
    (15, 22222222, 4, 7, 63),
    (15, 22222222, 4, 7, 64),
    
    (14, 22222222, 1, 7, 65),
    (14, 22222222, 1, 7, 66),
    (14, 22222222, 1, 7, 67),
    (14, 22222222, 1, 7, 68),
    
    (13, 22222222, 2, 7, 69),
    (13, 22222222, 2, 7, 70),
    (13, 22222222, 2, 7, 71),
    
    (12, 22222222, 3, 7, 72),
    (12, 22222222, 3, 7, 73),

    (11, 22222222, 4, 7, 74);

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
    (49, 110, 60, 165),
    
    (55, 100, 80, 180),
    (56, 111, 90, 180),
    (57, 114, 95, 180),
    (58, 100, 89, 180),
    (59, 120, 90, 180),
    
    (60, 120, 45, 167),
    (61, 110, 46, 167),
    (62, 100, 47, 167),
    (63, 115, 50, 167),
    (64, 120, 49, 167),
    
    (65, 90, 75, 189),
    (66, 100, 78, 189),
    (67, 90, 74, 189),
    (68, 110, 80, 173),

    (69, 90, 75, 189),
    (70, 100, 78, 189),
    (71, 90, 74, 189),

     (72, 100, 80, 180),
    (73, 111, 90, 180),

     (74, 120, 63, 165);
