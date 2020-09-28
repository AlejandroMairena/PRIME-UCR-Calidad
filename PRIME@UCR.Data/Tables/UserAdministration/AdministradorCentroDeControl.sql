CREATE TABLE [dbo].[AdministradorCentroDeControl]
(
	CédulaAdministradorCentroDeControl		nvarchar(12)		NOT NULL,
	primary key (CédulaAdministradorCentroDeControl),
	foreign key (CédulaAdministradorCentroDeControl)
		references	Funcionario(CédulaFuncionario)
);