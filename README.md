# PersonAPI .NET

Una aplicación ASP.NET MVC + Web API para gestionar información de personas, teléfonos, estudios y profesiones, conectada a SQL Server y desplegada en Docker.
Desarrollado por Juan Diego Perez, Juan Miguel Zuluaga y Samuel Ramirez Alvarez

## 📦 Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://www.docker.com/)
- Visual Studio 2022+ o VS Code
- Git

## ⚙️ Configuración del ambiente local

### ✅ Opción 1: Clonar desde GitHub

```bash
git clone https://github.com/rasamuel/personapi-dotnet.git
# Para acceder a la carpeta del proyecto
cd personapi-dotnet
# Para acceder a la carpeta del código
cd personapi_dotnet
```

### ✅ Opción 2: Descargar como `.zip` desde GitHub

1. Ve al repositorio: [https://github.com/rasamuel/personapi-dotnet](https://github.com/rasamuel/personapi-dotnet)
2. Haz clic en la pestaña **Releases** o selecciona un **tag estable**.
3. Descarga el archivo `.zip`, extráelo y entra a la carpeta del proyecto.

```bash
# Para acceder a la carpeta del proyecto
cd personapi-dotnet-entrega
# Para acceder a la carpeta del código
cd personapi_dotnet
```

1. **Inicia los contenedores con Docker Compose:**

```bash
docker compose up -d --build
```

Esto iniciará:
- SQL Server en `localhost:1433`
- La API en `http://localhost:9090`

2. **Abre en el navegador:**

- Interfaz web MVC: `http://localhost:9090`
- Endpoints API REST: `http://localhost:9090/api/telefono`, `api/estudio`, etc.

3. **Detener los contenedores:**

Para apagar y limpiar los contenedores:

```bash
docker compose down
```

## 🗂 Estructura del proyecto

```
Controllers/
├── Mvc/
├── Api/
Models/
Repositories/
Views/
appsettings.json
Dockerfile
docker-compose.yml
```

## 🐳 Despliegue con Docker

- Dockerfile compila el proyecto ASP.NET MVC
- Docker Compose crea los servicios: `webapi` y `sqlserver-db`
- Datos persistentes en volumen `mssql_data`

## 🔐 Variables sensibles

- Usuario SQL: `sa`
- Contraseña: definida en `docker-compose.yml`
