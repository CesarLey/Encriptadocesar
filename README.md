# CaesarApi

API RESTful en .NET 8 para cifrado y descifrado de mensajes usando el algoritmo de César, con autenticación JWT y almacenamiento en PostgreSQL (Supabase).

## 🚀 Deploy en Render
1. Haz fork o sube este repo a tu GitHub.
2. En [Render.com](https://render.com), crea un nuevo servicio Web.
3. Conecta tu repo y selecciona Docker como entorno.
4. Configura la variable de entorno `PORT` (Render la define automáticamente).
5. Configura las variables de entorno para la cadena de conexión PostgreSQL (ver abajo).
6. Haz deploy.

## 🛢 Configuración de Supabase
- Crea un proyecto en [Supabase](https://supabase.com/).
- Crea una base de datos PostgreSQL y copia la cadena de conexión.
- Reemplaza los valores en `appsettings.json` o usa variables de entorno:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=<HOST>;Database=<DB>;Username=<USER>;Password=<PASSWORD>;Port=5432;Ssl Mode=Require;Trust Server Certificate=true"
}
```

## 🔐 Autenticación JWT
- Endpoint de login: `POST /auth/login` con body:
```json
{
  "username": "admin",
  "password": "1234"
}
```
- Obtén el token y úsalo en el header `Authorization: Bearer {token}` para los endpoints protegidos.

## 📦 Endpoints
- `POST /mensajes/encriptar` — Encripta y guarda mensaje.
- `POST /mensajes/desencriptar` — Desencripta mensaje.
- `GET /mensajes/mensajes` — Lista todos los mensajes (requiere JWT).

## 🐳 Docker
- Build local:
```sh
docker build -t caesarapi .
docker run -e PORT=8080 -p 8080:8080 caesarapi
```

## 📝 Notas
- Swagger disponible en `/swagger` en desarrollo.
- CORS abierto en desarrollo.
- No olvides crear las migraciones y la tabla `Mensajes` en tu base de datos.

---

¡Listo para cifrar y descifrar mensajes de forma segura! 🔒 