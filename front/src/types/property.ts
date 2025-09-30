export interface Property {
  id: string;
  name: string;
  address: string;
  price: number;
  codeInternal: string;
  year: number;
  idOwner: string;
  image?: string;
}

export interface Owner {
  id: string;
  name: string;
  address: string;
  photo?: string;
  birthday: string;
}

export interface PropertyFilter {
  name?: string;
  address?: string;
  minPrice?: number;
  maxPrice?: number;
  page?: number;
  pageSize?: number;
}

export interface ApiResponse<T> {
  success: boolean;
  data: T;
  message: string;
  error: string;
}

export interface PropertyCardProps {
  property: Property;
  onClick?: (property: Property) => void;
}

export interface FilterFormData {
  search: string;
  minPrice: string;
  maxPrice: string;
}
