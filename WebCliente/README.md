# 🧑‍💻 Prueba Técnica - Confiamed Backend

Este proyecto implementa una solución basada en **microservicios** con un **cliente web** en ASP.NET Core Razor Pages.  

Incluye tres proyectos principales:

1. **Usuario_service** → Servicio que expone los usuarios disponibles.  
2. **Item_service** → Servicio que gestiona ítems y los asigna a usuarios.  
3. **ROBERTO_TOPANTA (WebCliente)** → Aplicación web que consume ambos servicios.

---

## 📌 Arquitectura

`[Usuario_service] ---> [Item_service] ---> [ROBERTO_TOPANTA WebCliente]
(API REST) (API REST) (Razor Pages)`


- `Usuario_service`: expone `api/usuarios` con lista de usuarios.  
- `Item_service`: consume `Usuario_service` y asigna ítems con reglas de negocio.  
- `ROBERTO_TOPANTA`: interfaz web que permite ver usuarios, crear ítems, ver ítems pendientes y marcarlos como completados.

---

## ⚙️ Requisitos

- .NET 8 SDK  
- Visual Studio 2022 (o VS Code con extensiones C# y Razor)  
- Postman (para pruebas de API)  

---

## ▶️ Ejecución del proyecto

El proyecto ya está configurado para levantar **los 3 microservicios al mismo tiempo**.  

1. Abre la solución:
   `WebCliente.sln`
2. Selecciona el perfil de ejecución **"Nuevo perfil"** en Visual Studio (esto inicia los 3 proyectos a la vez).  
3. Ejecutamos (Ctrl + F5 o F5).  

Se levantarán las siguientes URLs:

- **Usuario_service** → https://localhost:7057/swagger/index.html 
- **Item_service** → https://localhost:7058/swagger/index.html  
- **ROBERTO_TOPANTA (cliente web)** → http://localhost:5216  

---

## 📌 Endpoints disponibles

### 🔹 Usuario_service
- `GET /api/usuarios` → devuelve lista de usuarios.
- `GET /api/usuarios/{nombre}` → devuelve un usuario específico.
- `GET /api/usuarios/asignar` → rota usuarios disponibles.
---
### Ejemplo de respuesta:

```json
[
{ "id": 1, "nombreUsuario": "Juan Diaz", "totalItemsAsignados": 3, "itemsAltaPrioridad": 1 },
{ "id": 2, "nombreUsuario": "Maria Toques", "totalItemsAsignados": 5, "itemsAltaPrioridad": 2 },
{ "id": 3, "nombreUsuario": "Pedro Estevez", "totalItemsAsignados": 2, "itemsAltaPrioridad": 0 }
]
```

### 📌 Endpoints disponibles
🔹 Usuario_service
- GET /api/usuarios → devuelve lista de usuarios.
- GET /api/usuarios/{nombre} → devuelve un usuario específico.
- GET /api/usuarios/asignar → rota usuarios disponibles.


### Ejemplo de respuesta:
🔹 Item_service
---
- `POST` /api/items/asignar → asigna un ítem automáticamente a un usuario.

- `GET` /api/items/pendientes → devuelve ítems no completados.

- `POST` /api/items/completar/{id} → marca un ítem como completado.

### Ejemplo de request:
```json
{
  "id": 1,
  "titulo": "Reporte Urgente",
  "usuarioAsignado": "",
  "fechaEntrega": "2025-09-20T00:00:00",
  "completado": false,
  "relevancia": 1
}
```
### Ejemplo de respuesta:
```
{
  "usuario": "Juan Diaz",
  "item": "Reporte Urgente"
}
```

🔹 ROBERTO_TOPANTA (Cliente Web)

- `Usuarios` → muestra lista de usuarios desde Usuario_service.

- `Items` → muestra ítems pendientes desde Item_service, permite crear ítems y marcarlos como completados.

### 📜 Reglas de negocio

- Si el ítem tiene fecha de entrega en menos de 3 días, se asigna al usuario con menos carga (ItemsAsignados).
- Si el ítem tiene relevancia Alta, se asigna a un usuario con menos de 3 ítems de alta prioridad.

### Si no se cumple lo anterior, se asigna al usuario con menos carga general.

🧪 Pruebas con Postman

Crear ítem:

- POST https://localhost:7058/api/items/asignar


Body:
```json
{
  "id": 2,
  "titulo": "Tarea crítica",
  "usuarioAsignado": "",
  "fechaEntrega": "2025-09-18T00:00:00",
  "completado": false,
  "relevancia": 1
}
```
### Consultar pendientes:
- GET https://localhost:7058/api/items/pendientes

### Completar un ítem:
- POST https://localhost:7058/api/items/completar/1



### 👤 Autor
### Roberto Toapanta
### Ingeniero en Tecnologías de la Información
Quito - Ecuador