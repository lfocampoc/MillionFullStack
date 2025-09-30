# Real Estate API - Full Stack Application

API REST con .NET 9 y frontend Next.js 15 para gestión de propiedades inmobiliarias.

## 🚀 Inicio Rápido

### Prerrequisitos
- .NET 9 SDK
- Node.js 18+
- MongoDB

### Backend
```bash
cd api
dotnet restore
dotnet run --project RealEstateAPI.API
```
La API se abrirá automáticamente en Swagger:
- `https://localhost:7030/swagger/index.html` (Kestrel)
- `https://localhost:44308/swagger/index.html` (IIS Express)

### Frontend
```bash
cd front
npm install
cp env.example .env.local
npm run dev
```
**⚠️ IMPORTANTE**: Editar `.env.local` con:
```env
NEXT_PUBLIC_API_URL=https://localhost:7030/api
```
Frontend: `http://localhost:3000`

### Database
- **SeedData automático**: Se carga al iniciar la API
- **Backup manual**: `mongodump --db RealEstateDB --out ./backup`

## 📊 Funcionalidades

- **API REST**: CRUD completo de propiedades
- **Filtros**: Nombre, dirección, rango de precios
- **Frontend**: Lista responsive con filtros
- **Testing**: 87 tests unitarios con NUnit
- **Base de Datos**: MongoDB con SeedData automático

## 🧪 Tests

```bash
cd api/RealEstateAPI.Tests
dotnet test
```

## 📚 Documentación

- [API Documentation](api/API_DOCUMENTATION.md)
- [Database Documentation](DATABASE_DOCUMENTATION.md)
- [Installation Guide](INSTALLATION_GUIDE.md)

## 🔧 Tecnologías

**Backend**: .NET 9, MongoDB, AutoMapper, FluentValidation, Serilog, NUnit  
**Frontend**: Next.js 15, TypeScript, Tailwind CSS
