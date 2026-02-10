## Requisitos

- Node.js 18+
- Backend .NET 8 corriendo en `http://localhost:5000`
- PostgreSQL con la base de datos `RealPlazaDB` creada y script `Script.sql` ejecutado

## Instalación y ejecución

```bash
cd "Caso 3/RealPlaza.FrontEnd"
npm install
npm run dev
```

## Usuario de prueba

Crear un usuario directamente en PostgreSQL o desde la app (el primer acceso requiere un registro previo en BD):

```sql
INSERT INTO users(username, passwordhash, email, birthdate, isactive)
VALUES ('admin', '123456', 'admin@realplaza.com', '2026-02-10', true);
```

Luego ingresar con: **admin / 123456**

## Nota

- El backend debe tener CORS habilitado para `http://localhost:3000` (ya configurado en `Program.cs`).
- Si el backend corre en otro puerto, ajustar `baseURL` en `src/api/axiosConfig.js`.
