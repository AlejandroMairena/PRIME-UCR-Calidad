CREATE PROCEDURE [dbo].[DeletePermissionFromProfile]
	@idPermission int,
	@idProfile nvarchar(60)
AS
	BEGIN
		DELETE FROM Permite
		WHERE Permite.IdPermiso = @idPermission AND Permite.NombrePerfil = @idProfile;
	END