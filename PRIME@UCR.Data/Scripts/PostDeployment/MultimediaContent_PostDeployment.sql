use [PRIME@UCR.Data]

go
create trigger BorrarContenidoMultimedia
on MultimediaContent instead of delete
as 
declare @id int
select @id = d.Id
from deleted d
delete from Accion where MultContId = @id
delete from MultimediaContentItem where Id_MultCont = @id
delete from MultimediaContent where Id = @id
