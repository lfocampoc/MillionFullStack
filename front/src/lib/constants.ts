// Configuración de la API - Servicios CRUD

// Configuración de la API
export const API_CONFIG = {
  BASE_URL: process.env.NEXT_PUBLIC_API_URL || 'https://localhost:44308/api',
  ENDPOINTS: {
    // GET - Obtener todas las propiedades (con filtros)
    PROPERTIES: '/properties',
    // GET - Obtener una propiedad por ID
    PROPERTY_BY_ID: (id: string) => `/properties/${id}`,
    // POST - Crear nueva propiedad
    CREATE_PROPERTY: '/properties',
    // PUT - Actualizar propiedad existente
    UPDATE_PROPERTY: (id: string) => `/properties/${id}`,
    // DELETE - Eliminar propiedad
    DELETE_PROPERTY: (id: string) => `/properties/${id}`
  }
};

// Configuración de paginación
export const PAGINATION_CONFIG = {
  DEFAULT_PAGE_SIZE: 9,
  MAX_PAGE_SIZE: 20
};
