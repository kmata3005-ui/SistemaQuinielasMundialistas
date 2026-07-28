# Mejoras incorporadas

- Patrón Repository genérico para persistencia JSON.
- Ruta centralizada de archivos en `Data`.
- IDs generados con `Max + 1`.
- Validaciones de usuarios, partidos, quinielas y pronósticos duplicados.
- Normalización de estados de partidos.
- Bloqueo de pronósticos para partidos iniciados/finalizados.
- Eliminación del método duplicado de recálculo de puntos.
- Insignias básicas de líder global y último lugar.
- Estadísticas avanzadas.
- Seed automático con 40 usuarios, 12 partidos, 210 pronósticos y 5 quinielas privadas.

## Pendiente de probar en Visual Studio

Este paquete fue modificado sin ejecutar Windows Forms en este entorno. Abrirlo en Visual Studio, compilar y compartir cualquier error exacto para corregirlo.

## Módulo de insignias
- Se agregó una jerarquía de clases basada en `Insignia`.
- Se implementaron insignias de líder global, rey de los empates, racha de 10 aciertos y último del ranking.
- `InsigniaService` evalúa todas las reglas mediante polimorfismo.
- Se agregó `FrmInsignias` y el botón correspondiente en el menú principal.
- Las insignias obtenidas se guardan dentro de cada usuario en JSON.

## Módulo Timeline de Quinielas
- Se agregó el modelo `TimelineEvento`.
- Se agregó persistencia en `timeline.json` mediante Repository.
- Se agregó `TimelineService` para consultar y registrar eventos.
- Se agregó la pantalla `FrmTimeline` con filtro por quiniela.
- Se incluyeron eventos de quiniela, nuevo líder y mensajes de la vergüenza.
- Se agregó el botón Timeline al menú principal.
