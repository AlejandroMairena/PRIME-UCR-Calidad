CREATE TABLE [dbo].[Item]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
    [Nombre ] NVARCHAR(150) NOT NULL, 
    [ImagenDescriptiva] NVARCHAR(MAX) NOT NULL DEFAULT 'defaultCheckList.png', 
    [Descripcion ] NVARCHAR(150) NULL, 
    [Orden ] INT NOT NULL, 
    [IDSuperItem] INT NULL, 
    [IDLista] INT NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC),
    Foreign key (IDSuperItem) References Item (Id), 
    Foreign key (IDLista) References CheckList (Id)	On Delete Cascade
)
