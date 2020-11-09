CREATE TABLE [dbo].[InstanciaItem]
(
	[Id_Item]			INT			NOT NULL,
	[Id_Lista]			INT			NOT NULL,
	[Codigo_Incidente]	VARCHAR(50)	NOT NULL,
	[Completado]		BIT			NULL,
	PRIMARY KEY(Id_Item, Id_Lista, Codigo_Incidente),
	FOREIGN KEY(Id_Item)
		REFERENCES Item(Id),
	FOREIGN KEY(Id_Lista)
		REFERENCES CheckList(Id),
	FOREIGN KEY(Codigo_Incidente)
		REFERENCES Incidente(Codigo)
)
