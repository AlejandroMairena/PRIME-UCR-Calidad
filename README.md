# Prime@UCR

## Definiciones, acrónimos y abreviaciones

CEACO: Centro Especializado de Atención de Pacientes con COVID-19 

PRIME: Primera Respuesta Médica Especializada

## Listado de equipos y miembros de los equipos.


### Atenienses++
Integrantes:

- Jose Andrés Víquez Ramírez B88635

- Daniela Vargas Sauma B88306 

- Luis Andrés Sánchez Romero B87367

- Fernando Morales B85338

- Elian Ortega Velasquez B85791

### Asignación de roles para cada una de las iteraciones
#### Sprint 0:

- Atenienses++

| Integrante                     | Rol             |
| -------------------------------| ----------------| 
| Luis Andrés Sánchez Romero     | Developer       |
| Daniela Vargas Sauma           | Scrum Master    |
| Jose Andrés Víquez Ramirez     | Developer       |
| Fernando Ezequiel Morales      | Scrum Ambassador|
| Elian Ortega Velasquez         | Developer       |

## Descripción general del sistema a desarrollar

### Solución propuesta

Ante el problema descrito anteriormente se da como solución la implementación de una aplicación web por medio de la cual se permite gestionar de mejor manera el proceso ante la atención de incidentes de pacientes COVID-19, inclusive el traslado de los mismos. La idea es brindar un medio digital que permite crear, monitorear, atender y gestionar los incidentes del equipo PRIME del CEACO ante la atención de pacientes. Esta aplicación permitiría llevar el registro desde el momento del reporte del incidente hasta el momento en que se culmine la atención de dicho paciente.

La aplicación a desarrollar se compone de 4 módulos principales; el primero de ellos es el despacho, seguimiento y monitoreo en tiempo real tanto del equipo PRIME como de los pacientes COVID. El segundo la creación de listas de chequeo parametrizables para cada uno de los procesos de atención. El tercero la gestión del envío de información en tiempo real entre el equipo PRIME en campo y el centro de control, tanto para archivos de texto como de multimedia. Y finalmente la autorización y administración gráfica, por medio de cuadros de control, de los diferentes tipos de granularidad de la información.

El primero módulo permitiría inicialmente la creación de nuevos incidentes de pacientes COVID-19 para su respectiva atención de parte del equipo PRIME. La idea consiste en brindar un medio por el cual se pueden notificar al equipo PRIME del CEACO la lista de incidentes en espera de ser atendidos de acuerdo a su prioridad para que se pueda llevar un mejor control de atenciones. Igualmente, este módulo se encargaría de automatizar el proceso de despacho de pacientes de un centro de salud al siguiente en los casos que sea requerido; o bien desde cualquier zona geográfica donde sea necesaria la atención de pacientes COVID-19. Finalmente, este módulo se encargaría de monitorear en tiempo real las unidades PRIME del CEACO, así como la de pacientes COVID por medio de un mapa nacional que permita visualizar la movilización de unidades.

Posteriormente el módulo de creación de listas de chequeo parametrizables por cada uno de los procesos de atención a pacientes COVID, permite para cada uno de los procesos de atención de un incidente, tener listas de chequeos de requerimientos obligatorios para cada uno de los tipos de traslado. Por ejemplo, para un traslado de un paciente permite tener la serie de pasos que los miembros de equipo PRIME debe cumplir antes de atender el incidente con la finalidad de satisfacer las condiciones impuestas por los medios de salud. Es decir, contar con las distintas capas de guantes, mascarillas, batas de protección, entre otros.

El módulo de administración de la información de las listas de chequeo permite garantizar la correcta emisión de mensajes informativos del proceso de atención a la central de mando del equipo PRIME del CEACO. Es decir, este módulo se encarga de garantizar el correcto envío de información presente en listas de chequeo, o en las actualizaciones del estado de un traslado, por medio de mensajes de texto o mensajes multimedia desde el equipo móvil del PRIME hasta el equipo del centro de control para su correcta gestión de la información entre todos los entes responsables de los traslados.

Y finalmente el módulo de autorización y administración gráfica de los distintos niveles de granularidad permite el acceso a la aplicación de un usuario previamente identificado y con sus permisos para poder gestionar, monitorear y administrar cada uno de procesos de atención de incidentes de pacientes COVID de acuerdo a sus permisos sobre el acceso a la información. Igualmente permite mostrar resúmenes visuales, por medio de gráficos, de los procesos de atención de pacientes COVID donde entonces se permitiría llevar una mejor administración del equipo de respuesta PRIME para garantizar su eficiencia en los procesos de atención.

En síntesis, la solución del problema dado se puede resumir por medio de un diagrama de flujos de la aplicación a partir del cual se puede visualizar el proceso de atención de pacientes COVID-19. 

