# Iteración 2 - Bloque 1: Base de datos relacional

## Cambios realizados

- Se mantiene Blazor Web como nueva capa View.
- Se agregaron modelos relacionales basados en las entidades reales de V1.
- Se agregó `AppDbContext` con Entity Framework Core.
- Se sustituyó `JsonRepository<T>` por `EfRepository<T>` manteniendo Repository Pattern.
- Se agregó SQLite como base de datos relacional.
- Se migraron los datos existentes de V1 a `Data/quinielas_v2.db`.
- Se agregaron relaciones entre usuarios, pronósticos, partidos, quinielas, selecciones, insignias y timeline.
- Se agregó una pantalla `/base-datos` para verificar la conexión y los registros migrados.

## Datos migrados desde V1

- 41 usuarios.
- 41 partidos.
- 210 pronósticos.
- 6 quinielas.
- 25 eventos del timeline.
- Relaciones de participantes por quiniela.
- Insignias existentes.
- Fecha simulada.

## Estructura principal

`Blazor -> Services -> IRepository<T> -> EfRepository<T> -> AppDbContext -> SQLite`

## Importante

Los JSON de la primera iteración no se utilizan en tiempo de ejecución en V2. La fuente de datos de este bloque es la base relacional `quinielas_v2.db`.
