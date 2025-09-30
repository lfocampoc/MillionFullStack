// Configuración de la API
export const API_CONFIG = {
  BASE_URL: 'https://localhost:44308/api',
  ENDPOINTS: {
    PROPERTIES: '/properties',
    PROPERTY_BY_ID: (id: string) => `/properties/${id}`,
    CREATE_PROPERTY: '/properties',
    UPDATE_PROPERTY: (id: string) => `/properties/${id}`,
    DELETE_PROPERTY: (id: string) => `/properties/${id}`
  }
};

// Configuración de paginación
export const PAGINATION_CONFIG = {
  DEFAULT_PAGE_SIZE: 9,
  MAX_PAGE_SIZE: 20
};
