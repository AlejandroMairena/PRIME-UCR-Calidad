CREATE TABLE [dbo].[Administrador]
(
	CédulaAdministrador		nvarchar(12)		NOT NULL,
	primary key (CédulaAdministrador),
	foreign key (CédulaAdministrador)
		references Funcionario(CédulaFuncionario)
);