DELETE FROM CheckList
DELETE FROM Item

DBCC CHECKIDENT ('CheckList', RESEED, 0)
DBCC CHECKIDENT ('Item', RESEED, 0)

insert into CheckList(Nombre, Tipo, Descripcion, Orden)
Values('Salida de Paciente de la Unidad de Internamiento', 'Paciente en origen', 'Se debe realizar al pie de la cama del usuario, previa salida Unidad de Hospitalización', 1);

insert into Item(Nombre, Orden, IDLista)
Values('Asegurar TET, corrugado o interfase de VMNI', 2, 1);

insert into Item(Nombre, Orden, IDLista)
Values('Según dispositivo de oxígeno suplementario (Nasocanula, Venturi, Reservorio o Cánula de alto flujo), valorar la necesidad de colocación de mascarilla quirúrgica', 1, 1);

insert into Item(Nombre, Orden, IDLista)
Values('Asegurar y probar permeabilidad de accesos vasculares', 3, 1);

insert into Item(Nombre, Orden, IDLista)
Values('Colocar electrodos y realizar monitoreos', 4, 1);

insert into Item(Nombre, Orden, IDLista)
Values('Asegurar sondas y otras invasiones', 5, 1);

insert into Item(Nombre, Orden, IDLista)
Values('Frenar cama y camilla de transporte', 6, 1);

insert into Item(Nombre, Orden, IDLista)
Values('Toma de signos vitales antes de mover a la camilla de transporte', 7, 1);

insert into Item(Nombre, Orden, IDLista)
Values('Movilizar en bloque, de forma sincrónica con equipo de apoyo de la unidad', 8, 1);

insert into Item(Nombre, Orden, IDLista)
Values('Colocar dispositivos de sujeción de la camilla de transporte al paciente', 9, 1);

insert into Item(Nombre, Orden, IDLista)
Values('Asegurar bombas de infusión en caso necesario', 10, 1);

insert into Item(Nombre, Orden, IDLista)
Values('Comunicar al centro regulador y al receptor, el tiempo estimado de llegada', 11, 1);


insert into CheckList(Nombre, Tipo, Orden)
Values('BataEquipo y Protección Personal. Con Bata','Colocación equipo' , 2);

insert into Item(Nombre, Orden, IDLista)
Values('Higiene de las manos al menos por 60 segundos con agua jabón o solución alcohólica', 1, 2);

insert into Item(Nombre, Orden, IDLista)
Values('Botas descartables', 2, 2);

insert into Item(Nombre, Orden, IDLista)
Values('Par de guantes (interiores)', 3, 2);

insert into Item(Nombre, Orden, IDLista)
Values('Bata', 4, 2);

insert into Item(Nombre, Orden, IDLista)
Values('Segundo par de guantes (exteriores), extiéndalos de manera que cubran las mangas.', 5, 2);

insert into Item(Nombre, Orden, IDLista)
Values('Respirador N 95 (FFP2)', 6, 2);

insert into Item(Nombre, Orden, IDLista)
Values('Prueba de ajuste', 7, 2);

insert into Item(Nombre, Orden, IDLista)
Values('Lentes de seguridad', 8, 2);

insert into Item(Nombre, Orden, IDLista)
Values('Verificar que la colocación de los lentes no alteró el sello facial del respirador. Repetir prueba de ajuste', 9, 2);

insert into Item(Nombre, Orden, IDLista)
Values('Gorro', 10, 2);

insert into Item(Nombre, Orden, IDLista)
Values('Carreta', 11, 2);

insert into Item(Nombre, Orden, IDLista)
Values('Tercer par de guantes (exteriores), Extiéndalos de manera que cubran las mangas.', 12, 2);
