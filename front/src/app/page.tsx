'use client';

import { useState, useEffect } from 'react';
import { Property, PropertyFilter } from '@/lib/types';
import { propertyService } from '@/services/propertyService';
import { formatPrice } from '@/lib/utils';
import Link from 'next/link';

export default function HomePage() {
  const [properties, setProperties] = useState<Property[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [filters, setFilters] = useState({
    search: '',
    minPrice: '',
    maxPrice: '',
  });

  const loadProperties = async (filter?: PropertyFilter) => {
    try {
      setLoading(true);
      setError(null);
      const data = await propertyService.getProperties(filter);
      setProperties(data as Property[]);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Error al cargar las propiedades');
      console.error('Error cargando propiedades:', err);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadProperties();
  }, []);

  const handleSearch = () => {
    const filter: PropertyFilter = {
      page: 1,
      pageSize: 20,
    };

    if (filters.search.trim()) {
      filter.name = filters.search.trim();
    }

    if (filters.minPrice) {
      filter.minPrice = parseInt(filters.minPrice);
    }

    if (filters.maxPrice) {
      filter.maxPrice = parseInt(filters.maxPrice);
    }

    loadProperties(filter);
  };

  const handleKeyPress = (e: React.KeyboardEvent) => {
    if (e.key === 'Enter') {
      handleSearch();
    }
  };

  const showNoProperties = !loading && !error && properties.length === 0;

  return (
    <>
      <div className="text-center mb-12">
        <h1 className="text-4xl md:text-5xl font-bold text-gray-900">
          Encuentra la Propiedad de tus Sueños
        </h1>
        <p className="mt-4 text-lg text-gray-600 dark:text-gray-400">
          Explora nuestra exclusiva selección de propiedades de lujo en las mejores ubicaciones.
        </p>
      </div>

      {/* Formulario de búsqueda */}
      <div className="search-form bg-background-light dark:bg-background-dark/50 p-6 rounded-xl shadow-md mb-12 space-y-6">
        <div className="grid grid-cols-1 md:grid-cols-4 gap-6">
          <div className="relative">
            <span className="material-symbols-outlined absolute left-3 top-1/2 -translate-y-1/2 text-gray-400">search</span>
            <input 
              className="w-full pl-10 pr-4 py-3 rounded-lg bg-white dark:bg-gray-800 border border-gray-300 dark:border-gray-600 focus:ring-2 focus:ring-primary focus:border-primary transition-all" 
              placeholder="Buscar por nombre o dirección" 
              type="text"
              value={filters.search}
              onChange={(e) => setFilters({...filters, search: e.target.value})}
              onKeyPress={handleKeyPress}
            />
          </div>
          <div className="relative">
            <span className="material-symbols-outlined absolute left-3 top-1/2 -translate-y-1/2 text-gray-400">attach_money</span>
            <input 
              className="w-full pl-10 pr-4 py-3 rounded-lg bg-white dark:bg-gray-800 border border-gray-300 dark:border-gray-600 focus:ring-2 focus:ring-primary focus:border-primary transition-all" 
              placeholder="Precio mínimo" 
              type="number"
              value={filters.minPrice}
              onChange={(e) => setFilters({...filters, minPrice: e.target.value})}
            />
          </div>
          <div className="relative">
            <span className="material-symbols-outlined absolute left-3 top-1/2 -translate-y-1/2 text-gray-400">attach_money</span>
            <input 
              className="w-full pl-10 pr-4 py-3 rounded-lg bg-white dark:bg-gray-800 border border-gray-300 dark:border-gray-600 focus:ring-2 focus:ring-primary focus:border-primary transition-all" 
              placeholder="Precio máximo" 
              type="number"
              value={filters.maxPrice}
              onChange={(e) => setFilters({...filters, maxPrice: e.target.value})}
            />
          </div>
          <button
            onClick={handleSearch}
            className="bg-primary text-white px-6 py-3 rounded-lg font-semibold hover:bg-primary/90 transition-colors flex items-center justify-center gap-2"
          >
            <span className="material-symbols-outlined">search</span>
            Buscar
          </button>
        </div>
      </div>

      {/* Mensaje de error */}
      {error && (
        <div className="bg-red-50 dark:bg-red-900/20 border border-red-200 dark:border-red-800 text-red-700 dark:text-red-400 px-4 py-3 rounded-lg mb-6">
          <div className="flex items-center gap-2">
            <span className="material-symbols-outlined">error</span>
            <strong>Error:</strong> {error}
          </div>
        </div>
      )}

      {/* Cargando */}
      {loading && (
        <div className="flex justify-center items-center py-12">
          <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-primary"></div>
        </div>
      )}

      {/* Sin propiedades */}
      {showNoProperties && (
        <div className="text-center py-12">
          <div className="bg-gray-50 dark:bg-gray-800 rounded-lg p-8">
            <span className="material-symbols-outlined text-6xl text-gray-400 mb-4 block">home_work</span>
            <h3 className="text-xl font-semibold text-gray-700 dark:text-gray-300 mb-2">
              No se encontraron propiedades
            </h3>
            <p className="text-gray-500 dark:text-gray-400">
              Intenta ajustar los filtros de búsqueda o vuelve más tarde.
            </p>
          </div>
        </div>
      )}

      {/* Lista de propiedades */}
      {properties.length > 0 && (
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-8">
          {properties.map((property) => (
            <div key={property.id} className="bg-white rounded-xl overflow-hidden shadow-lg hover:shadow-2xl transition-shadow duration-300 transform hover:-translate-y-1">
              <div 
                className="w-full h-56 bg-cover bg-center" 
                style={{backgroundImage: `url("${property.image}")`}}
              ></div>
              <div className="p-6">
                <h3 className="text-xl font-bold text-gray-900 dark:text-black mb-2">{property.name}</h3>
                <p className="text-gray-600 dark:text-gray-400 mb-4">{property.address}</p>
                <div className="flex justify-between items-center">
                  <p className="text-2xl font-bold text-primary">{formatPrice(property.price)}</p>
                  <Link 
                    href={`/properties/${property.id}`}
                    className="bg-primary text-white px-4 py-2 rounded-lg font-semibold hover:bg-primary/90 transition-colors"
                  >
                    Ver Detalles
                  </Link>
                </div>
              </div>
            </div>
          ))}
        </div>
      )}
    </>
  );
}