use [PRIME@UCR.Data]

go
create trigger [BorrarContenidoMultimedia]
on [dbo].[MultimediaContent] instead of delete
as 
begin
declare @id int
select @id = d.Id
from deleted d
delete from Accion where MultContId = @id
delete from MultimediaContentItem where Id_MultCont = @id
delete from MultimediaContent where Id = @id
end;