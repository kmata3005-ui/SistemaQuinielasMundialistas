# Banderas reales

La versión de entrega reemplaza los códigos de país por imágenes PNG almacenadas localmente en `Resources/Banderas`.

Las banderas se muestran en:

- Tabla de posiciones por grupos.
- Ranking público.
- Ranking privado.

`BanderaHelper` resuelve el archivo correspondiente y mantiene las imágenes en memoria para evitar bloqueos de archivos y lecturas repetidas.
