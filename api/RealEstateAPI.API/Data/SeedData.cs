using MongoDB.Bson;
using MongoDB.Driver;
using RealEstateAPI.Core.Entities;

namespace RealEstateAPI.API.Data
{
    public static class SeedData
    {
        public static async Task SeedDatabaseAsync(IMongoDatabase database)
        {
            // Primero me aseguro de que las colecciones existan
            await CreateCollectionsIfNotExistAsync(database);
            
            // Verifico si ya hay datos para evitar duplicados
            var propertiesCollection = database.GetCollection<Property>("properties");
            var ownersCollection = database.GetCollection<Owner>("owners");
            
            if (await propertiesCollection.CountDocumentsAsync(_ => true) > 0)
            {
                return; // Ya hay datos, no necesito crear más
            }
            
            // Creo los propietarios de ejemplo primero
            var owners = CreateSampleOwners();
            await ownersCollection.InsertManyAsync(owners);
            
            // Luego creo las propiedades usando los propietarios
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
        }
        
        public static List<Owner> CreateSampleOwners()
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
        
        public static List<Property> CreateSampleProperties(List<Owner> owners)
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
                        File = "https://lh3.googleusercontent.com/aida-public/AB6AXuDC4tYTpApFj8TWLc3Qme7XsaxEJe4cBhRFk_Czmuld6AU2LZtAqqTHVNABIMngaOQVIs_T-OgpJeoucY0l0ghHFR3Ad-AaPM7VuknFxrjO9FyfhhMTU2zwebEIiHf4kwX7H-vPsPYl83IsxJ1ZzTEhnDrT6ysLWF7NaZSt82jsNFbOJ53nDX5WSATw4SO3OVbBfebCjyP9oaeJyoxiC_pSUqhziHzOqqEahCfnwUNDQPnhTlC_OlmjczJQRxT8hxWKjzgbuqRFICdu",
                        Enabled = true,
                        IdProperty = property1Id
                    },
                    new PropertyImage
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        File = "https://lh3.googleusercontent.com/aida-public/AB6AXuDC4tYTpApFj8TWLc3Qme7XsaxEJe4cBhRFk_Czmuld6AU2LZtAqqTHVNABIMngaOQVIs_T-OgpJeoucY0l0ghHFR3Ad-AaPM7VuknFxrjO9FyfhhMTU2zwebEIiHf4kwX7H-vPsPYl83IsxJ1ZzTEhnDrT6ysLWF7NaZSt82jsNFbOJ53nDX5WSATw4SO3OVbBfebCjyP9oaeJyoxiC_pSUqhziHzOqqEahCfnwUNDQPnhTlC_OlmjczJQRxT8hxWKjzgbuqRFICdu",
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
                        File = "https://lh3.googleusercontent.com/aida-public/AB6AXuBRgWulSHhwY4yRO9MLGgSiusBKVzazjiU7KsddvurvDUJzhuhPzB6yjivvmZubE3foM33JNH8bqapboTRXYH2vSdtFtXaNZmmGZaptCFUs0jPDgV2VM22XD_9nIJsjYXRqwDzmzRXwwfqC3QlyV3kEItSHYM7ug7SnS8xEqr25vVM3OldL5FCW6A4UrJCQq7pqEhAc65oP54BX0DRt7_rwKigK9ZSelafT5NC6ag7sCcQ8QzswkMZ8WHeJZ-T_J6nouvsCk8LIgK8i",
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
                        File = "https://lh3.googleusercontent.com/aida-public/AB6AXuDxv2ye-FmOh9hcVo8SPiPD9QLzzPSqKQn0oEJrmZuPuhSnMV50M9yKUUSIsgftkHc5xWZwuuPdwLcBaguRkqZMft6zQO91ObHnGCXlr6sdzEnuezcqXWJS_1fy7pUF5TR5nLb0hIpmYC0QUGuqxQOqWtuUJ7r2vcTbV6TgbCuhUwD4ed16e9p77wGa-qiyZfG5iWLMsE5znV7mtQqo6a0HMa-Z5qTLsnLM2oarlZP-01eQ-llaNPvt2BX2NAxwZC9vmbNOkj56MDKZ",
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
                        File = "https://lh3.googleusercontent.com/aida-public/AB6AXuBWvEy8SdCTYiRCx4Kj8S3Eikth_1n0YNl_odCedTzePYFFy373CTHSKnlHbK2qbW-eLtBulAAS6rIjKuPgEgE6muGbH2g1RTahS2cTBxjvAY3fvxTNKXNTrHCLJfO39eq2HKkOM4LONG5GM7rm2Fws2XZ8x_lcutJaDNh4uEc8t1u9rhltZ5vrASIz_FzcgRAaM5rp3DmZ67t5uukQBJBlciS_wVf0B5OeYQ6Nan61eSJ4n0M1xVhS4zbveUKo2K2lxs1GxDY1KOK_",
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
                        File = "https://lh3.googleusercontent.com/aida-public/AB6AXuAjIpqxkVV5IErLETfy5S-7ASNxejGUQUKtVJ7VF7XSoXqGfHMlQTcB7QE5oSYHVSO7A4wZcXJrX9WTjGdHG-37llYBVtd1tNt3Oi8EdR2A84KfGVkXuBm9vnFrz_kj3raje0wZNhlmXWsqomrGKjrLLO1GB3hS5C86cjP9J7vom2TrSXgzLNmkyfo76P8DB6S5sX7H3q3KxsVsRUN-cg7dbJGKi8guj0arFHTIn8VDnc3SO7Ou9_tsctvJiryPF_Ls35m435S6rODS",
                        Enabled = true,
                        IdProperty = property5Id
                    }
                },
                Traces = new List<PropertyTrace>()
            });

            // Propiedad 6
            var property6Id = ObjectId.GenerateNewId().ToString();
            properties.Add(new Property
            {
                Id = property6Id,
                Name = "Casa de Gol Veraneo",
                Address = "Finca La Esperanza, Zona Boscosa",
                Price = 190000,
                CodeInternal = "PROP-006",
                Year = 2015,
                IdOwner = owners[1].Id,
                Images = new List<PropertyImage>
                {
                    new PropertyImage
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        File = "https://lh3.googleusercontent.com/aida-public/AB6AXuAABqDu_CQ9iFsBHjTNuD-kwLJKGaoLmZkDDF1YHt453mZThuvzwL3rzJ7XzYHqyFPGVyK9VVPZOI8A04ijE6tVvTPJhFaWg6zmzAX4Uam0ILgnkO9Tukp93qexQZ9GmqnWZDnzLgihpRhy7ShuMa2HDUx9kX9-KZcIgBYltY8cWrtLWvqjpIOaLjKtbO2ArQ_LiVOeNc6nZUjfZaDWMcKn4bvcCq51IR9k5i7hIQO1RjTqrcXqXpwPcalnUGq7bIiSJ5kzSfyiHhkM",
                        Enabled = true,
                        IdProperty = property6Id
                    }
                },
                Traces = new List<PropertyTrace>()
            });

            return properties;
        }
    }
}
