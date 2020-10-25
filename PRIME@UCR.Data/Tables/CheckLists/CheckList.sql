CREATE TABLE [dbo].[CheckList](
	[Id]			INT IDENTITY (1, 1) NOT NULL,
	[Nombre]		NVARCHAR (100)		NOT NULL,
	[Tipo]			NVARCHAR (20)		NOT NULL,
	[Descripcion]	NVARCHAR (200)		NULL,
	[Orden]			INT					NOT NULL,
	[ImagenDescriptiva]	NVARCHAR (MAX)		DEFAULT '/images/defaultCheckList.svg',
	PRIMARY KEY CLUSTERED ([Id] ASC)
)
