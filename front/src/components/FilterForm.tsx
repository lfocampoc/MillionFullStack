'use client';

import { useState } from 'react';
import { PropertyFilter } from '@/types/property';

interface FilterFormProps {
  onFilter: (filter: PropertyFilter) => void;
  loading?: boolean;
}

export default function FilterForm({ onFilter, loading = false }: FilterFormProps) {
  const [formData, setFormData] = useState({
    search: '',
    minPrice: '',
    maxPrice: '',
  });

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    
    const filter: PropertyFilter = {
      page: 1,
      pageSize: 12,
    };

    if (formData.search.trim()) {
      // Buscar tanto en name como en address
      filter.name = formData.search.trim();
      filter.address = formData.search.trim();
    }

    if (formData.minPrice) {
      filter.minPrice = parseInt(formData.minPrice);
    }

    if (formData.maxPrice) {
      filter.maxPrice = parseInt(formData.maxPrice);
    }

    onFilter(filter);
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value,
    }));
  };

  return (
    <div className="bg-background-light dark:bg-background-dark p-6 rounded-xl shadow-sm border border-slate-200 dark:border-slate-800 mb-12">
      <form onSubmit={handleSubmit}>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 items-end">
          <div className="lg:col-span-2">
            <label className="block text-sm font-medium text-slate-700 dark:text-slate-300 mb-1" htmlFor="search">
              Search
            </label>
            <div className="relative">
              <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                <span className="material-symbols-outlined text-slate-400">search</span>
              </div>
              <input
                className="w-full pl-10 pr-4 py-2.5 bg-slate-100 dark:bg-slate-900 border border-slate-300 dark:border-slate-700 rounded-lg focus:ring-primary focus:border-primary text-slate-900 dark:text-slate-100"
                id="search"
                name="search"
                placeholder="City, Address, ZIP..."
                type="text"
                value={formData.search}
                onChange={handleInputChange}
              />
            </div>
          </div>
          
          <div>
            <label className="block text-sm font-medium text-slate-700 dark:text-slate-300 mb-1" htmlFor="minPrice">
              Min Price
            </label>
            <input
              className="w-full py-2.5 px-4 bg-slate-100 dark:bg-slate-900 border border-slate-300 dark:border-slate-700 rounded-lg focus:ring-primary focus:border-primary text-slate-900 dark:text-slate-100"
              id="minPrice"
              name="minPrice"
              type="number"
              placeholder="Min Price"
              value={formData.minPrice}
              onChange={handleInputChange}
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-slate-700 dark:text-slate-300 mb-1" htmlFor="maxPrice">
              Max Price
            </label>
            <input
              className="w-full py-2.5 px-4 bg-slate-100 dark:bg-slate-900 border border-slate-300 dark:border-slate-700 rounded-lg focus:ring-primary focus:border-primary text-slate-900 dark:text-slate-100"
              id="maxPrice"
              name="maxPrice"
              type="number"
              placeholder="Max Price"
              value={formData.maxPrice}
              onChange={handleInputChange}
            />
          </div>

          <div className="md:col-span-2 lg:col-span-3 flex justify-end">
            <button
              type="submit"
              disabled={loading}
              className="w-full md:w-auto px-6 py-2.5 text-sm font-bold text-white bg-primary rounded-lg hover:bg-primary/90 transition-colors flex items-center justify-center gap-2 disabled:opacity-50 disabled:cursor-not-allowed"
            >
              <span className="material-symbols-outlined">filter_alt</span>
              {loading ? 'Searching...' : 'Apply Filters'}
            </button>
          </div>
        </div>
      </form>
    </div>
  );
}
