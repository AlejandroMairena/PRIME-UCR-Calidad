CREATE TABLE [dbo].[CheckList](
	[Id]			INT IDENTITY (1, 1) NOT NULL,
	[Nombre]		NVARCHAR (100)		NOT NULL,
	[Tipo]			NVARCHAR (20)		NOT NULL,
	[Descripcion]	NVARCHAR (200)		NULL,
	[Orden]			INT					NOT NULL,
	[NombreImagen]	NVARCHAR (MAX)		DEFAULT 'defaultCheckList.png',
	PRIMARY KEY CLUSTERED ([Id] ASC)
)
