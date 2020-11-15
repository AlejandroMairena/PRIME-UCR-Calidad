CREATE TABLE [dbo].[InstanciaItemPadre]
(
	[Id_Item]			INT			NOT NULL,
	[Id_Lista]			INT			NOT NULL,
	[Codigo_Incidente]	VARCHAR(50)	NOT NULL,
	[FechaHoraInicio]	DATETIME	NULL,
	[FechaHoraFin]		DATETIME	NULL,
	PRIMARY KEY(Id_Item, Id_Lista, Codigo_Incidente),
	FOREIGN KEY(Id_Item, Id_Lista, Codigo_Incidente)
		REFERENCES InstanciaItem(Id_Item, Id_Lista, Codigo_Incidente)
)
