# T�tulo placeholder 

## b. Tabla de contenidos.

c. Definiciones, acr�nimos y abreviaciones.
d. Introducci�n que explique el objetivo y prop�sito del documento y su estructura.
e. Listado de equipos y miembros de los equipos.
a. Asignaci�n de roles para cada una de las iteraciones.
f. Descripci�n general del sistema a desarrollar.
a. Contexto y situaci�n actual.
b. Problema que resuelve.
c. Interesados del proyecto y tipos de usuarios.
d. Soluci�n propuesta.
e. An�lisis del entorno (estrategia del negocio y objetivos del sistema desde la
perspectiva del negocio, clientes, uso esperado de la aplicaci�n, sistemas legados
relacionados, aspectos regulatorios, supuestos y restricciones del negocio, otras
soluciones existentes).
f. Visi�n del producto.
g. Relaci�n con otros sistemas externos.
h. Descripci�n de los temas (m�dulos) asignados a cada equipo. Descripci�n de los
principales epics asociados al tema(s) a desarrollar por cada uno de los equipos.
Relaci�n con los dem�s m�dulos (temas) del sistema.
i. Requerimientos funcionales (Backlog del producto, solo indicando la referencia al
proyecto en Jira).
j. Mapa de ruta del producto (Product Road Map).
k. Requerimientos no funcionales que debe cumplir toda la aplicaci�n web.
g. Artefactos de bases de datos.
a. Esquema conceptual de la base de datos.
b. Esquema l�gico de la base de datos.
h. Decisiones t�cnicas.
a. Metodolog�as utilizadas y procesos definidos.
b. Artefactos utilizados en el desarrollo del proyecto.
c. Tecnolog�as utilizadas con sus respectivas versiones.
d. Repositorio de c�digo y estrategia git para el proyecto.
e. Definici�n de listo (Definition of Done, DoD).
i. Referencias bibliogr�ficas.

[TOC]


## c. Definiciones, acr�nimos y abreviaciones.

- **Visi�n del producto**: proyecci�n al futuro de qu� ser� el producto y por qu� se crear�.
- **Mapa de ruta del producto**: plan de acci�n que muestra c�mo un producto evolucionar� a lo largo del tiempo.
- **Definici�n de listo**: serie de criterios que toda historia de usuario debe cumplir antes de considerarse completada.

## e. Listado de equipos y miembros de los equipos. 

Diosvier:

-Adri�n Sibaja  B87561

-Erik K�hlmann  B84175

-Esteban Mar�n  B84594

-Daniel Salazar B87214

-Ricardo Franco B83050
    
    
### a. Asignaci�n de roles para cada una de las iteraciones.

**Sprint 0**

Diosvier:
    
-Adri�n Sibaja B87561      Scrum Master

-Erik K�hlmann B84175      Scrum Ambassador

-Esteban Mar�n B84594      Developer

-Daniel Salazar B87214     Developer

-Ricardo Franco B83050     Developer

    
## f. Descripci�n general del sistema a desarrollar.

### a. Contexto y situaci�n actual.

### b. Problema que resuelve.

El servicio de Terapia Respiratoria del CEACO por medio del equipo de Primera Respuesta M�dica Especializada (PRIME), se encarga de la movilizaci�n de todos los pacientes con COVID-19 del pa�s, pero de forma m�s importante de pacientes cr�ticos, que necesitan de una movilizaci�n de centros de bajo nivel de complejidad a hospitales de mayor complejidad.

En la actualidad el equipo PRIME, en conjunto con un equipo de la UCR, ya se encuentra trabajando en las funcionalidades iniciales de una aplicaci�n m�vil a partir de las prioridades de los usuarios especialistas respiratorios.

Dada que la emergencia sanitaria se encuentra en uno de sus picos de contagio, y se esperan nuevas olas de transmisi�n, el equipo PRIME requiere adem�s una primera versi�n de una aplicaci�n web para administrar sus procesos de atenci�n, y que complemente la funcionalidad que ofrecer� la aplicaci�n m�vil. El presente producto corresponde a la implementaci�n de la aplicaci�n web.

### f. Visi�n del producto.

Puede encontrar nuestra visi�n del producto en el link: https://docs.google.com/spreadsheets/d/1XnhCmkLnF6gNzaVUzOPjGgIScgK2AZjcDtBU81P5B8c/edit#gid=1003342746

### g. Relaci�n con otros sistemas externos.

### h. Descripci�n de los temas (m�dulos) asignados a cada equipo. Descripci�n de los principales epics asociados al tema(s) a desarrollar por cada uno de los equipos. Relaci�n con los dem�s m�dulos (temas) del sistema.

**Diosvier - Administraci�n de incidentes**

Para la administraci�n de los traslados (e incidentes) se requiere la implementaci�n de una plataforma que permita el despacho, seguimiento y monitoreo en tiempo real por medio de GPS y mapas tanto para las unidades terrestres, mar�timas y a�reas. Los epics asociados a este tema son:

1. **Crear incidente con datos b�sicos:** para cada incidente, se debe digitar el origen, destino, nombre del paciente, s�ntomas y dem�s datos �tiles para su atenci�n.
2. **Asociar expediente con incidente nuevo:** para cada incidente, se consulta en la base de datos si fue creado un expediente anteriormente o si debe generarse uno nuevo.
3. **Asignaci�n de incidente aprobado:** una vez se ha revisado que el incidente se ingres� correctamente, se aprueba y se procede a asignar una unidad de transporte y un equipo para su atenci�n.
    
Con el m�dulo Dashboard, se debe coordinar el despliegue consistente y limpio de la informaci�n en la interfaz de la aplicaci�n. Con el m�dulo Expedientes M�dicos, se encuentran las consultas a los distintos expedientes generados con anterioridad y las solicitudes para crear nuevos documentos. Por �ltimo, con el m�dulo Listas de chequeo, se requiere coordinar el despliegue de esta informaci�n en la aplicaci�n para su uso durante el traslado.

### i. Requerimientos funcionales (Backlog del producto, solo indicando la referencia al proyecto en Jira).
    
### j. Mapa de ruta del producto (Product Road Map).

Puede encontrar nuestro Product Roadmap en el link: https://docs.google.com/spreadsheets/d/1XnhCmkLnF6gNzaVUzOPjGgIScgK2AZjcDtBU81P5B8c/edit#gid=1653817941

### k. Requerimientos no funcionales que debe cumplir toda la aplicaci�n web.

**Diosvier/Administraci�n de Traslados - Usabilidad**

- La aplicaci�n debe proveer una interfaz sencilla e intuitiva de utilizar para registrar un incidente.
- Si el usuario registrara alg�n incidente incorrectamente, se le debe notificar con un mensaje claro y conciso su error.
- La aplicaci�n debe seguir todos los lineamientos del Manual de Identidad Visual de la Universidad de Costa Rica.


## h. Decisiones t�cnicas.

### a. Metodolog�as utilizadas y procesos definidos.
1. Git: mecanismo para control de versiones. Se opt� por una rama por equipo y una rama por cada desarrollador. Adem�s, se acordaron reglas para subir c�digo a la rama *master*, las cuales est�n especificadas en la definici�n de listo.
2. Scrum: metodolog�a �gil para el desarrollo de software. Se trabaj� con un *scrum of scrums*, con cada equipo auto-organizado trabajando en un m�dulo espec�fico de la aplicaci�n. En los links adjuntos, se pueden consultar los distintos procesos que engloba esta metodolog�a.
    