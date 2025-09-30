import type { Metadata } from "next";
import { Manrope } from "next/font/google";
import "./globals.scss";
import Header from "@/components/Header";
import Footer from "@/components/Footer";

const manrope = Manrope({
  subsets: ["latin"],
  weight: ["400", "500", "700", "800"],
  display: "swap",
  variable: "--font-manrope",
});

export const metadata: Metadata = {
  title: "Urban Nest - Real Estate",
  description: "Encuentra la propiedad de tus sueños. Explora nuestra exclusiva selección de propiedades de lujo en las mejores ubicaciones.",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <head>
        <link 
          href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined" 
          rel="stylesheet" 
        />
      </head>
      <body className={`${manrope.variable} bg-background-light dark:bg-background-dark font-display text-gray-800`}>
        <div className="flex flex-col min-h-screen">
          <Header />
          <main className="flex-grow container mx-auto px-4 sm:px-6 lg:px-8 py-8 md:py-12">
            {children}
          </main>
          <Footer />
        </div>
      </body>
    </html>
  );
}