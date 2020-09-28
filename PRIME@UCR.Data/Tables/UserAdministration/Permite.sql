CREATE TABLE [dbo].[Permite]
(
	NombrePerfil			nvarchar(40)	NOT NULL,
	IdPermiso				int				NOT NULL,
	primary key (NombrePerfil, IdPermiso),
	foreign key (NombrePerfil)
		references Perfil(NombrePerfil),
	foreign key (IdPermiso)
		references Permiso(IdPermiso)
);