CREATE TRIGGER [InsertarIncidente]
	ON [dbo].[Incidente]
	FOR INSERT
	AS
	BEGIN
		SET NOCOUNT ON
		DECLARE @ID int;
		DECLARE @Modalidad varchar(30);
		DECLARE @FechaHoraRegistro datetime;
		DECLARE ptr CURSOR FOR
			SELECT Id, Modalidad, FechaHoraRegistro
			FROM inserted
			ORDER BY Id;

		OPEN ptr

		FETCH NEXT FROM ptr INTO
		    @ID,
            @Modalidad,
            @FechaHoraRegistro

		WHILE @@FETCH_STATUS = 0
		BEGIN
			DECLARE @next_id int;
			DECLARE @mod char(10);
			SET @mod = UPPER(SUBSTRING(@Modalidad, 1, 3));
			SET @mod = REPLACE(@mod, 'Á', 'A');
			SET @mod = REPLACE(@mod, 'É', 'E');
			SET @mod = REPLACE(@mod, 'Í', 'I');
			SET @mod = REPLACE(@mod, 'Ó', 'O');
			SET @mod = REPLACE(@mod, 'Ú', 'U');

			EXEC @next_id = dbo.SiguienteConsecutivoIncidente @InsertedId = @ID;

			-- build code
			DECLARE @code varchar(50);
			SET @code =
				RIGHT(REPLICATE('0', 4) + CAST(YEAR(@FechaHoraRegistro) AS varchar(10)), 4) + 
				'-' +
				RIGHT(REPLICATE('0', 2) + CAST(MONTH(@FechaHoraRegistro) AS varchar(10)), 2) + 
				'-' +
				RIGHT(REPLICATE('0', 2) + CAST(DAY(@FechaHoraRegistro) AS varchar(10)), 2) + 
				'-' +
				RIGHT(REPLICATE('0', 6) + CAST(@next_id AS varchar(10)), 6) + 
				'-' +
				'IT' +
				'-' +
				@mod;
			
			UPDATE Incidente
			SET Codigo = @code
			WHERE Id = @ID

			FETCH NEXT FROM ptr INTO
			    @ID,
				@Modalidad,
				@FechaHoraRegistro

		END

		CLOSE ptr
		DEALLOCATE ptr
	    
	END
