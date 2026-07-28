# Formularios compatibles con el diseñador de Windows Forms

Se reorganizaron los formularios `FrmGrupos`, `FrmInsignias` y `FrmTimeline` para separar la interfaz visual de la lógica.

Cada formulario ahora contiene:

- Archivo principal `.cs` con la lógica y los eventos.
- Archivo `.Designer.cs` con los controles y sus propiedades visuales.
- Archivo `.resx` para recursos del formulario.

Esto permite abrir los tres formularios desde **Ver diseñador** en Visual Studio y modificar visualmente sus controles sin afectar los servicios ni los cálculos existentes.
