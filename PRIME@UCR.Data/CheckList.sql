﻿CREATE TABLE [dbo].[CheckList](
	[Id]			INT IDENTITY (1, 1) NOT NULL,
	[Nombre]		NVARCHAR (50)		NOT NULL,
	[Tipo]			NVARCHAR (20)		NOT NULL,
	[Descripcion]	NVARCHAR (100)		NULL,
	[Imagen]		VARBINARY(50)		NULL,
	[Orden]			INT					NOT NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC)
)
