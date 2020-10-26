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


INSERT INTO Expediente(CedulaPaciente, CedulaMedicoDuenno)
VALUES
    ('6666666','11111111'),
    ('7777777','22222222'),
    ('88888888','999999999'),
    ('1234', '11111111'),
    ('5678', '22222222'),
    ('9101112', '999999999'),
    ('987','11111111'),
    ('654','22222222'),
    ('432','999999999');