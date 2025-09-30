'use client';

import { useState, useEffect } from 'react';
import { useParams } from 'next/navigation';
import { Property } from '@/types/property';
import { propertyService } from '@/services/propertyService';
import { formatPrice } from '@/lib/utils';
import Link from 'next/link';
import Image from 'next/image';

export default function PropertyDetailsPage() {
  const params = useParams();
  const [property, setProperty] = useState<Property | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const loadProperty = async () => {
      try {
        setLoading(true);
        setError(null);
        const data = await propertyService.getPropertyById(params.id as string);
        setProperty(data as Property);
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Error al cargar la propiedad');
      } finally {
        setLoading(false);
      }
    };

    if (params.id) {
      loadProperty();
    }
  }, [params.id]);

  if (loading) {
    return (
      <div className="flex justify-center items-center py-12">
        <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-primary"></div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="bg-red-50 dark:bg-red-900/20 border border-red-200 dark:border-red-800 text-red-700 dark:text-red-400 px-4 py-3 rounded-lg">
        <div className="flex items-center gap-2">
          <span className="material-symbols-outlined">error</span>
          <strong>Error:</strong> {error}
        </div>
      </div>
    );
  }

  if (!property) {
    return (
      <div className="text-center py-12">
        <div className="text-6xl mb-4">🏠</div>
        <h1 className="text-2xl font-bold text-gray-900 dark:text-white mb-4">
          Propiedad no encontrada
        </h1>
        <Link 
          href="/"
          className="inline-flex items-center gap-2 text-primary hover:text-primary/80 transition-colors"
        >
          <span className="material-symbols-outlined">arrow_back</span>
          Volver a la lista
        </Link>
      </div>
    );
  }

  return (
    <div className="max-w-4xl mx-auto">
      {/* Navegación */}
      <div className="mb-6 text-sm font-medium text-slate-500 dark:text-slate-400">
        <Link className="hover:text-primary dark:hover:text-primary" href="/">
          Propiedades
        </Link>
        <span className="mx-2">/</span>
        <span className="text-slate-800 dark:text-black">{property.name}</span>
      </div>

      {/* Imagen principal */}
      <div className="w-full h-96 rounded-xl overflow-hidden mb-8 shadow-lg">
        <Image
          alt={property.name}
          className="w-full h-full object-cover"
          src={property.image || 'https://lh3.googleusercontent.com/aida-public/AB6AXuAgM9pGVSg9IZiSketcdeI9L4DxWus8HaJSLUhEhkpPGSfD8VVYgVCfSTB90_Zunb3_U3yfNz2UPi1oCvSi-surDShTnTzR_21ar6DIbtnqWmMOJCR70qE6RaIoBNgqVHx_hC21AjqxyiDm2qeFR6nnhtS7eaf7yFxEGjKGUkbTQ1rU-w2UuwJ96gzY9O2f2AA7GKvQfKtllUx5ZGD8ypxERY0hjKxRsorct5vnSKtfnQSt4nvWaaKrV3SUuDBdfoWnzNN2d_eOkpH7'}
          width={800}
          height={400}
          placeholder="blur"
          blurDataURL="data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDAAYEBQYFBAYGBQYHBwYIChAKCgkJChQODwwQFxQYGBcUFhYaHSUfGhsjHBYWICwgIyYnKSopGR8tMC0oMCUoKSj/2wBDAQcHBwoIChMKChMoGhYaKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCj/wAARCAABAAEDASIAAhEBAxEB/8QAFQABAQAAAAAAAAAAAAAAAAAAAAv/xAAhEAACAQMDBQAAAAAAAAAAAAABAgMABAUGIWGRkqGx0f/EABUBAQEAAAAAAAAAAAAAAAAAAAMF/8QAGhEAAgIDAAAAAAAAAAAAAAAAAAECEgMRkf/aAAwDAQACEQMRAD8AltJagyeH0AthI5xdrLcNM91BF5pX2HaH9bcfaSXWGaRmknyJckliyjqTzSlT54b6bk+h0R//2Q=="
        />
      </div>

      {/* Título y precio */}
      <div className="flex flex-col md:flex-row justify-between items-start gap-8 mb-8">
        <div className="flex-grow">
          <h1 className="text-4xl font-extrabold text-slate-900 dark:text-black mb-2">
            {property.name}
          </h1>
          <p className="text-lg text-slate-500 dark:text-slate-400">
            Una casa moderna con vistas panorámicas
          </p>
        </div>
        <div className="text-4xl font-extrabold text-primary flex-shrink-0">
          {formatPrice(property.price)}
        </div>
      </div>

      {/* Descripción */}
      <div className="prose prose-lg max-w-none text-slate-600 dark:text-slate-300 mb-10">
        <p>
          Esta impresionante casa de 4 dormitorios y 3 baños ofrece una combinación perfecta de lujo y comodidad. Con una amplia sala de estar, una cocina gourmet y un patio trasero con piscina, es ideal para el entretenimiento. La suite principal cuenta con un baño en suite y un vestidor. Ubicada en un vecindario tranquilo, esta casa está cerca de escuelas, parques y tiendas.
        </p>
      </div>

      {/* Detalles de la propiedad */}
      <div className="border-t border-b border-black/10 dark:border-white/10 divide-y divide-black/10 dark:divide-white/10">
        <div className="grid grid-cols-1 md:grid-cols-2">
          <div className="p-4">
            <p className="text-sm text-slate-500 dark:text-slate-400 mb-1">Dirección</p>
            <p className="text-base font-semibold text-slate-800 dark:text-black">{property.address}</p>
          </div>
          <div className="p-4 md:border-l md:border-black/10 md:dark:border-white/10">
            <p className="text-sm text-slate-500 dark:text-slate-400 mb-1">Dormitorios</p>
            <p className="text-base font-semibold text-slate-800 dark:text-black">4</p>
          </div>
        </div>
        <div className="grid grid-cols-1 md:grid-cols-2">
          <div className="p-4">
            <p className="text-sm text-slate-500 dark:text-slate-400 mb-1">Baños</p>
            <p className="text-base font-semibold text-slate-800 dark:text-black">3</p>
          </div>
          <div className="p-4 md:border-l md:border-black/10 md:dark:border-white/10">
            <p className="text-sm text-slate-500 dark:text-slate-400 mb-1">Tamaño</p>
            <p className="text-base font-semibold text-slate-800 dark:text-black">2,500 pies cuadrados</p>
          </div>
        </div>
        <div className="grid grid-cols-1 md:grid-cols-2">
          <div className="p-4">
            <p className="text-sm text-slate-500 dark:text-slate-400 mb-1">Tipo de propiedad</p>
            <p className="text-base font-semibold text-slate-800 dark:text-black">Residencial</p>
          </div>
        </div>
      </div>

      {/* Botón de regreso */}
      <div className="mt-10">
        <Link 
          href="/"
          className="bg-primary/20 dark:bg-primary/30 text-primary px-6 py-3 rounded-lg text-sm font-bold hover:bg-primary/30 dark:hover:bg-primary/40 transition-colors"
        >
          Volver a la lista
        </Link>
      </div>
    </div>
  );
}