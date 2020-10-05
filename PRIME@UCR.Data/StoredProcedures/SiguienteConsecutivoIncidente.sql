CREATE PROCEDURE [dbo].[SiguienteConsecutivoIncidente] @InsertedId int
AS
	SET NOCOUNT ON
	DECLARE @code varchar(50);
	DECLARE @id int;
	SELECT TOP 1 @code = i.Codigo
	FROM Incidente i
	WHERE YEAR(i.FechaHoraRegistro) = YEAR(GETDATE())
		AND i.Id <> @InsertedId
	ORDER BY i.Id DESC;


	IF @code IS NULL
		SET @id = 1
	ELSE
		SET @id = CAST(SUBSTRING(@code, 12, 6) as int) + 1
		
RETURN @id
-- YYYY-MM-DD-XXXXXX-IT-MOD
