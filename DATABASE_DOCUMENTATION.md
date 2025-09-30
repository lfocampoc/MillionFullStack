# Database Documentation

## 🗄️ Información General

- **Tipo**: MongoDB
- **Puerto**: 27017
- **Base de Datos**: RealEstateDB

## 📊 Colecciones

### Properties
```javascript
{
  "_id": ObjectId,
  "Name": "string",
  "Address": "string", 
  "Price": "decimal",
  "CodeInternal": "string",
  "Year": "integer",
  "IdOwner": ObjectId,
  "Images": [...],
  "Traces": [...]
}
```

### Owners
```javascript
{
  "_id": ObjectId,
  "Name": "string",
  "Address": "string",
  "Photo": "string (nullable)",
  "Birthday": "datetime (nullable)"
}
```

## 🔍 Índices

```javascript
// Búsquedas por nombre y dirección
db.properties.createIndex({ "Name": "text" })
db.properties.createIndex({ "Address": "text" })

// Filtros de precio
db.properties.createIndex({ "Price": 1 })

// Índice compuesto
db.properties.createIndex({ "Name": 1, "Price": 1 })
```

## 📝 Datos de Ejemplo

Se cargan automáticamente al iniciar:
- 6 propiedades
- 3 propietarios
- Imágenes de ejemplo

## 🔧 Configuración

**appsettings.json**:
```json
{
  "ConnectionStrings": {
    "MongoDB": "mongodb://localhost:27017"
  },
  "DatabaseName": "RealEstateDB"
}
```

## 🧪 Consultas Útiles

```javascript
// Buscar por nombre
db.properties.find({ "Name": { $regex: "Casa", $options: "i" } })

// Filtrar por precio
db.properties.find({ "Price": { $gte: 300000, $lte: 600000 } })

// Con información del propietario
db.properties.aggregate([
  { $lookup: { from: "owners", localField: "IdOwner", foreignField: "_id", as: "Owner" } },
  { $unwind: "$Owner" }
])
```