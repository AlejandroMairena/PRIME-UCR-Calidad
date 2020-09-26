# Prime@UCR

## Definiciones, acr�nimos y abreviaciones

CEACO: Centro Especializado de Atenci�n de Pacientes con COVID-19 

PRIME: Primera Respuesta M�dica Especializada

## Listado de equipos y miembros de los equipos.


### Atenienses++
Integrantes:

- Jose Andr�s V�quez Ram�rez B88635

- Daniela Vargas Sauma B88306 

- Luis Andr�s S�nchez Romero B87367

- Fernando Morales B85338

- Elian Ortega Velasquez B85791

### Asignaci�n de roles para cada una de las iteraciones
#### Sprint 0:

- Atenienses++

| Integrante                     | Rol             |
| -------------------------------| ----------------| 
| Luis Andr�s S�nchez Romero     | Developer       |
| Daniela Vargas Sauma           | Scrum Master    |
| Jose Andr�s V�quez Ramirez     | Developer       |
| Fernando Ezequiel Morales      | Scrum Ambassador|
| Elian Ortega Velasquez         | Developer       |

## Descripci�n general del sistema a desarrollar

### Soluci�n propuesta

Ante el problema descrito anteriormente se da como soluci�n la implementaci�n de una aplicaci�n web por medio de la cual se permite gestionar de mejor manera el proceso ante la atenci�n de incidentes de pacientes COVID-19, inclusive el traslado de los mismos. La idea es brindar un medio digital que permite crear, monitorear, atender y gestionar los incidentes del equipo PRIME del CEACO ante la atenci�n de pacientes. Esta aplicaci�n permitir�a llevar el registro desde el momento del reporte del incidente hasta el momento en que se culmine la atenci�n de dicho paciente.

La aplicaci�n a desarrollar se compone de 4 m�dulos principales; el primero de ellos es el despacho, seguimiento y monitoreo en tiempo real tanto del equipo PRIME como de los pacientes COVID. El segundo la creaci�n de listas de chequeo parametrizables para cada uno de los procesos de atenci�n. El tercero la gesti�n del env�o de informaci�n en tiempo real entre el equipo PRIME en campo y el centro de control, tanto para archivos de texto como de multimedia. Y finalmente la autorizaci�n y administraci�n gr�fica, por medio de cuadros de control, de los diferentes tipos de granularidad de la informaci�n.

El primero m�dulo permitir�a inicialmente la creaci�n de nuevos incidentes de pacientes COVID-19 para su respectiva atenci�n de parte del equipo PRIME. La idea consiste en brindar un medio por el cual se pueden notificar al equipo PRIME del CEACO la lista de incidentes en espera de ser atendidos de acuerdo a su prioridad para que se pueda llevar un mejor control de atenciones. Igualmente, este m�dulo se encargar�a de automatizar el proceso de despacho de pacientes de un centro de salud al siguiente en los casos que sea requerido; o bien desde cualquier zona geogr�fica donde sea necesaria la atenci�n de pacientes COVID-19. Finalmente, este m�dulo se encargar�a de monitorear en tiempo real las unidades PRIME del CEACO, as� como la de pacientes COVID por medio de un mapa nacional que permita visualizar la movilizaci�n de unidades.

Posteriormente el m�dulo de creaci�n de listas de chequeo parametrizables por cada uno de los procesos de atenci�n a pacientes COVID, permite para cada uno de los procesos de atenci�n de un incidente, tener listas de chequeos de requerimientos obligatorios para cada uno de los tipos de traslado. Por ejemplo, para un traslado de un paciente permite tener la serie de pasos que los miembros de equipo PRIME debe cumplir antes de atender el incidente con la finalidad de satisfacer las condiciones impuestas por los medios de salud. Es decir, contar con las distintas capas de guantes, mascarillas, batas de protecci�n, entre otros.

El m�dulo de administraci�n de la informaci�n de las listas de chequeo permite garantizar la correcta emisi�n de mensajes informativos del proceso de atenci�n a la central de mando del equipo PRIME del CEACO. Es decir, este m�dulo se encarga de garantizar el correcto env�o de informaci�n presente en listas de chequeo, o en las actualizaciones del estado de un traslado, por medio de mensajes de texto o mensajes multimedia desde el equipo m�vil del PRIME hasta el equipo del centro de control para su correcta gesti�n de la informaci�n entre todos los entes responsables de los traslados.

Y finalmente el m�dulo de autorizaci�n y administraci�n gr�fica de los distintos niveles de granularidad permite el acceso a la aplicaci�n de un usuario previamente identificado y con sus permisos para poder gestionar, monitorear y administrar cada uno de procesos de atenci�n de incidentes de pacientes COVID de acuerdo a sus permisos sobre el acceso a la informaci�n. Igualmente permite mostrar res�menes visuales, por medio de gr�ficos, de los procesos de atenci�n de pacientes COVID donde entonces se permitir�a llevar una mejor administraci�n del equipo de respuesta PRIME para garantizar su eficiencia en los procesos de atenci�n.

