# PersonAPI .NET

Una aplicación ASP.NET MVC + Web API para gestionar información de personas, teléfonos, estudios y profesiones, conectada a SQL Server y desplegada en Docker.
Desarrollado por Juan Diego Perez, Juan Miguel Zuluaga y Samuel Ramirez Alvarez

## 📦 Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://www.docker.com/)
- Visual Studio 2022+ o VS Code
- Git

## ⚙️ Configuración del ambiente local

1. **Clona el repositorio:**

```bash
git clone https://github.com/rasamuel/personapi-dotnet.git
//Para acceder a la carpeta del proyecto
cd personapi_dotne
//Para acceder a la carpeta del codigo
cd personapi_dotne
```

2. **Compila el proyecto:**

```bash
dotnet build
```

3. **Inicia los contenedores con Docker Compose:**

```bash
docker compose up -d --build
```

Esto iniciará:
- SQL Server en `localhost:1433`
- La API en `http://localhost:9090`

4. **Aplica migraciones (si las usas):**

```bash
dotnet ef database update
```

5. **Abre en el navegador:**

- Interfaz web MVC: `http://localhost:9090`
- Endpoints API REST: `http://localhost:9090/api/telefono`, `api/estudio`, etc.

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
