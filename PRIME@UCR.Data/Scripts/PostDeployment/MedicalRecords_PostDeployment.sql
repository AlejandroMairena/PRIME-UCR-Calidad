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


INSERT INTO Expediente(CedulaPaciente, CedulaMedicoDuenno, FechaCreacion, Clinica)
VALUES
    ('6666666','11111111','10/26/2020','clinica'),
    ('7777777','22222222','10/26/2020','clinica'),
    ('88888888','999999999','10/26/2020','clinica'),
    ('1234', '11111111','10/26/2020','clinica'),
    ('5678', '22222222','10/26/2020','clinica'),
    ('9101112', '999999999','10/26/2020','clinica'),
    ('987','11111111','10/26/2020','clinica'),
    ('654','22222222','10/26/2020','clinica'),
    ('432','999999999','10/26/2020','clinica');