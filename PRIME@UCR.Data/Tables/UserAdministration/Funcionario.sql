CREATE TABLE [dbo].[Funcionario]
(
	CédulaFuncionario		nvarchar(12)		NOT NULL,
	primary key (CédulaFuncionario),
	foreign key (CédulaFuncionario)
		references	Persona(Cédula)
);