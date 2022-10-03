### TP FINAL Programacion Avanzada II - Grupo 4

**Alumnos:**
- Gutierrez, Marcelo
- Figueras, Gonzalo
- Regueira, Alberto
- Galarza, Agustin
- Quintela, Victor

### TodoApp

**Explicacion:**

Todo App es una aplicacion para el manejo de las tareas del usuario. 
Estas se agruparan por materias y podran ser eliminadas, actulizadas y cambiar su estado.

**Hisrorias de usuario**

Yo Como Cliente Quiero una lista de tareas para organizar mi tiempo
Yo como usuario Quiero poder cargar una tarea
Yo como usuario quiero poder editar una tarea
Yo como usuario quiero poder eliminar una tarea
Yo como usuario quiero poder diferenciar mis tareas por materia

**Diagrama de Clases:**

![Alt text](Diagrama.jpeg?raw=true "Diagrama de clases")

**Postman Collection:**

Se adjunta en el repositorio la coleccion de postman

**Consideraciones para iniciar el proyecto:**

Para generar la base de datos, es necesario correr las migraciones generadas

Para ello se debe correr los siguientes comandos en la Consola de Administracion de Paquetes NuGet

PM> add-migration TodoApp

PM> Update-Database
