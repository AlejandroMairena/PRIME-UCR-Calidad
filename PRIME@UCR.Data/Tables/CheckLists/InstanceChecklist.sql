CREATE TABLE [dbo].[InstanceChecklist]
(
	[InstanciadoId] INT NOT NULL,
	[PlantillaId] int not null,
	Foreign key (PlantillaId) References CheckList (Id),
	[IncidentCod] varchar(50) not null ,
	Foreign key (IncidentCod) References Incidente (Codigo),
	[Completado] bit null default(0),
	[FechaHoraInicio] DATETIME null,
	[FechaHoraFinal] DATETIME null, 
    CONSTRAINT [PK_InstanceChecklist] PRIMARY KEY ([InstanciadoId])
)
