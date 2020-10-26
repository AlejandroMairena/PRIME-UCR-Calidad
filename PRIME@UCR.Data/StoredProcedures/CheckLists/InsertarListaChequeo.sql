CREATE PROCEDURE [dbo].[InsertarListaChequeo]
	@nombre				NVARCHAR(100),
	@tipo				NVARCHAR(20),
	@descripcion		NVARCHAR(200),
	@orden				INT,
	@imagenDescriptiva	NVARCHAR(MAX)
AS
	BEGIN
		declare @MyTable TABLE (
			ListId INT
		)
		INSERT INTO CheckList
		OUTPUT inserted.id INTO @MyTable
		VALUES (
			@nombre,
			@tipo,
			@descripcion,
			@orden,
			@imagenDescriptiva
		)

		SELECT ListId from @MyTable;
	END