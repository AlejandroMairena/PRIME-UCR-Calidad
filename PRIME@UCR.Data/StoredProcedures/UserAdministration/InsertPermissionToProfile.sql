CREATE PROCEDURE [dbo].[InsertPermissionToProfile]
	@idPermission int,
	@idProfile nvarchar(60)
AS
	BEGIN
		INSERT INTO Permite
		VALUES (
			@idProfile,
			@idPermission
		);
	END