En s�ntesis, la soluci�n del problema dado se puede resumir por medio de un diagrama de flujos de la aplicaci�n a partir del cual se puede visualizar el proceso de atenci�n de pacientes COVID-19. 

![](https://i.imgur.com/BTWHypT.png)
*Figura 1: Flujo de la aplicaci�n web*

El primer paso corresponde a la creaci�n de un nuevo incidente de atenci�n de un paciente COVID; luego el equipo PRIME del CEACO hace el despacho del incidente, ya sea por medio de una visita a la residencia del paciente para administrar su traslado o a un hospital. Para esto el equipo de atenci�n sigue una serie de procedimientos previos a la atenci�n del paciente para verificar el seguimiento de los protocolos de salud indicados. Igualmente, durante la atenci�n del paciente el equipo PRIME puede validar el cumplimiento de una serie de procedimientos m�dicos a aplicarle a dicho paciente de acuerdo a su condici�n; y finalmente una validaci�n de los cumplimientos al finalizar la atenci�n del paciente. 

Todo esto se realiza por medio de las listas de chequeo y el env�o de informaci�n en tiempo real entre el equipo PRIME y la central de control o los respectivos centros de salud asociados al traslado. Adem�s, la aplicaci�n permite monitorear en tiempo real la movilizaci�n de las unidades del equipo PRIME durante cualquier momento de la atenci�n de un incidente.

Adem�s de esto, la aplicaci�n propuesta como soluci�n permite que se d� una administraci�n gr�fica, por medio de cuadros de control de mando, en la cual se pueden visualizar gr�ficos sobre los procesos de atenci�n de pacientes COVID donde se resuma informaci�n administrativa de inter�s. Adem�s de que se manejar�a la seguridad de la aplicaci�n por medio de la asignaci�n de usuarios y perfiles a cada una de las personas con acceso a la aplicaci�n para que se pueda manejar los distintos niveles de granularidad y acceso a la informaci�n de acuerdo al perfil asociado.

### Descripci�n  de  los  temas  (m�dulos)  asignados  a  cada  equipo

#### Atenienses++

El tema del que se encarga de desarrollar Atenienses++ es el de estad�stica y autenticaci�n. Este tema est� caracterizado por el desarrollo de la autenticaci�n de usuarios, es decir, que aquellos que tengan un
usuario asignado, puedan ingresar a la aplicaci�n con este, adem�s de los diversos asuntos pertinentes a este tema, como la seguridad, la regulaci�n de los tipos de usuario (sus caracter�sticas y permisos), adem�s,
en la parte de estad�stica, se ve todo lo que tiene que ver con la representaci�n visual de las estadisticas a mostrar al usuario, mediante el uso de gr�ficos.

Los epics a desarrollar para este tema son los siguientes:

1.  **Administraci�n de usuarios y perfiles**:
El administrador de la aplicaci�n debe contar con m�todos de autenticaci�n de los usuarios de la aplicaci�n para garantizar la confiabilidad del acceso a los datos de la aplicaci�n.

2.  **Administraci�n del dashboard**:
El administrador debe poder visualizar un dashboard con estad�sticas referentes a la informaci�n obtenida a trav�s de la aplicaci�n para tener una representaci�n gr�fica de dicha informaci�n.

3.  **Vista gr�fica de usuarios y perfiles**:
El administrador debe poder visualizar la informacion y permisos asignados a cada usuario de la aplicacion. 

## Artefactos de base de datos

### Esquema l�gico de la base de datos

## Decisiones t�cnicas

### Tecnolog�as utilizadas con sus respectivas versiones.
Para el presente proyecto, se decidi� utilizar las siguientes tecnolog�as:

#### Blazor
Se opt� por el uso de Blazor como framework para el desarrollo de la aplicaci�n web utilizando .NET, desarrollado por microsoft. 
#### Core 3.1
Es un franework de desarrollo de aplicaciones desarrollado por Microsoft.
#### Blazor server 3.1
Esto corresponde a un modelo de alojamiento, de manera que la aplicaci�n se ejecute desde un servidor desde la aplicaci�n ASP.NET
#### Sql server 2019
Corresponde a un sistema de manejo de bases de datos relacionales desarrollado por Microsoft.
#### Bootstrap 4.5.2
Corresponde a un framework de CSS. Es una biblioteca de herramientas para el dise�o de aplicaciones web. 
### Repositorio de c�digo y estrategia git para el proyecto

### Definici�n de listo

## Referencias Bibliogr�ficas
-Ouellette, A. (2017, 20 septiembre). What is Bootstrap: A Beginners Guide. CareerFoundry. https://getbootstrap.com/docs/4.5/getting-started/introduction/
- Roth, D. (2020, 11 agosto). ASP.NET Core Blazor hosting models. Microsoft Docs. https://docs.microsoft.com/en-us/aspnet/core/blazor/hosting-models?view=aspnetcore-3.1
- Roth, D., & Latham, L. (2020, 19 junio). Introduction to ASP.NET Core Blazor. Microsoft Docs. https://docs.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-3.1