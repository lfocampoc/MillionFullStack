using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;

namespace RealEstateAPI.API.HealthChecks
{
    public class MongoDbHealthCheck : IHealthCheck
    {
        private readonly IMongoDatabase _database;

        public MongoDbHealthCheck(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                // Verificar la conexión ejecutando un comando simple
                await _database.RunCommandAsync((Command<object>)"{ping:1}", cancellationToken: cancellationToken);
                
                return HealthCheckResult.Healthy("MongoDB connection is working");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("MongoDB connection failed", ex);
            }
        }
    }
}
