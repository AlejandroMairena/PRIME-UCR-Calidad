CREATE TABLE [dbo].[TienePerfil]
(
	CédulaFuncionario		nvarchar(12)		NOT NULL,
	NombrePerfil			nvarchar(40)		NOT NULL,
	primary key (CédulaFuncionario, NombrePerfil),
	foreign key(NombrePerfil)
		references Perfil(NombrePerfil),
	foreign key(CédulaFuncionario)
		references Funcionario(CédulaFuncionario)
);
