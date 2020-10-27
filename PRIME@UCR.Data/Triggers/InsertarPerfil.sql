CREATE TRIGGER [InsertarPerfil] ON [dbo].[Pertenece]
AFTER INSERT
AS
BEGIN
	DECLARE  @IdUsuario NVARCHAR(450),
		     @NombrePerfil NVARCHAR(60);
	
	DECLARE ptr CURSOR FOR 
		SELECT i.IdUsuario, i.NombrePerfil 
		FROM inserted as i;

	OPEN ptr

	FETCH NEXT FROM ptr INTO @IdUsuario, @NombrePerfil
	WHILE @@FETCH_STATUS = 0 BEGIN
		 DECLARE @cedula nvarchar(12);
		 SELECT @cedula = p.Cédula
		 FROM Usuario as u JOIN Persona as p on u.CédulaPersona = p.Cédula
		 WHERE u.Id = @IdUsuario

		 DECLARE @amount INT 
		 SELECT @amount = COUNT(*)
			FROM Funcionario as f
			WHERE f.Cédula = @cedula
		 IF @amount = 0
		 BEGIN
			INSERT INTO Funcionario VALUES (@cedula)
		 END
		 IF @NombrePerfil = 'Administrador'
		 BEGIN
			INSERT INTO Administrador VALUES (@cedula)
		 END
		 IF @NombrePerfil = 'Especialista técnico médico'
		 BEGIN
			INSERT INTO EspecialistaTécnicoMédico VALUES (@cedula)
		 END
		 IF @NombrePerfil = 'Médico'
		 BEGIN
			INSERT INTO Médico VALUES (@cedula)
		 END
		 IF @NombrePerfil = 'Gerente médico'
		 BEGIN
			INSERT INTO GerenteMédico VALUES (@cedula)
		 END
		 IF @NombrePerfil = 'Coordinador técnico médico'
		 BEGIN
			INSERT INTO CoordinadorTécnicoMédico VALUES (@cedula)
		 END
		 IF @NombrePerfil = 'Administrador de la central de control'
		 BEGIN
			INSERT INTO AdministradorCentroDeControl VALUES (@cedula)
		 END
		 FETCH NEXT FROM ptr INTO @IdUsuario, @NombrePerfil
	END
CLOSE ptr
DEALLOCATE ptr
END;
	    
	