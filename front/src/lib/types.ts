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

