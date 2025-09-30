'use client';

import Link from 'next/link';

export default function Footer() {
  return (
    <footer className="bg-background-light dark:bg-background-dark/50 border-t border-gray-200 dark:border-gray-700">
      <div className="container mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <div className="flex flex-col md:flex-row justify-between items-center space-y-6 md:space-y-0">
          <div className="text-center md:text-left">
            <p className="text-sm text-gray-500 dark:text-gray-400">© 2024 Urban Nest. Todos los derechos reservados.</p>
          </div>
          <div className="flex space-x-6">
            <Link 
              href="#" 
              className="text-gray-500 hover:text-primary dark:text-gray-400 dark:hover:text-primary transition-colors"
            >
              Facebook
            </Link>
            <Link 
              href="#" 
              className="text-gray-500 hover:text-primary dark:text-gray-400 dark:hover:text-primary transition-colors"
            >
              Twitter
            </Link>
            <Link 
              href="#" 
              className="text-gray-500 hover:text-primary dark:text-gray-400 dark:hover:text-primary transition-colors"
            >
              Instagram
            </Link>
          </div>
        </div>
      </div>
    </footer>
  );
}