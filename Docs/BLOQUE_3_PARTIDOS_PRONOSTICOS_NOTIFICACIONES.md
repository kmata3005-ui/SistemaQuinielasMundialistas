# Bloque 3 - Partidos, pronósticos y notificaciones

## Objetivo
Implementar las funcionalidades de Iteración 2 relacionadas con la administración de partidos,
pronósticos del usuario y notificaciones de partidos sin pronosticar durante las próximas 24 horas.

## Funcionalidades implementadas

### Administrador
- Puede cambiar la fecha y hora de un partido.
- Puede modificar el estado: Próximo, En curso o Finalizado.
- Puede registrar el marcador final y anotadores.
- Puede modificar la fecha simulada del sistema.
- Al finalizar un partido se recalculan los puntos de sus pronósticos.
- El administrador no puede pronosticar.

### Usuario
- Puede consultar partidos que todavía no han iniciado.
- Puede registrar un único pronóstico por partido.
- No puede registrar pronósticos si el partido inició, está en curso o finalizó.
- Puede consultar el historial de sus pronósticos y puntos obtenidos.

### Notificaciones
- Al entrar al dashboard se consultan partidos de las próximas 24 horas.
- Se excluyen partidos para los cuales el usuario ya realizó un pronóstico.
- Se excluyen partidos iniciados, en curso o finalizados.
- Existe una página dedicada de notificaciones.

## Regla de puntos
- Marcador exacto: 5 puntos.
- Ganador o empate correcto: 2 puntos.
- Pronóstico incorrecto: 0 puntos.

## Fecha simulada
La lógica utiliza ConfiguracionSistema.FechaSimulada para poder probar los estados del sistema
sin depender de la fecha real del equipo.
