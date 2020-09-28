CREATE TABLE [dbo].[Médico]
(
	CédulaMédico		nvarchar(12)		NOT NULL,
	primary key (CédulaMédico),
	foreign key (CédulaMédico)
		references	Funcionario(CédulaFuncionario)
);