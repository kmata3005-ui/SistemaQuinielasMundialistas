#  Sistema de Quinielas Mundialistas

##  Descripción

Sistema desarrollado en **C# Windows Forms** que permite administrar una quiniela mundialista, donde los usuarios pueden registrar pronósticos, consultar rankings, administrar quinielas, visualizar estadísticas y seguir el desarrollo del torneo mediante una fecha simulada.

El proyecto fue desarrollado aplicando principios de **Programación Orientada a Objetos**, arquitectura **MVC**, principios **SOLID**, **Clean Code** y el patrón de diseño **Repository**, utilizando archivos **JSON** para la persistencia de los datos.

---

#  Tecnologías utilizadas

- C#
- .NET Windows Forms
- Visual Studio 2022
- JSON
- MVC (Modelo - Vista - Controlador)
- Programación Orientada a Objetos
- SOLID
- Repository Pattern
- Git
- GitHub

---

#  Estructura del proyecto

```
SistemaQuinielasMundialistas
│
├── Controllers
├── Models
├── Views
├── Services
├── Repositories
├── Data
├── Resources
├── Utils
└── Docs
```

---

#  Funcionalidades

- Registro y administración de usuarios.
- Gestión de partidos.
- Gestión de quinielas públicas y privadas.
- Registro de pronósticos.
- Bloqueo automático de pronósticos cuando el partido está en curso.
- Fecha simulada.
- Ranking público.
- Ranking privado.
- Sistema de insignias.
- Timeline de notificaciones.
- Tabla de posiciones por grupos.
- Generación automática de cruces eliminatorios.
- Resolución por penales.
- Estadísticas por rango de fechas.
- Persistencia mediante archivos JSON.

---

# ▶️ Cómo ejecutar el proyecto

1. Abrir la solución **SistemaQuinielasMundialistas.sln** en Visual Studio 2022.
2. Restaurar los paquetes de NuGet si es necesario.
3. Compilar la solución.
4. Ejecutar el proyecto utilizando el botón **Iniciar** o presionando **F5**.

---

#  Persistencia de datos

La información del sistema se almacena mediante archivos JSON ubicados dentro de la carpeta **Data**.

Entre ellos se encuentran:

- Usuarios
- Partidos
- Pronósticos
- Quinielas
- Timeline

Los datos se cargan automáticamente al iniciar la aplicación.

---

#  Arquitectura

El sistema fue desarrollado utilizando la arquitectura **Modelo-Vista-Controlador (MVC)**.

- **Models:** representan las entidades del sistema.
- **Views:** contienen la interfaz gráfica desarrollada con Windows Forms.
- **Controllers / Services:** implementan la lógica del negocio.
- **Repositories:** administran la persistencia de la información utilizando archivos JSON.

---

#  Funcionalidades implementadas

- Gestión de usuarios
- Gestión de quinielas
- Gestión de partidos
- Pronósticos
- Ranking global
- Ranking privado
- Timeline
- Insignias
- Tabla de grupos
- Cruces eliminatorios
- Penales
- Estadísticas

---

#  Autor

Kendall Mata Sánchez.

Universidad Politécnica Internacional.

Curso:	Tecnicas de programacion.

Profesor: Luis Felipe Mora Umaña.

2026.