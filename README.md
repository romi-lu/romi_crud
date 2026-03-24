# Romi CRUD

Aplicación full stack: API **ASP.NET Core** (`RomiCrud.Api`), front **Angular** (`romi-crud-web`) y base de datos **MySQL** / **MariaDB**.

## Requisitos en la otra PC

| Herramienta | Notas |
|-------------|--------|
| [.NET SDK](https://dotnet.microsoft.com/download) | Compatible con `net10.0` del proyecto (SDK 10.x). |
| [Node.js](https://nodejs.org/) (LTS) | Para `npm` y Angular CLI. |
| MySQL 8+ o MariaDB 10.4+ | Servidor en ejecución. |

Opcional: [Git](https://git-scm.com/) para clonar el repositorio.

## 1. Obtener el código

```bash
git clone https://github.com/romi-lu/romi_crud.git
cd romi_crud
```

## 2. Base de datos

1. Crea la base (desde el cliente MySQL o herramienta gráfica):

   ```sql
   CREATE DATABASE IF NOT EXISTS crud_romi
     CHARACTER SET utf8mb4
     COLLATE utf8mb4_unicode_ci;
   ```

   También puedes usar el script: `RomiCrud.Api/Scripts/01_create_database.sql`.

2. Anota **servidor**, **puerto**, **usuario** y **contraseña** de MySQL.

## 3. Configurar la API

Edita `RomiCrud.Api/appsettings.json` o `appsettings.Development.json` y ajusta la cadena de conexión:

```json
"ConnectionStrings": {
  "Default": "Server=TU_SERVIDOR;Port=3306;Database=crud_romi;User ID=TU_USUARIO;Password=TU_CLAVE;"
}
```

En desarrollo puedes usar secretos de usuario (no se suben al repo):

```bash
cd RomiCrud.Api
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:Default" "Server=...;Database=crud_romi;..."
```

**JWT:** en `appsettings.json` están `Jwt:Key`, `Issuer` y `Audience`. En producción usa una clave larga y secreta (variables de entorno o secretos), no la de ejemplo.

Al **arrancar la API**, se aplican migraciones y el seed crea catálogos y el usuario **admin** / **admin123** si la base está vacía.

## 4. Restaurar y ejecutar la API

Desde la raíz del repo (o dentro de `RomiCrud.Api`):

```bash
cd RomiCrud.Api
dotnet restore
dotnet run --launch-profile http
```

Por defecto escucha en **http://localhost:5282** (ver `Properties/launchSettings.json`).

Si prefieres aplicar migraciones a mano:

```bash
dotnet ef database update --project RomiCrud.Api
```

(Necesitas el paquete de diseño ya referenciado en el `.csproj`.)

## 5. Front Angular

En otra terminal:

```bash
cd romi-crud-web
npm install
npx ng serve
```

Abre el navegador en **http://localhost:4200** (o la URL que muestre la consola).

El archivo `src/proxy.conf.json` hace que las peticiones a `/api` durante `ng serve` se reenvíen a **http://127.0.0.1:5282**. La API debe estar **encendida** en ese puerto.

`src/environments/environment.ts` tiene `apiUrl` vacío a propósito para usar ese proxy en desarrollo.

## 6. Uso

1. Con la **API** y **`ng serve`** en marcha, entra a `/login`.
2. Usuario: **admin** — Contraseña: **admin123** (creados por el seed la primera vez).
3. Gestiona personas en la pantalla principal.

**Swagger** (solo con la API en modo Development): http://localhost:5282/swagger

## Resumen rápido (dos terminales)

| Terminal | Comando |
|----------|---------|
| 1 | `cd RomiCrud.Api` → `dotnet run --launch-profile http` |
| 2 | `cd romi-crud-web` → `npm install` → `npx ng serve` |

Luego: http://localhost:4200

## Estructura del repositorio

```
romi_crud/
├── RomiCrud.Api/      # API REST, JWT, EF Core, MySQL
├── romi-crud-web/     # SPA Angular
├── RomiCrud.slnx
└── README.md
```

Más detalles del CLI de Angular: [romi-crud-web/README.md](romi-crud-web/README.md).
