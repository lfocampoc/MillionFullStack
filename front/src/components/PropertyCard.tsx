'use client';

import { Property } from '@/lib/types';
import { formatPrice } from '@/lib/utils';
import Link from 'next/link';
import Image from 'next/image';

interface PropertyCardProps {
  property: Property;
}

export default function PropertyCard({ property }: PropertyCardProps) {

  return (
    <div className="bg-background-light dark:bg-background-dark rounded-xl overflow-hidden shadow-md border border-slate-200 dark:border-slate-800 flex flex-col">
      <div className="relative">
        <Image 
          alt={property.name} 
          className="w-full h-48 object-cover" 
          src={property.image || '/placeholder-property.jpg'}
          width={400}
          height={300}
          placeholder="blur"
          blurDataURL="data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDAAYEBQYFBAYGBQYHBwYIChAKCgkJChQODwwQFxQYGBcUFhYaHSUfGhsjHBYWICwgIyYnKSopGR8tMC0oMCUoKSj/2wBDAQcHBwoIChMKChMoGhYaKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCj/wAARCAABAAEDASIAAhEBAxEB/8QAFQABAQAAAAAAAAAAAAAAAAAAAAv/xAAhEAACAQMDBQAAAAAAAAAAAAABAgMABAUGIWGRkqGx0f/EABUBAQEAAAAAAAAAAAAAAAAAAAMF/8QAGhEAAgIDAAAAAAAAAAAAAAAAAAECEgMRkf/aAAwDAQACEQMRAD8AltJagyeH0AthI5xdrLcNM91BF5pX2HaH9bcfaSXWGaRmknyJckliyjqTzSlT54b6bk+h0R//2Q=="
        />
        <div className="absolute top-2 right-2 bg-primary text-white text-xs font-bold px-2 py-1 rounded-full">
          {formatPrice(property.price)}
        </div>
      </div>
      <div className="p-4 flex flex-col flex-grow">
        <h3 className="text-lg font-bold text-slate-900 dark:text-white">{property.name}</h3>
        <p className="text-sm text-slate-500 dark:text-slate-400 mb-4">{property.address}</p>
        <div className="text-xs text-slate-400 dark:text-slate-500 mb-4">
          <span className="material-symbols-outlined inline-block mr-1">calendar_month</span>
          Built in {property.year}
        </div>
        <div className="mt-auto">
          <Link 
            href={`/properties/${property.id}`}
            className="w-full text-center px-4 py-2 text-sm font-bold text-white bg-primary rounded-lg hover:bg-primary/90 transition-colors block"
          >
            View Details
          </Link>
        </div>
      </div>
    </div>
  );
}
