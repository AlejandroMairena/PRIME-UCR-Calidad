CREATE TABLE [dbo].[EspecialistaTécnicoMédico]
(
	CédulaEspecialistaTécnicoMédico		nvarchar(12)		NOT NULL,
	primary key (CédulaEspecialistaTécnicoMédico),
	foreign key (CédulaEspecialistaTécnicoMédico)
		references Funcionario(CédulaFuncionario)
);