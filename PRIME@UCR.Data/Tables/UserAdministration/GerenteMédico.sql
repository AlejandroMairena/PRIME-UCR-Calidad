CREATE TABLE [dbo].[GerenteMédico]
(
	CédulaGerenteMédico		nvarchar(12)		NOT NULL,
	primary key (CédulaGerenteMédico),
	foreign key (CédulaGerenteMédico)
		references	Funcionario(CédulaFuncionario)
);