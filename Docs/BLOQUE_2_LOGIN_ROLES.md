# Bloque 2 - Login, roles y administración de usuarios

Se agregó autenticación contra la base de datos, sesión de usuario, separación de roles Usuario/Administrador y mantenimiento administrativo de usuarios.

## Credenciales de demostración
- Administrador: `admin` / `Admin2026!`
- Usuario: `usuario1` / `Mundial2026`

## Funcionalidades
- Inicio y cierre de sesión.
- Identificación del rol almacenado en la tabla Usuarios.
- Dashboard diferente para Usuario y Administrador.
- El administrador no dispone de opciones de pronóstico.
- Desactivar/reactivar usuarios.
- Reset de contraseña.
- Contraseñas migradas a PBKDF2 con salt.
