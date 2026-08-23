# Bloque 4 - Ranking, insignias, quinielas y reportes por rol

## Base utilizada
Este bloque se construye sobre el Bloque 3 corregido y reutiliza la base de datos relacional migrada desde la V1.

## Funcionalidades

### Ranking
- Ranking global de usuarios activos.
- Orden por puntos.
- Muestra insignias.
- Destaca al usuario autenticado.

### Insignias
- Se leen desde la base de datos relacional.
- Se muestran en el dashboard del usuario.
- Se muestran en el ranking.
- Se conservan las insignias migradas desde la Iteración 1.

### Quinielas
- Se muestran quinielas públicas.
- Para quinielas privadas, el usuario solo ve aquellas a las que pertenece.
- Cada quiniela incluye integrantes y posiciones por puntos.
- Se muestran insignias de los participantes.

### Reportes de Administrador
- Resultado más repetido.
- Partido con más aciertos.
- Usuarios con más aciertos: Top 1, Top 3 y Top 5.
- Partido con más pronósticos.
- Promedio de goles.
- Partido sin aciertos.

### Reportes de Usuario
- Equipo más apostado.
- Equipo sorpresa con estadística detallada.
- Probabilidad de acierto de pronósticos anteriores.

## Lógica reutilizada de la V1
Para `Equipo más apostado` y `Equipo sorpresa` se adaptó la lógica del
`EstadisticaService` de la Iteración 1 a Entity Framework Core.

En la V1, un equipo se considera sorpresa cuando gana un partido y menos de la mitad
de los pronósticos lo daban como ganador. Esa misma regla se mantiene en V2.

## Probabilidad de acierto
La especificación solicita un reporte de probabilidad de acierto de pronósticos anteriores,
pero no define una fórmula concreta. En V2 se expresa como porcentaje histórico:

Aciertos / Pronósticos de partidos finalizados * 100

Un acierto corresponde a un pronóstico que obtuvo 2 o 5 puntos.
También se muestra por separado la cantidad de marcadores exactos (5 puntos).

## Bloque 5
La descarga de reportes en CSV/TXT y la bonificación PDF se implementarán en el siguiente bloque.
