using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RealEstateAPI.Core.DTOs;

namespace RealEstateAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly HealthCheckService _healthCheckService;
        private readonly ILogger<HealthController> _logger;

        public HealthController(HealthCheckService healthCheckService, ILogger<HealthController> logger)
        {
            _healthCheckService = healthCheckService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<object>>> GetHealth()
        {
            try
            {
                _logger.LogInformation("Health check requested");
                var healthReport = await _healthCheckService.CheckHealthAsync();
                
                var status = healthReport.Status == HealthStatus.Healthy ? "Healthy" : "Unhealthy";
                var data = new
                {
                    status = status,
                    totalDuration = healthReport.TotalDuration,
                    entries = healthReport.Entries.Select(entry => new
                    {
                        name = entry.Key,
                        status = entry.Value.Status.ToString(),
                        duration = entry.Value.Duration,
                        description = entry.Value.Description,
                        data = entry.Value.Data
                    })
                };

                return Ok(ApiResponse<object>.SuccessResult(data, $"System is {status}"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during health check");
                return Ok(ApiResponse<object>.ErrorResult(ex.Message, "Health check failed"));
            }
        }

        [HttpGet("ready")]
        public async Task<ActionResult<ApiResponse<object>>> GetReadiness()
        {
            try
            {
                _logger.LogInformation("Readiness check requested");
                var healthReport = await _healthCheckService.CheckHealthAsync(check => check.Tags.Contains("ready"));
                
                var status = healthReport.Status == HealthStatus.Healthy ? "Ready" : "Not Ready";
                var data = new
                {
                    status = status,
                    totalDuration = healthReport.TotalDuration,
                    entries = healthReport.Entries.Select(entry => new
                    {
                        name = entry.Key,
                        status = entry.Value.Status.ToString(),
                        duration = entry.Value.Duration,
                        description = entry.Value.Description
                    })
                };

                return Ok(ApiResponse<object>.SuccessResult(data, $"System is {status}"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during readiness check");
                return Ok(ApiResponse<object>.ErrorResult(ex.Message, "Readiness check failed"));
            }
        }
    }
}
