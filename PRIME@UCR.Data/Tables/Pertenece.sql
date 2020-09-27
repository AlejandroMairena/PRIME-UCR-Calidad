CREATE TABLE [dbo].[Pertenece]
(
	CédulaPersona		nvarchar(12)		NOT NULL,
	CorreoUsuario		nvarchar(40)		NOT NULL,
	NombrePerfil		nvarchar(40)		NOT NULL,
	primary key (CédulaPersona, CorreoUsuario, NombrePerfil),
	foreign key (CédulaPersona, CorreoUsuario)
		references Usuario(CédulaPersona, Correo),
	foreign key (NombrePerfil)
		references Perfil(NombrePerfil)
);
