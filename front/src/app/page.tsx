'use client';

import { useState, useEffect } from 'react';
import { Property, PropertyFilter } from '@/types/property';
import { propertyService } from '@/services/propertyService';
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
      setProperties(data);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Error loading properties');
      console.error('Error loading properties:', err);
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

  const formatPrice = (price: number) => {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
      minimumFractionDigits: 0,
      maximumFractionDigits: 0,
    }).format(price);
  };

  // Datos de ejemplo para mostrar el diseño exacto
  const sampleProperties = [
    {
      id: '1',
      name: 'Casa moderna en el centro',
      address: '123 Oak Street, Ciudad Central',
      price: 450000,
      image: 'https://lh3.googleusercontent.com/aida-public/AB6AXuDC4tYTpApFj8TWLc3Qme7XsaxEJe4cBhRFk_Czmuld6AU2LZtAqqTHVNABIMngaOQVIs_T-OgpJeoucY0l0ghHFR3Ad-AaPM7VuknFxrjO9FyfhhMTU2zwebEIiHf4kwX7H-vPsPYl83IsxJ1ZzTEhnDrT6ysLWF7NaZSt82jsNFbOJ53nDX5WSATw4SO3OVbBfebCjyP9oaeJyoxiC_pSUqhziHzOqqEahCfnwUNDQPnhTlC_OlmjczJQRxT8hxWKjzgbuqRFICdu'
    },
    {
      id: '2',
      name: 'Apartamento de lujo con vistas',
      address: '456 Pine Avenue, Distrito de Lujo',
      price: 750000,
      image: 'https://lh3.googleusercontent.com/aida-public/AB6AXuBRgWulSHhwY4yRO9MLGgSiusBKVzazjiU7KsddvurvDUJzhuhPzB6yjivvmZubE3foM33JNH8bqapboTRXYH2vSdtFtXaNZmmGZaptCFUs0jPDgV2VM22XD_9nIJsjYXRqwDzmzRXwwfqC3QlyV3kEItSHYM7ug7SnS8xEqr25vVM3OldL5FCW6A4UrJCQq7pqEhAc65oP54BX0DRt7_rwKigK9ZSelafT5NC6ag7sCcQ8QzswkMZ8WHeJZ-T_J6nouvsCk8LIgK8i'
    },
    {
      id: '3',
      name: 'Villa con piscina privada',
      address: '789 Maple Drive, Zona Residencial',
      price: 950000,
      image: 'https://lh3.googleusercontent.com/aida-public/AB6AXuDxv2ye-FmOh9hcVo8SPiPD9QLzzPSqKQn0oEJrmZuPuhSnMV50M9yKUUSIsgftkHc5xWZwuuPdwLcBaguRkqZMft6zQO91ObHnGCXlr6sdzEnuezcqXWJS_1fy7pUF5TR5nLb0hIpmYC0QUGuqxQOqWtuUJ7r2vcTbV6TgbCuhUwD4ed16e9p77wGa-qiyZfG5iWLMsE5znV7mtQqo6a0HMa-Z5qTLsnLM2oarlZP-01eQ-llaNPvt2BX2NAxwZC9vmbNOkj56MDKZ'
    },
    {
      id: '4',
      name: 'Casa de campo con encanto',
      address: '101 Elm Road, Pueblo Tranquilo',
      price: 350000,
      image: 'https://lh3.googleusercontent.com/aida-public/AB6AXuBWvEy8SdCTYiRCx4Kj8S3Eikth_1n0YNl_odCedTzePYFFy373CTHSKnlHbK2qbW-eLtBulAAS6rIjKuPgEgE6muGbH2g1RTahS2cTBxjvAY3fvxTNKXNTrHCLJfO39eq2HKkOM4LONG5GM7rm2Fws2XZ8x_lcutJaDNh4uEc8t1u9rhltZ5vrASIz_FzcgRAaM5rp3DmZ67t5uukQBJBlciS_wVf0B5OeYQ6Nan61eSJ4n0M1xVhS4zbveUKo2K2lxs1GxDY1KOK_'
    },
    {
      id: '5',
      name: 'Dúplex espacioso en zona residencial',
      address: '222 Cedar Lane, Barrio Familiar',
      price: 550000,
      image: 'https://lh3.googleusercontent.com/aida-public/AB6AXuAjIpqxkVV5IErLETfy5S-7ASNxejGUQUKtVJ7VF7XSoXqGfHMlQTcB7QE5oSYHVSO7A4wZcXJrX9WTjGdHG-37llYBVtd1tNt3Oi8EdR2A84KfGVkXuBm9vnFrz_kj3raje0wZNhlmXWsqomrGKjrLLO1GB3hS5C86cjP9J7vom2TrSXgzLNmkyfo76P8DB6S5sX7H3q3KxsVsRUN-cg7dbJGKi8guj0arFHTIn8VDnc3SO7Ou9_tsctvJiryPF_Ls35m435S6rODS'
    },
    {
      id: '6',
      name: 'Estudio moderno para solteros',
      address: '333 Birch Court, Zona Urbana',
      price: 250000,
      image: 'https://lh3.googleusercontent.com/aida-public/AB6AXuAABqDu_CQ9iFsBHjTNuD-kwLJKGaoLmZkDDF1YHt453mZThuvzwL3rzJ7XzYHqyFPGVyK9VVPZOI8A04ijE6tVvTPJhFaWg6zmzAX4Uam0ILgnkO9Tukp93qexQZ9GmqnWZDnzLgihpRhy7ShuMa2HDUx9kX9-KZcIgBYltY8cWrtLWvqjpIOaLjKtbO2ArQ_LiVOeNc6nZUjfZaDWMcKn4bvcCq51IR9k5i7hIQO1RjTqrcXqXpwPcalnUGq7bIiSJ5kzSfyiHhkM'
    }
  ];

  // Usar datos de ejemplo si no hay datos de la API
  const displayProperties = properties.length > 0 ? properties : sampleProperties;

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

      {/* Search Form - Exact styling from design */}
      <div className="bg-background-light dark:bg-background-dark/50 p-6 rounded-xl shadow-md mb-12 space-y-6">
        <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
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
        </div>
      </div>

      {/* Error Message */}
      {error && (
        <div className="bg-red-50 dark:bg-red-900/20 border border-red-200 dark:border-red-800 text-red-700 dark:text-red-400 px-4 py-3 rounded-lg mb-6">
          <div className="flex items-center gap-2">
            <span className="material-symbols-outlined">error</span>
            <strong>Error:</strong> {error}
          </div>
        </div>
      )}

      {/* Loading */}
      {loading && (
        <div className="flex justify-center items-center py-12">
          <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-primary"></div>
        </div>
      )}

      {/* Properties Grid - Exact styling from design */}
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-8">
        {displayProperties.map((property) => (
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
    </>
  );
}