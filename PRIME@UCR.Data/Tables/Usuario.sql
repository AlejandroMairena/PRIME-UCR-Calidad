CREATE TABLE [dbo].[Usuario]
(
	CédulaPersona		nvarchar(12)		NOT NULL,
	Correo				nvarchar(40)		NOT NULL,
	Contraseña			nvarchar(20)		NOT NULL,	
	primary key(CédulaPersona, Correo),
	foreign key(CédulaPersona)
		references Persona(Cédula)
);