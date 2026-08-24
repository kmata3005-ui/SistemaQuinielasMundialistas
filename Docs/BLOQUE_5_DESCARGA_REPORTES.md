# Bloque 5 - Descarga de reportes

## Requisito cubierto
La Iteración 2 solicita que los reportes puedan descargarse como CSV o TXT.

Este bloque implementa ambas alternativas para:
- Administrador.
- Usuario.

## Formatos
### CSV
Archivo separado por punto y coma para facilitar su apertura en Excel con configuraciones regionales en español.

### TXT
Resumen legible del reporte mostrado en pantalla.

### PDF - bonificación
Se agregó descarga PDF para intentar obtener la bonificación indicada por la especificación.

El PDF se genera con una clase escrita dentro del proyecto (`ReporteExportService.PdfSimple`)
sin paquetes externos de generación PDF. El generador crea la estructura básica del estándar PDF:
catálogo, páginas, fuente, streams de contenido, tabla xref y trailer.

Esto permite explicar que la generación del PDF fue desarrollada dentro del proyecto.

## Descarga en Blazor
El servicio genera el contenido en memoria como `byte[]`.
Blazor convierte los bytes a Base64 y una función JavaScript propia crea la descarga en el navegador.

## Archivos principales
- `Services/ReporteExportService.cs`
- `wwwroot/js/download.js`
- `Components/Pages/Admin/ReportesAdmin.razor`
- `Components/Pages/ReportesUsuario.razor`
