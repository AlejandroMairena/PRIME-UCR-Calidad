CREATE PROCEDURE [dbo].[DeleteUserFromProfile]
	@idUser nvarchar(450),
	@idProfile nvarchar(60)
AS
	BEGIN
		DELETE FROM Pertenece
		WHERE Pertenece.IdUsuario = @idUser AND Pertenece.NombrePerfil = @idProfile;
	END