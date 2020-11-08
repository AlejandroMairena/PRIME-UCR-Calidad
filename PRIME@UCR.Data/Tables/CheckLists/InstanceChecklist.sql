CREATE TABLE [dbo].[InstanceChecklist]
(
	[InstanciadoId] INT not null,
	[PlantillaId] int not null,
	Foreign key (PlantillaId) References CheckList (Id) On Delete Cascade,
	[IncidentCod] varchar(50) not null ,
	Foreign key (IncidentCod) References Incidente (Codigo) On Delete Cascade,
	[Completado] bit null default(0),
	[FechaHoraInicio] DATETIME null,
	[FechaHoraFinal] DATETIME null, 
    CONSTRAINT [PK_InstanceChecklist] PRIMARY KEY ([InstanciadoId])
)
