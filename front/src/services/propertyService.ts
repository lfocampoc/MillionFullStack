import { Property, PropertyFilter, ApiResponse } from '@/lib/types';
import { API_CONFIG } from '@/lib/constants';

class PropertyService {
  private async request<T>(endpoint: string, options?: RequestInit): Promise<T> {
    const url = `${API_CONFIG.BASE_URL}${endpoint}`;
    
    try {
      const response = await fetch(url, {
        headers: {
          'Content-Type': 'application/json',
          ...options?.headers,
        },
        ...options,
      });

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      const data: ApiResponse<T> = await response.json();
      
      if (!data.success) {
        throw new Error(data.error || 'Error en la respuesta de la API');
      }

      return data.data;
    } catch (error) {
      console.error('Error en PropertyService:', error);
      if (error instanceof TypeError && error.message.includes('fetch')) {
        throw new Error('No se pudo conectar con el servidor. Verifica que la API esté ejecutándose.');
      }
      throw error;
    }
  }

  async getProperties(filter?: PropertyFilter): Promise<Property[]> {
    const params = new URLSearchParams();
    
    if (filter?.name) params.append('name', filter.name);
    if (filter?.address) params.append('address', filter.address);
    if (filter?.minPrice) params.append('minPrice', filter.minPrice.toString());
    if (filter?.maxPrice) params.append('maxPrice', filter.maxPrice.toString());
    if (filter?.page) params.append('page', filter.page.toString());
    if (filter?.pageSize) params.append('pageSize', filter.pageSize.toString());

    const queryString = params.toString();
    const endpoint = `${API_CONFIG.ENDPOINTS.PROPERTIES}${queryString ? `?${queryString}` : ''}`;
    
    return this.request<Property[]>(endpoint);
  }

  async getPropertyById(id: string): Promise<Property> {
    return this.request<Property>(API_CONFIG.ENDPOINTS.PROPERTY_BY_ID(id));
  }

  async createProperty(property: Omit<Property, 'id'>): Promise<Property> {
    return this.request<Property>(API_CONFIG.ENDPOINTS.CREATE_PROPERTY, {
      method: 'POST',
      body: JSON.stringify(property),
    });
  }

  async updateProperty(id: string, property: Partial<Property>): Promise<Property> {
    return this.request<Property>(API_CONFIG.ENDPOINTS.UPDATE_PROPERTY(id), {
      method: 'PUT',
      body: JSON.stringify(property),
    });
  }

  async deleteProperty(id: string): Promise<void> {
    return this.request<void>(API_CONFIG.ENDPOINTS.DELETE_PROPERTY(id), {
      method: 'DELETE',
    });
  }
}

export const propertyService = new PropertyService();
