CREATE TABLE [dbo].[Item]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Nombre ] NVARCHAR(150) NOT NULL, 
    [ImagenDescriptiva] NVARCHAR(MAX) NOT NULL DEFAULT 'defaultCheckList.png', 
    [Descripcion ] NVARCHAR(150) NULL , 
    [Orden ] INT NOT NULL, 
    [IDSuperItem] INT NOT NULL, 
    [IDLista] INT NOT NULL, 
    Foreign key (IDSuperItem) References Item (Id), 
    Foreign key (IDLista) References CheckList (Id)	On Delete Cascade
)
