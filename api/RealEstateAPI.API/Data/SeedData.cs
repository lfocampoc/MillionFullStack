using MongoDB.Bson;
using MongoDB.Driver;
using RealEstateAPI.Core.Entities;

namespace RealEstateAPI.API.Data
{
    public static class SeedData
    {
        public static async Task SeedDatabaseAsync(IMongoDatabase database)
        {
            // Crear colecciones si no existen
            await CreateCollectionsIfNotExistAsync(database);
            
            // Verificar si ya hay datos
            var propertiesCollection = database.GetCollection<Property>("properties");
            var ownersCollection = database.GetCollection<Owner>("owners");
            
            if (await propertiesCollection.CountDocumentsAsync(_ => true) > 0)
            {
                return; // Ya hay datos, no hacer nada
            }
            
            // Crear propietarios de prueba
            var owners = CreateSampleOwners();
            await ownersCollection.InsertManyAsync(owners);
            
            // Crear propiedades de prueba
            var properties = CreateSampleProperties(owners);
            await propertiesCollection.InsertManyAsync(properties);
            
            Console.WriteLine("✅ Datos de prueba creados exitosamente");
        }
        
        private static async Task CreateCollectionsIfNotExistAsync(IMongoDatabase database)
        {
            var collections = await database.ListCollectionNamesAsync();
            var collectionNames = await collections.ToListAsync();
            
            if (!collectionNames.Contains("owners"))
            {
                await database.CreateCollectionAsync("owners");
            }
            
            if (!collectionNames.Contains("properties"))
            {
                await database.CreateCollectionAsync("properties");
            }
            
            if (!collectionNames.Contains("propertyImages"))
            {
                await database.CreateCollectionAsync("propertyImages");
            }
            
            if (!collectionNames.Contains("propertyTraces"))
            {
                await database.CreateCollectionAsync("propertyTraces");
            }
        }
        
        private static List<Owner> CreateSampleOwners()
        {
            return new List<Owner>
            {
                new Owner
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = "Juan Pérez",
                    Address = "Av. Principal 123, Ciudad",
                    Photo = "https://i.pravatar.cc/150?img=1",
                    Birthday = new DateTime(1980, 5, 15)
                },
                new Owner
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = "María González",
                    Address = "Calle Secundaria 456, Ciudad",
                    Photo = "https://i.pravatar.cc/150?img=2",
                    Birthday = new DateTime(1975, 8, 22)
                },
                new Owner
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = "Carlos Rodríguez",
                    Address = "Plaza Central 789, Ciudad",
                    Photo = "https://i.pravatar.cc/150?img=3",
                    Birthday = new DateTime(1985, 12, 10)
                }
            };
        }
        
        private static List<Property> CreateSampleProperties(List<Owner> owners)
        {
            var properties = new List<Property>();
            
            // Propiedad 1
            var property1Id = ObjectId.GenerateNewId().ToString();
            properties.Add(new Property
            {
                Id = property1Id,
                Name = "Casa Moderna en Zona Norte",
                Address = "Av. Norte 100, Zona Norte",
                Price = 250000,
                CodeInternal = "PROP-001",
                Year = 2020,
                IdOwner = owners[0].Id,
                Images = new List<PropertyImage>
                {
                    new PropertyImage
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        File = "https://picsum.photos/800/600?random=1",
                        Enabled = true,
                        IdProperty = property1Id
                    },
                    new PropertyImage
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        File = "https://picsum.photos/800/600?random=2",
                        Enabled = true,
                        IdProperty = property1Id
                    }
                },
                Traces = new List<PropertyTrace>
                {
                    new PropertyTrace
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        DateSale = new DateTime(2020, 3, 15),
                        Name = "Venta inicial",
                        Value = 250000,
                        Tax = 25000,
                        IdProperty = property1Id
                    }
                }
            });
            
            // Propiedad 2
            var property2Id = ObjectId.GenerateNewId().ToString();
            properties.Add(new Property
            {
                Id = property2Id,
                Name = "Apartamento Centro Histórico",
                Address = "Calle Histórica 50, Centro",
                Price = 180000,
                CodeInternal = "PROP-002",
                Year = 2018,
                IdOwner = owners[1].Id,
                Images = new List<PropertyImage>
                {
                    new PropertyImage
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        File = "https://picsum.photos/800/600?random=3",
                        Enabled = true,
                        IdProperty = property2Id
                    }
                },
                Traces = new List<PropertyTrace>()
            });
            
            // Propiedad 3
            var property3Id = ObjectId.GenerateNewId().ToString();
            properties.Add(new Property
            {
                Id = property3Id,
                Name = "Villa con Jardín",
                Address = "Residencial Los Pinos 25, Zona Sur",
                Price = 450000,
                CodeInternal = "PROP-003",
                Year = 2022,
                IdOwner = owners[2].Id,
                Images = new List<PropertyImage>
                {
                    new PropertyImage
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        File = "https://picsum.photos/800/600?random=4",
                        Enabled = true,
                        IdProperty = property3Id
                    },
                    new PropertyImage
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        File = "https://picsum.photos/800/600?random=5",
                        Enabled = true,
                        IdProperty = property3Id
                    },
                    new PropertyImage
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        File = "https://picsum.photos/800/600?random=6",
                        Enabled = true,
                        IdProperty = property3Id
                    }
                },
                Traces = new List<PropertyTrace>()
            });
            
            // Propiedad 4
            var property4Id = ObjectId.GenerateNewId().ToString();
            properties.Add(new Property
            {
                Id = property4Id,
                Name = "Loft Industrial",
                Address = "Distrito Industrial 200, Zona Este",
                Price = 320000,
                CodeInternal = "PROP-004",
                Year = 2019,
                IdOwner = owners[0].Id,
                Images = new List<PropertyImage>
                {
                    new PropertyImage
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        File = "https://picsum.photos/800/600?random=7",
                        Enabled = true,
                        IdProperty = property4Id
                    }
                },
                Traces = new List<PropertyTrace>()
            });
            
            // Propiedad 5
            var property5Id = ObjectId.GenerateNewId().ToString();
            properties.Add(new Property
            {
                Id = property5Id,
                Name = "Casa de Campo",
                Address = "Finca La Esperanza, Zona Rural",
                Price = 150000,
                CodeInternal = "PROP-005",
                Year = 2015,
                IdOwner = owners[1].Id,
                Images = new List<PropertyImage>
                {
                    new PropertyImage
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        File = "https://picsum.photos/800/600?random=8",
                        Enabled = true,
                        IdProperty = property5Id
                    }
                },
                Traces = new List<PropertyTrace>()
            });
            
            return properties;
        }
    }
}
