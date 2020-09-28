CREATE TABLE [dbo].[CoordinadorTécnicoMédico]
(
	CédulaCoordinadorTécnicoMédico		nvarchar(12)		NOT NULL,
	primary key (CédulaCoordinadorTécnicoMédico),
	foreign key (CédulaCoordinadorTécnicoMédico)
		references Funcionario(CédulaFuncionario)
);