![](https://i.imgur.com/BTWHypT.png)
*Figura 1: Flujo de la aplicación web*

El primer paso corresponde a la creación de un nuevo incidente de atención de un paciente COVID; luego el equipo PRIME del CEACO hace el despacho del incidente, ya sea por medio de una visita a la residencia del paciente para administrar su traslado o a un hospital. Para esto el equipo de atención sigue una serie de procedimientos previos a la atención del paciente para verificar el seguimiento de los protocolos de salud indicados. Igualmente, durante la atención del paciente el equipo PRIME puede validar el cumplimiento de una serie de procedimientos médicos a aplicarle a dicho paciente de acuerdo a su condición; y finalmente una validación de los cumplimientos al finalizar la atención del paciente. 

Todo esto se realiza por medio de las listas de chequeo y el envío de información en tiempo real entre el equipo PRIME y la central de control o los respectivos centros de salud asociados al traslado. Además, la aplicación permite monitorear en tiempo real la movilización de las unidades del equipo PRIME durante cualquier momento de la atención de un incidente.

Además de esto, la aplicación propuesta como solución permite que se dé una administración gráfica, por medio de cuadros de control de mando, en la cual se pueden visualizar gráficos sobre los procesos de atención de pacientes COVID donde se resuma información administrativa de interés. Además de que se manejaría la seguridad de la aplicación por medio de la asignación de usuarios y perfiles a cada una de las personas con acceso a la aplicación para que se pueda manejar los distintos niveles de granularidad y acceso a la información de acuerdo al perfil asociado.

### Descripción  de  los  temas  (módulos)  asignados  a  cada  equipo

#### Atenienses++

El tema del que se encarga de desarrollar Atenienses++ es el de estadística y autenticación. Este tema está caracterizado por el desarrollo de la autenticación de usuarios, es decir, que aquellos que tengan un
usuario asignado, puedan ingresar a la aplicación con este, además de los diversos asuntos pertinentes a este tema, como la seguridad, la regulación de los tipos de usuario (sus características y permisos), además,
en la parte de estadística, se ve todo lo que tiene que ver con la representación visual de las estadisticas a mostrar al usuario, mediante el uso de gráficos.

Los epics a desarrollar para este tema son los siguientes:

1.  **Administración de usuarios y perfiles**:
El administrador de la aplicación debe contar con métodos de autenticación de los usuarios de la aplicación para garantizar la confiabilidad del acceso a los datos de la aplicación.

2.  **Administración del dashboard**:
El administrador debe poder visualizar un dashboard con estadísticas referentes a la información obtenida a través de la aplicación para tener una representación gráfica de dicha información.

3.  **Vista gráfica de usuarios y perfiles**:
El administrador debe poder visualizar la informacion y permisos asignados a cada usuario de la aplicacion. 

## Artefactos de base de datos

### Esquema lógico de la base de datos

![](https://i.imgur.com/e6I08mP.png)

A continuación se muestra el esquema lógico dividido en los correspondientes a cada equipo: 

Atenienses++ (Administración de Usuarios):
![](https://i.imgur.com/6xnF6eE.png)

Diosvier (Administración de Translados) :
![](https://i.imgur.com/HE20mND.png)

Legados (Control del Procedimientos) :
![](https://i.imgur.com/Mobj6U3.png)

Drim Team (Gestión de Información) :
![](https://i.imgur.com/te7oeHP.png)


## Decisiones técnicas

### Tecnologías utilizadas con sus respectivas versiones.
Para el presente proyecto, se decidió utilizar las siguientes tecnologías:

#### Blazor
Se optó por el uso de Blazor como framework para el desarrollo de la aplicación web utilizando .NET, desarrollado por microsoft. 
#### Core 3.1
Es un franework de desarrollo de aplicaciones desarrollado por Microsoft.
#### Blazor server 3.1
Esto corresponde a un modelo de alojamiento, de manera que la aplicación se ejecute desde un servidor desde la aplicación ASP.NET
#### Sql server 2019
Corresponde a un sistema de manejo de bases de datos relacionales desarrollado por Microsoft.
#### Bootstrap 4.5.2
Corresponde a un framework de CSS. Es una biblioteca de herramientas para el diseño de aplicaciones web. 
### Repositorio de código y estrategia git para el proyecto

#### Repositorio 
Se usó Bitbucket para guardar el repositorio que contiene el código del desarrollo del proyecto, el cual se encuentra en el siguiente enlace: https://bitbucket.org/cristian_quesadalopez/ecci_ci0128_ii2020_g01_pi/src/master/. 

#### Estrategia git para el proyecto

La estrategia de git que se utlizó fue la de branching; en esta forma de trabajo, la estructura es la siguiente: inicialmente, se tiene el branch de master, luego por equipo, se tiene un branch aparte y, por cada desarrollador indivual, se tiene un branch personal. Al ser 4 equipos que forman parte del proyecto, se tendrá, además del branch master, 1 branch por equipo + 5 branches por los integrantes de los equipos; por lo que, en total, se tendrían 25 branches diferentes. 

Cabe destacar que en la branch master únicamente se le harán commits y merge al final de las iteraciones; esto para tener releases funcionales en el master y poder asegurar que el la aplicación haya sido testeada correctamente antes de ser agregada al branch. 

En más detalle, los desarrolladores que vayan implementando funcionalidades a la aplicación van realizando commits a su branch por la duración de un sprint. Cuando sea necesario, estos desarrolladores pueden realizar merges con su branch de equipo, ya que otros miembros del equipo pueden ir utilizando las funcionalidades que sus compañeros hayan implementado. Una vez que se termine la iteración, se hará merge entre los diferentes branches de los equipos principales, para de esta forma, poder hacer un único merge con el master, para así evitar la máxima cantidad de errores que pueden ocurrir. 

### Definición de listo

## Referencias Bibliográficas
-Ouellette, A. (2017, 20 septiembre). What is Bootstrap: A Beginners Guide. CareerFoundry. https://getbootstrap.com/docs/4.5/getting-started/introduction/
- Roth, D. (2020, 11 agosto). ASP.NET Core Blazor hosting models. Microsoft Docs. https://docs.microsoft.com/en-us/aspnet/core/blazor/hosting-models?view=aspnetcore-3.1
- Roth, D., & Latham, L. (2020, 19 junio). Introduction to ASP.NET Core Blazor. Microsoft Docs. https://docs.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-3.1