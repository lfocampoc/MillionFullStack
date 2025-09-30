# Urban Nest - Frontend

Una aplicación web moderna para listar propiedades inmobiliarias, construida con Next.js 15, TypeScript y Tailwind CSS.

## 🚀 Características

- **Framework**: Next.js 15 con App Router
- **Lenguaje**: TypeScript
- **Estilos**: Tailwind CSS con tema personalizado
- **Responsive**: Diseño adaptativo para móviles y desktop
- **Componentes**: Arquitectura modular con componentes reutilizables
- **API Integration**: Conexión con API REST de .NET Core

## 📋 Funcionalidades

### Página Principal
- Lista de propiedades con diseño de tarjetas
- Formulario de filtros (búsqueda por nombre/dirección, rango de precios)
- Paginación
- Estados de carga y error

### Página de Detalles
- Vista detallada de cada propiedad
- Información completa de la propiedad
- Botones de acción (Contact Agent, Save Property)
- Navegación de regreso

### Componentes Reutilizables
- `Header`: Navegación principal con logo y menú
- `PropertyCard`: Tarjeta de propiedad con imagen y detalles
- `FilterForm`: Formulario de filtros con validación

## 🛠️ Instalación

```bash
# Instalar dependencias
npm install

# Configurar variables de entorno
# Crear archivo .env.local con:
NEXT_PUBLIC_API_URL=http://localhost:5000/api

# Ejecutar en modo desarrollo
npm run dev
```

## 🎨 Diseño

El diseño está inspirado en plataformas inmobiliarias modernas con:

- **Paleta de colores**: Azul primario (#1193d4) con tema claro/oscuro
- **Tipografía**: Manrope para una apariencia moderna
- **Iconos**: Material Symbols para consistencia visual
- **Layout**: Grid responsive con máximo ancho de 4xl

## 📱 Responsive Design

- **Mobile First**: Diseño optimizado para móviles
- **Breakpoints**: sm (640px), md (768px), lg (1024px), xl (1280px)
- **Grid System**: Adaptativo de 1 columna (móvil) a 3 columnas (desktop)
- **Navigation**: Menú colapsable en móviles

## 🔧 Configuración de la API

La aplicación se conecta con la API de .NET Core mediante:

```typescript
// src/config/api.ts
export const API_CONFIG = {
  BASE_URL: process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5000/api',
};
```

### Endpoints utilizados:
- `GET /api/properties` - Lista de propiedades con filtros
- `GET /api/properties/{id}` - Detalle de propiedad específica

## 🚀 Scripts Disponibles

```bash
npm run dev          # Desarrollo
npm run build        # Producción
npm run start        # Servidor de producción
npm run lint         # Linting
```

## 📦 Estructura del Proyecto

```
src/
├── app/                 # App Router de Next.js
│   ├── globals.css     # Estilos globales
│   ├── layout.tsx      # Layout principal
│   ├── page.tsx        # Página principal
│   └── properties/[id]/ # Página de detalles
├── components/          # Componentes reutilizables
│   ├── Header.tsx
│   ├── PropertyCard.tsx
│   └── FilterForm.tsx
├── config/             # Configuraciones
│   └── api.ts
├── services/           # Servicios de API
│   └── propertyService.ts
└── types/              # Tipos TypeScript
    └── property.ts
```

## 🎯 Próximas Mejoras

- [ ] Implementar paginación real
- [ ] Agregar modo oscuro toggle
- [ ] Implementar favoritos con localStorage
- [ ] Agregar más filtros (tipo de propiedad, área, etc.)
- [ ] Optimización de imágenes con Next.js Image
- [ ] Implementar búsqueda en tiempo real
- [ ] Agregar animaciones con Framer Motion