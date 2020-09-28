CREATE TABLE [dbo].[Paciente]
(
	CédulaPersona		nvarchar(12)		NOT NULL,
	primary key (CédulaPersona),
	foreign key (CédulaPersona)
		references Persona(Cédula)
);