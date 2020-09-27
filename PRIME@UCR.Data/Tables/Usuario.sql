CREATE TABLE [dbo].[Usuario]
(
	Id					nvarchar(450) NOT NULL,
	CédulaPersona		nvarchar(12)		NOT NULL,
	Correo				nvarchar(40)		NOT NULL,
	Contraseña			nvarchar(20)		NOT NULL,	
	primary key(Id),
	foreign key (Id) 
		references AspNetUsers(Id),
	foreign key(CédulaPersona)
		references Persona(Cédula)

);