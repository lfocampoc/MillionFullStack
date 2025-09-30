// Tipos base para propiedades
export interface Property {
  id: string;
  name: string;
  address: string;
  price: number;
  year?: number;
  codeInternal?: string;
  idOwner?: string;
  image?: string;
  images?: PropertyImage[];
  owner?: PropertyOwner;
  traces?: PropertyTrace[];
}

export interface PropertyImage {
  id: string;
  file: string;
  enabled: boolean;
  idProperty: string;
}

export interface PropertyOwner {
  id: string;
  name: string;
  address: string;
  photo?: string;
  birthday?: string;
}

export interface PropertyTrace {
  id: string;
  dateSale: string;
  name: string;
  value: number;
  tax: number;
    idProperty: string;
}

// Tipos para filtros
export interface PropertyFilter {
  name?: string;
  address?: string;
  minPrice?: number;
  maxPrice?: number;
  page?: number;
  pageSize?: number;
}

// Tipos para respuestas de API
export interface ApiResponse<T> {
  success: boolean;
  data: T;
  message: string;
  error?: string;
}

// Tipos para configuración
export interface AppConfig {
  name: string;
  description: string;
  hero: {
    title: string;
    subtitle: string;
  };
}

export interface ApiConfig {
  BASE_URL: string;
  ENDPOINTS: {
    PROPERTIES: string;
    PROPERTY_BY_ID: (id: string) => string;
  };
}

// Tipos para navegación
export interface NavigationItem {
  label: string;
  href: string;
  active?: boolean;
}

// Tipos para formularios
export interface SearchFormData {
  search: string;
  minPrice: string;
  maxPrice: string;
}

// Tipos para paginación
export interface PaginationConfig {
  DEFAULT_PAGE_SIZE: number;
  MAX_PAGE_SIZE: number;
}

// Tipos para filtros de precio
export interface PriceRange {
  label: string;
  value: string;
}

// Tipos para configuración de imágenes
export interface ImageDimensions {
  width: number;
  height: number;
}

export interface ImageConfig {
  PLACEHOLDER: string;
  DIMENSIONS: {
    CARD: ImageDimensions;
    DETAIL: ImageDimensions;
    THUMBNAIL: ImageDimensions;
  };
}
