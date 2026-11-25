# Watchlist Netflix

Aplicación para crear listas de seguimiento de películas y series. Prueba técnica para Insfera.

## Stack

- **Base de datos:** MySQL 8.4 (en Docker)
- **API:** .NET 8 (C#) con Entity Framework Core
- **Front:** Vue 3 (Composition API) + Vue Router
- **Estilos:** Bootstrap 5

## Funcionalidades

1. Login de usuario
2. Cada usuario crea y consulta sus propias listas
3. Buscador con filtros: nombre, tipo, género y año
4. Ficha de cada película o serie
5. Añadir un título a una lista
6. Quitar un título de una lista
7. Eliminar listas de seguimiento
8. Cambiar entre modo claro y oscuro

## Cómo ejecutarlo

Requisitos: Docker, .NET 8 SDK y Node.js.

**1. Base de datos** (desde la raíz del proyecto)

```bash
docker compose up -d
```

En el primer arranque, Docker crea el esquema y carga los datos de ejemplo y sus pósters automáticamente. Los scripts solo se ejecutan cuando MySQL inicializa una base de datos vacía.

Si ya tenías una base de datos inicializada antes de este cambio y quieres aplicar los pósters, ejecuta una vez:

```bash
docker exec -i watchlist-db mysql -uroot -proot --default-character-set=utf8mb4 watchlist < database/posters.sql
```

`database/posters.sql` contiene las URLs precargadas. Para regenerarlo con `scripts/posters.mjs` hace falta una clave propia de TMDB: `TMDB_TOKEN='...' node scripts/posters.mjs`. La clave no se necesita para ejecutar la aplicación.

**2. API**

```bash
cd backend
dotnet run
```

Escucha en `http://localhost:5151`. Swagger está en `http://localhost:5151/swagger`.

**3. Web**

```bash
cd frontend
npm install
npm run dev
```

Abrir `http://localhost:5173`.

**4. Crear una cuenta**

En la web, abre **Regístrate** desde la pantalla de inicio de sesión. Al completar el formulario, la sesión se inicia automáticamente. También puedes crear una cuenta desde Swagger con `POST /api/Auth/register`:

```json
{ "username": "gael", "password": "1234" }
```

## Modelo de datos

Siete tablas normalizadas (hasta 3FN): `usuarios`, `listas`, `titulos`, `tipos`, `generos` y dos tablas intermedias para las relaciones muchos a muchos:

- `titulo_genero`: un título tiene varios géneros y un género pertenece a varios títulos.
- `lista_titulo`: una lista tiene varios títulos y un título puede estar en varias listas. Su clave primaria compuesta impide duplicar un título dentro de la misma lista.

Los filtros del buscador salen de las columnas de `titulos` (nombre, tipo, año) y de la relación con `generos`.

## Decisiones técnicas

- **Entity Framework Core (Database First):** el esquema se diseñó primero en SQL y las clases se generaron con `dotnet ef dbcontext scaffold`.
- **Contraseñas:** se guardan cifradas con BCrypt, nunca en texto plano.
- **Sesión sencilla:** el enunciado no pide seguridad avanzada, así que el login solo comprueba usuario y contraseña. La sesión se guarda en `localStorage`.
- **CORS:** la API solo acepta peticiones desde `http://localhost:5173`.
- **Imágenes:** `imagen_url` guarda enlaces a pósters de TMDB para los títulos de ejemplo. La aplicación no necesita una clave de TMDB para mostrarlos.
- **Filtros en el servidor:** la API construye una única consulta SQL con solo los filtros que llegan.

## Créditos

Los pósters provienen de TMDB. Este producto usa la API de TMDB pero no está avalado ni certificado por TMDB.