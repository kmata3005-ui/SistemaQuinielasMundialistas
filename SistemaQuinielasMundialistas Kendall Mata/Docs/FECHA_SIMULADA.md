# Fecha simulada y estados automáticos

## Funcionamiento

El módulo de Partidos incluye un reloj simulado persistente. La fecha se almacena en `Data/configuracion.json` al ejecutar el programa.

Estados calculados automáticamente:

- **Próximo:** la fecha simulada es anterior a la fecha de inicio.
- **En curso:** la fecha simulada está entre la hora de inicio y las dos horas siguientes.
- **Finalizado:** han transcurrido dos horas o más desde el inicio.

## Controles

- **Aplicar:** establece la fecha seleccionada.
- **+1 hora:** avanza el reloj una hora.
- **+1 día:** avanza el reloj un día.
- **Fecha real:** restablece el reloj a la fecha actual del equipo.

Cuando un partido pasa a `En curso` deja de aceptar pronósticos. Al pasar a `Finalizado`, se recalculan los puntos de los pronósticos y el ranking de usuarios.
