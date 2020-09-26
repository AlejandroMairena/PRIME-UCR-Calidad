CREATE TABLE [dbo].[Ubicacion]
(
	Id		INT IDENTITY(1,1),
	CedulaDeMedico	INT, --Tiene que ser cambiada a llave foranea cuando se implemente entidad medico
	PRIMARY KEY (Id),
);
