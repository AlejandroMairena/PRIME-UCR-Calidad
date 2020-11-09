CREATE TABLE [dbo].[InstanciaItemHoja]
(
	[Id_Item]					INT			NOT NULL,
	[Id_Lista]					INT			NOT NULL,
	[Codigo_Incidente]			VARCHAR(50)	NOT NULL,
	[Id_Item_Padre]				INT			NOT NULL,
	[Id_Lista_Padre]			INT			NOT NULL,
	[Codigo_Incidente_Padre]	VARCHAR(50)	NOT NULL,
	[FechaHora]					DATETIME	NULL,
	PRIMARY KEY(Id_Item, Id_Lista, Codigo_Incidente),
	FOREIGN KEY(Id_Item, Id_Lista, Codigo_Incidente)
		REFERENCES InstanciaItem(Id_Item, Id_Lista, Codigo_Incidente),
	FOREIGN KEY(Id_Item_Padre, Id_Lista_Padre, Codigo_Incidente_Padre)
		REFERENCES InstanciaItemPadre(Id_Item, Id_Lista, Codigo_Incidente)

)
