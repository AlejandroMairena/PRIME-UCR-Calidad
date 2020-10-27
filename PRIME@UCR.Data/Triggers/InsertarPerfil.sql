﻿
CREATE TRIGGER [InsertarPerfil]
	ON [dbo].[Pertenece]
	INSTEAD OF INSERT
	AS
	BEGIN
		SET NOCOUNT ON
		DECLARE  @IdUsuario nvarchar(450);
		DECLARE  @NombrePerfil nvarchar(60);


		DECLARE ptr CURSOR FOR
			SELECT *
			FROM inserted;

		OPEN ptr

		FETCH NEXT FROM ptr INTO
			@IdUsuario,
			@NombrePerfil

			
        DECLARE @cedula nvarchar(12);

		select TOP 1 @cedula = u.CédulaPersona
			from Usuario u
			where u.Id = @IdUsuario;

		select f.Cédula
		from Funcionario f
		where f.Cédula = @cedula

		IF @@ROWCOUNT < 1 
		BEGIN
			INSERT INTO Funcionario(Cédula)
			VALUES (@cedula)
		END
		

		WHILE @@FETCH_STATUS = 0
		BEGIN
			BEGIN TRANSACTION;		
			
			SELECT REPLACE(@NombrePerfil, ' ', '')

			

			IF @NombrePerfil = 'Administrador'
			BEGIN
				INSERT INTO Administrador (Cédula)VALUES (@cedula)
			END
			
			ELSE IF @NombrePerfil = 'Médico'
			BEGIN
				INSERT INTO Médico (Cédula)VALUES (@cedula)
			END

			ELSE IF @NombrePerfil = 'Gerentemédico'
			BEGIN
				INSERT INTO GerenteMédico (Cédula)VALUES (@cedula)
			END

			ELSE IF @NombrePerfil = 'Especialistatécnicomédico'
			BEGIN
				INSERT INTO EspecialistaTécnicoMédico (Cédula)VALUES (@cedula)
			END

			ELSE IF @NombrePerfil = 'Coordinadortécnicomédico'
			BEGIN
				INSERT INTO CoordinadorTécnicoMédico (Cédula)VALUES (@cedula)
			END

			ELSE IF @NombrePerfil = 'Administradordelacentraldecontrol'
			BEGIN
				INSERT INTO AdministradorCentroDeControl(Cédula)VALUES (@cedula)
			END
			
			COMMIT

			FETCH NEXT FROM ptr INTO
				@IdUsuario,
				@NombrePerfil
		END

		CLOSE ptr
		DEALLOCATE ptr
	    
	END
