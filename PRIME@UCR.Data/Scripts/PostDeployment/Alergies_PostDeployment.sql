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
DELETE FROM Alergias
DELETE FROM ListaAlergia

DELETE FROM Antecedentes
DELETE FROM ListaAntecedentes

INSERT INTO ListaAlergia(NombreAlergia)
VALUES
    ('Miel'),
    ('Penisilina'),
    ('Nitrofuranos')

INSERT INTO ListaAntecedentes(NombreAntecedente)
VALUES 
    ('Cáncer'),
    ('Diabetes'),
    ('Hipertensión'),
    ('Problemas del corazón');

INSERT INTO Antecedentes(IdExpediente,IdListaAntecedentes)
VALUES 
    (10, 1)
