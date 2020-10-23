CREATE PROCEDURE [dbo].[InsertUserToProfile]
    @idUsuario nvarchar(450),
    @nombrePerfil nvarchar(60)
AS
    BEGIN
        INSERT INTO Pertenece
        VALUES (
            @idUsuario,
            @NombrePerfil
        );
    END