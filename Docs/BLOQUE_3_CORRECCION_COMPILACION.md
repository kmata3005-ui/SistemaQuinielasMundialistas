# Corrección de compilación del Bloque 3

Se corrigió `PartidosAdmin.razor`.

## Problema
Los controles HTML `datetime-local` estaban enlazados a propiedades `string`.
En la versión actual de Blazor el binding de este control trabaja de forma tipada con `DateTime`,
lo que provocaba errores CS1503 y CS0029 durante la generación del componente Razor.

## Corrección
- `FechaHoraTexto` se sustituyó por `DateTime FechaHora`.
- `FechaSimuladaTexto` se sustituyó por `DateTime FechaSimulada`.
- Se usa `@bind:format="yyyy-MM-ddTHH:mm"` en ambos controles.
- Se eliminó la conversión manual con `DateTime.TryParse`.

La lógica funcional del Bloque 3 no cambia.
