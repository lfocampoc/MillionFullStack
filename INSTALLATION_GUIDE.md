# Guía de Instalación

## 📋 Prerrequisitos
- .NET 9 SDK
- Node.js 18+
- MongoDB

## 🚀 Instalación

### 1. MongoDB
```bash
# Local
mongod
```

### 2. Backend
```bash
cd api
dotnet restore
copy RealEstateAPI.API\appsettings.Example.json RealEstateAPI.API\appsettings.json
dotnet run --project RealEstateAPI.API
```

### 3. Frontend
```bash
cd front
npm install
copy env.example .env.local
# Editar .env.local con: NEXT_PUBLIC_API_URL=https://localhost:7030/api
npm run dev
```

## 🔧 Configuración

**Backend** (`api/RealEstateAPI.API/appsettings.json`):
```json
{
  "ConnectionStrings": {
    "MongoDB": "mongodb://localhost:27017"
  }
}
```

**Frontend** (`front/.env.local`):
```env
NEXT_PUBLIC_API_URL=https://localhost:7030/api
```

## 🧪 Verificar
- API: https://localhost:7030/swagger/index.html
- Frontend: http://localhost:3000
- Tests: `cd api/RealEstateAPI.Tests && dotnet test`

## 💾 Database
- **SeedData**: Se carga automáticamente al iniciar la API
- **Backup**: `mongodump --db RealEstateDB --out ./backup`

## 🔧 Problemas Comunes
- **MongoDB no conecta**: Verificar puerto 27017
- **CORS error**: Verificar URL en `.env.local`
- **Puerto en uso**: `taskkill /F /IM iisexpress.exe`
