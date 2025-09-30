# API Documentation

## 📋 Información General

- **Base URL**: `https://localhost:7030/api` o `https://localhost:44308/api`
- **Formato**: JSON
- **Autenticación**: No requerida

## 🏠 Endpoints

### Propiedades

```http
GET    /api/properties              # Lista propiedades con filtros
GET    /api/properties/{id}         # Obtiene propiedad por ID
POST   /api/properties              # Crea nueva propiedad
PUT    /api/properties/{id}         # Actualiza propiedad
DELETE /api/properties/{id}         # Elimina propiedad
```

### Health Check

```http
GET    /api/health                  # Estado del sistema
GET    /api/health/ready            # Sistema listo
```

## 🔍 Filtros

**Query Parameters** para `GET /api/properties`:

- `name` - Filtrar por nombre
- `address` - Filtrar por dirección  
- `minPrice` - Precio mínimo
- `maxPrice` - Precio máximo
- `page` - Número de página (default: 1)
- `pageSize` - Tamaño de página (default: 10)

**Ejemplo**:
```http
GET /api/properties?name=Casa&minPrice=100000&maxPrice=500000
```

## 📝 Modelos

### PropertyDto
```json
{
  "id": "string",
  "idOwner": "string", 
  "name": "string",
  "address": "string",
  "price": "decimal",
  "codeInternal": "string",
  "year": "integer",
  "image": "string (nullable)",
  "owner": "OwnerDto (nullable)"
}
```

### OwnerDto
```json
{
  "id": "string",
  "name": "string",
  "address": "string", 
  "photo": "string (nullable)",
  "birthday": "datetime (nullable)"
}
```

### ApiResponse<T>
```json
{
  "success": "boolean",
  "message": "string",
  "data": "T"
}
```

## 🚨 Respuestas

**Éxito**:
```json
{
  "success": true,
  "message": "Propiedades obtenidas exitosamente",
  "data": [...]
}
```

**Error**:
```json
{
  "success": false,
  "message": "Error al obtener propiedades",
  "data": null
}
```

## 🧪 Testing

- **Swagger UI**: https://localhost:7030/swagger o https://localhost:44308/swagger/index.html
- **Postman**: Usar archivo `api/RealEstateAPI.API.http`
- **cURL**: Ejemplos incluidos arriba

### Tests Unitarios
```bash
cd api/RealEstateAPI.Tests
dotnet test
```

### Coverage Report
```bash
cd api/RealEstateAPI.Tests
dotnet test --collect:"XPlat Code Coverage"
```