# Título placeholder 

## b. Tabla de contenidos.

c. Definiciones, acrónimos y abreviaciones.
d. Introducción que explique el objetivo y propósito del documento y su estructura.
e. Listado de equipos y miembros de los equipos.
a. Asignación de roles para cada una de las iteraciones.
f. Descripción general del sistema a desarrollar.
a. Contexto y situación actual.
b. Problema que resuelve.
c. Interesados del proyecto y tipos de usuarios.
d. Solución propuesta.
e. Análisis del entorno (estrategia del negocio y objetivos del sistema desde la
perspectiva del negocio, clientes, uso esperado de la aplicación, sistemas legados
relacionados, aspectos regulatorios, supuestos y restricciones del negocio, otras
soluciones existentes).
f. Visión del producto.
g. Relación con otros sistemas externos.
h. Descripción de los temas (módulos) asignados a cada equipo. Descripción de los
principales epics asociados al tema(s) a desarrollar por cada uno de los equipos.
Relación con los demás módulos (temas) del sistema.
i. Requerimientos funcionales (Backlog del producto, solo indicando la referencia al
proyecto en Jira).
j. Mapa de ruta del producto (Product Road Map).
k. Requerimientos no funcionales que debe cumplir toda la aplicación web.
g. Artefactos de bases de datos.
a. Esquema conceptual de la base de datos.
b. Esquema lógico de la base de datos.
h. Decisiones técnicas.
a. Metodologías utilizadas y procesos definidos.
b. Artefactos utilizados en el desarrollo del proyecto.
c. Tecnologías utilizadas con sus respectivas versiones.
d. Repositorio de código y estrategia git para el proyecto.
e. Definición de listo (Definition of Done, DoD).
i. Referencias bibliográficas.

[TOC]


## c. Definiciones, acrónimos y abreviaciones.

- **Visión del producto**: proyección al futuro de qué será el producto y por qué se creará.
- **Mapa de ruta del producto**: plan de acción que muestra cómo un producto evolucionará a lo largo del tiempo.
- **Definición de listo**: serie de criterios que toda historia de usuario debe cumplir antes de considerarse completada.

## e. Listado de equipos y miembros de los equipos. 

Diosvier:

-Adrián Sibaja  B87561

-Erik Kühlmann  B84175

-Esteban Marín  B84594

-Daniel Salazar B87214

-Ricardo Franco B83050
    
    
### a. Asignación de roles para cada una de las iteraciones.

**Sprint 0**

Diosvier:
    
-Adrián Sibaja B87561      Scrum Master

-Erik Kühlmann B84175      Scrum Ambassador

-Esteban Marín B84594      Developer

-Daniel Salazar B87214     Developer

-Ricardo Franco B83050     Developer

    
## f. Descripción general del sistema a desarrollar.

### a. Contexto y situación actual.

### b. Problema que resuelve.

El servicio de Terapia Respiratoria del CEACO por medio del equipo de Primera Respuesta Médica Especializada (PRIME), se encarga de la movilización de todos los pacientes con COVID-19 del país, pero de forma más importante de pacientes críticos, que necesitan de una movilización de centros de bajo nivel de complejidad a hospitales de mayor complejidad.

En la actualidad el equipo PRIME, en conjunto con un equipo de la UCR, ya se encuentra trabajando en las funcionalidades iniciales de una aplicación móvil a partir de las prioridades de los usuarios especialistas respiratorios.

Dada que la emergencia sanitaria se encuentra en uno de sus picos de contagio, y se esperan nuevas olas de transmisión, el equipo PRIME requiere además una primera versión de una aplicación web para administrar sus procesos de atención, y que complemente la funcionalidad que ofrecerá la aplicación móvil. El presente producto corresponde a la implementación de la aplicación web.

### f. Visión del producto.

Puede encontrar nuestra visión del producto en el link: https://docs.google.com/spreadsheets/d/1XnhCmkLnF6gNzaVUzOPjGgIScgK2AZjcDtBU81P5B8c/edit#gid=1003342746

### g. Relación con otros sistemas externos.

### h. Descripción de los temas (módulos) asignados a cada equipo. Descripción de los principales epics asociados al tema(s) a desarrollar por cada uno de los equipos. Relación con los demás módulos (temas) del sistema.

**Diosvier - Administración de incidentes**

Para la administración de los traslados (e incidentes) se requiere la implementación de una plataforma que permita el despacho, seguimiento y monitoreo en tiempo real por medio de GPS y mapas tanto para las unidades terrestres, marítimas y aéreas. Los epics asociados a este tema son:

1. **Crear incidente con datos básicos:** para cada incidente, se debe digitar el origen, destino, nombre del paciente, síntomas y demás datos útiles para su atención.
2. **Asociar expediente con incidente nuevo:** para cada incidente, se consulta en la base de datos si fue creado un expediente anteriormente o si debe generarse uno nuevo.
3. **Asignación de incidente aprobado:** una vez se ha revisado que el incidente se ingresó correctamente, se aprueba y se procede a asignar una unidad de transporte y un equipo para su atención.
    
Con el módulo Dashboard, se debe coordinar el despliegue consistente y limpio de la información en la interfaz de la aplicación. Con el módulo Expedientes Médicos, se encuentran las consultas a los distintos expedientes generados con anterioridad y las solicitudes para crear nuevos documentos. Por último, con el módulo Listas de chequeo, se requiere coordinar el despliegue de esta información en la aplicación para su uso durante el traslado.

### i. Requerimientos funcionales (Backlog del producto, solo indicando la referencia al proyecto en Jira).
    
### j. Mapa de ruta del producto (Product Road Map).

Puede encontrar nuestro Product Roadmap en el link: https://docs.google.com/spreadsheets/d/1XnhCmkLnF6gNzaVUzOPjGgIScgK2AZjcDtBU81P5B8c/edit#gid=1653817941

### k. Requerimientos no funcionales que debe cumplir toda la aplicación web.

**Diosvier/Administración de Traslados - Usabilidad**

- La aplicación debe proveer una interfaz sencilla e intuitiva de utilizar para registrar un incidente.
- Si el usuario registrara algún incidente incorrectamente, se le debe notificar con un mensaje claro y conciso su error.
- La aplicación debe seguir todos los lineamientos del Manual de Identidad Visual de la Universidad de Costa Rica.


## h. Decisiones técnicas.

### a. Metodologías utilizadas y procesos definidos.
1. Git: mecanismo para control de versiones. Se optó por una rama por equipo y una rama por cada desarrollador. Además, se acordaron reglas para subir código a la rama *master*, las cuales están especificadas en la definición de listo.
2. Scrum: metodología ágil para el desarrollo de software. Se trabajó con un *scrum of scrums*, con cada equipo auto-organizado trabajando en un módulo específico de la aplicación. En los links adjuntos, se pueden consultar los distintos procesos que engloba esta metodología.
    